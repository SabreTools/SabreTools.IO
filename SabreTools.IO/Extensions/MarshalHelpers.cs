using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Common methods for use during marshalling
    /// </summary>
    internal static class MarshalHelpers
    {
        /// <summary>
        /// Get an attribute of the requested type
        /// </summary>
        public static TAttribute? GetAttribute<TAttribute>(FieldInfo? fi) where TAttribute : Attribute
        {
            // If the field info is invalid
            if (fi == null)
                return null;

            // Get all matching attributes
            var attributes = fi.GetCustomAttributes(typeof(TAttribute), true);
            if (attributes == null || attributes.Length == 0)
                return null;

            // Get the first attribute that matches
            return attributes[0] as TAttribute;
        }

        /// <summary>
        /// Get an attribute of the requested type
        /// </summary>
        public static TAttribute? GetAttribute<TAttribute>(Type? type) where TAttribute : Attribute
        {
            // If the field info is invalid
            if (type == null)
                return null;

            // Get all matching attributes
            var attributes = type.GetCustomAttributes(typeof(TAttribute), true);
            if (attributes == null || attributes.Length == 0)
                return null;

            // Get the first attribute that matches
            return attributes[0] as TAttribute;
        }

        /// <summary>
        /// Determine the layout kind for a type
        /// </summary>
        public static LayoutKind DetermineLayoutKind(StructLayoutAttribute? layoutAttr, Type type)
        {
            LayoutKind layoutKind = LayoutKind.Auto;

            if (layoutAttr != null)
                layoutKind = layoutAttr.Value;
            else if (type.IsAutoLayout)
                layoutKind = LayoutKind.Auto;
            else if (type.IsExplicitLayout)
                layoutKind = LayoutKind.Explicit;
            else if (type.IsLayoutSequential)
                layoutKind = LayoutKind.Sequential;

            return layoutKind;
        }

        /// <summary>
        /// Determine the encoding for a type
        /// </summary>
        public static Encoding DetermineEncoding(StructLayoutAttribute? layoutAttr)
        {
            return layoutAttr?.CharSet switch
            {
                CharSet.None => Encoding.ASCII,
                CharSet.Ansi => Encoding.ASCII,
                CharSet.Unicode => Encoding.Unicode,
                CharSet.Auto => Encoding.ASCII, // UTF-8 on Unix
                _ => Encoding.ASCII,
            };
        }

        /// <summary>
        /// Determine the parent hierarchy for a given type
        /// </summary>
        /// <remarks>Returns the highest parent as the first element</remarks>
        public static IEnumerable<Type> DetermineTypeLineage(Type type)
        {
            var lineage = new List<Type>();
            while (type != typeof(object) && type != typeof(ValueType))
            {
                lineage.Add(type);
                type = type.BaseType ?? typeof(object);
            }

            lineage.Reverse();
            return lineage;
        }

        /// <summary>
        /// Get an ordered set of fields from a type
        /// </summary>
        /// <remarks>Returns fields from the parents before fields from the type</remarks>
        public static FieldInfo[] GetFields(Type type)
        {
            // Get the type hierarchy for ensuring serialization order
            var lineage = DetermineTypeLineage(type);

            // Generate the fields by parent first
            var fieldsList = new List<FieldInfo>();
            foreach (var nextType in lineage)
            {
                var nextFields = nextType.GetFields();
                foreach (var field in nextFields)
                {
                    // Add fields if they aren't already included
                    int index = fieldsList.FindIndex(f => f.Name == field.Name && f.FieldType == field.FieldType);
                    if (index == -1)
                        fieldsList.Add(field);
                }
            }

            return [.. fieldsList];
        }

        /// <summary>
        /// Get the expected array size for a field
        /// </summary>
        /// <returns>Array size on success, -1 on failure</returns>
        public static int GetArrayElementCount(MarshalAsAttribute marshalAsAttr, FieldInfo[] fields, object instance)
        {
            int elementCount = -1;
            if (marshalAsAttr.Value == UnmanagedType.ByValArray)
            {
                elementCount = marshalAsAttr.SizeConst;
            }
            else if (marshalAsAttr.Value == UnmanagedType.LPArray)
            {
                elementCount = marshalAsAttr.SizeConst;
                if (marshalAsAttr.SizeParamIndex >= 0)
                    elementCount = GetLPArraySizeFromField(marshalAsAttr, fields, instance);
            }

            return elementCount;
        }

        /// <summary>
        /// Get the expected LPArray size from a field
        /// </summary>
        public static int GetLPArraySizeFromField(MarshalAsAttribute marshalAsAttr, FieldInfo[] fields, object instance)
        {
            // If the index is invalid
            if (marshalAsAttr.SizeParamIndex < 0)
                return -1;

            // Get the size field
            var sizeField = fields[marshalAsAttr.SizeParamIndex];
            if (sizeField == null)
                return -1;

            // Cast based on the field type
            return sizeField.GetValue(instance) switch
            {
                sbyte val => val,
                byte val => val,
                short val => val,
                ushort val => val,
                int val => val,
                uint val => (int)val,
                long val => (int)val,
                ulong val => (int)val,
                _ => -1,
            };
        }
    }
}
