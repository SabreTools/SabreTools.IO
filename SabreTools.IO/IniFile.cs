using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SabreTools.IO.Readers;
using SabreTools.IO.Writers;

namespace SabreTools.IO
{
    /// <summary>
    /// Key-value pair INI file
    /// </summary>
    public class IniFile : IDictionary<string, string?>
    {
        private readonly Dictionary<string, string?> _keyValuePairs = [];

        public string? this[string? key]
        {
            get
            {
                key = key?.ToLowerInvariant() ?? string.Empty;
                if (_keyValuePairs.ContainsKey(key))
                    return _keyValuePairs[key];

                return null;
            }
            set
            {
                key = key?.ToLowerInvariant() ?? string.Empty;
                _keyValuePairs[key] = value;
            }
        }

        /// <summary>
        /// Create an empty INI file
        /// </summary>
        public IniFile()
        {
        }

        /// <summary>
        /// Populate an INI file from path
        /// </summary>
        public IniFile(string path)
        {
            Parse(path);
        }

        /// <summary>
        /// Populate an INI file from stream
        /// </summary>
        public IniFile(Stream stream)
        {
            Parse(stream);
        }

        /// <summary>
        /// Add or update a key and value to the INI file
        /// </summary>
        public void AddOrUpdate(string key, string value)
        {
            this[key] = value;
        }

        /// <summary>
        /// Remove a key from the INI file
        /// </summary>
        public bool Remove(string key)
        {
            if (_keyValuePairs.ContainsKey(key))
            {
                _keyValuePairs.Remove(key.ToLowerInvariant());
                return true;
            }

            return false;
        }

        /// <summary>
        /// Read an INI file based on the path
        /// </summary>
        public bool Parse(string path)
        {
            // If we don't have a file, we can't read it
            if (!File.Exists(path))
                return false;

            using var fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return Parse(fileStream);
        }

        /// <summary>
        /// Read an INI file from a stream
        /// </summary>
        public bool Parse(Stream? stream)
        {
            // If the stream is invalid or unreadable, we can't process it
            if (stream == null || !stream.CanRead || stream.Position >= stream.Length - 1)
                return false;

            // Keys are case-insensitive by default
            try
            {
                // TODO: Can we use the section header in the reader?
                using var reader = new IniReader(stream, Encoding.UTF8);

                string? section = string.Empty;
                while (!reader.EndOfStream)
                {
                    // If we dont have a next line
                    if (!reader.ReadNextLine())
                        break;

                    // Process the row according to type
                    switch (reader.RowType)
                    {
                        case IniRowType.SectionHeader:
                            section = reader.Section;
                            break;

                        case IniRowType.KeyValue:
                            string? key = reader.KeyValuePair?.Key;

                            // Section names are prepended to the key with a '.' separating
                            if (!string.IsNullOrEmpty(section))
                                key = $"{section}.{key}";

                            // Set or overwrite keys in the returned dictionary
                            this[key] = reader.KeyValuePair?.Value;
                            break;

                        default:
                            // No-op
                            break;
                    }
                }
            }
            catch
            {
                // We don't care what the error was, just catch and return
                return false;
            }

            return true;
        }

        /// <summary>
        /// Write an INI file to a path
        /// </summary>
        public bool Write(string path)
        {
            // If we don't have a valid dictionary with values, we can't write out
            if (_keyValuePairs.Count == 0)
                return false;

            using var fileStream = File.OpenWrite(path);
            return Write(fileStream);
        }

        /// <summary>
        /// Write an INI file to a stream
        /// </summary>
        public bool Write(Stream stream)
        {
            // If we don't have a valid dictionary with values, we can't write out
            if (_keyValuePairs.Count == 0)
                return false;

            // If the stream is invalid or unwritable, we can't output to it
            if (stream == null || !stream.CanWrite || stream.Position >= stream.Length - 1)
                return false;

            try
            {
                using IniWriter writer = new(stream, Encoding.UTF8);

                // Order the keys to link sections together
                var orderedKeys = new string[_keyValuePairs.Keys.Count];
                _keyValuePairs.Keys.CopyTo(orderedKeys, 0);
                Array.Sort(orderedKeys);

                string section = string.Empty;
                for (int i = 0; i < orderedKeys.Length; i++)
                {
                    // Retrive the key and value
                    string key = orderedKeys[i];
                    string? value = _keyValuePairs[key];

                    // We assume '.' is a section name separator
                    if (key.Contains("."))
                    {
                        // Split the key by '.'
                        string[] data = key.Split('.');

                        // If the key contains an '.', we need to put them back in
                        string newSection = data[0].Trim();
                        string[] keyArr = new string[data.Length - 1];
                        Array.Copy(data, 1, keyArr, 0, keyArr.Length);
                        key = string.Join(".", keyArr).Trim();

                        // If we have a new section, write it out
                        if (!string.Equals(newSection, section, StringComparison.OrdinalIgnoreCase))
                        {
                            writer.WriteSection(newSection);
                            section = newSection;
                        }
                    }

                    // Now write out the key and value in a standardized way
                    writer.WriteKeyValuePair(key, value);
                }
            }
            catch
            {
                // We don't care what the error was, just catch and return
                return false;
            }

            return true;
        }

        #region IDictionary Impelementations

        public ICollection<string> Keys => _keyValuePairs.Keys;

        public ICollection<string?> Values => _keyValuePairs.Values;

        public int Count => (_keyValuePairs as ICollection<KeyValuePair<string, string>>)?.Count ?? 0;

        public bool IsReadOnly => false;

        public void Add(string key, string? value) => this[key] = value;

        bool IDictionary<string, string?>.Remove(string key) => Remove(key);

        public bool TryGetValue(string key, out string? value)
        {
            value = null;
            return _keyValuePairs?.TryGetValue(key.ToLowerInvariant(), out value) ?? false;
        }

        public void Add(KeyValuePair<string, string?> item) => this[item.Key] = item.Value;

        public void Clear() => _keyValuePairs?.Clear();

        public bool Contains(KeyValuePair<string, string?> item)
        {
            var newItem = new KeyValuePair<string, string?>(item.Key.ToLowerInvariant(), item.Value);
            return (_keyValuePairs as ICollection<KeyValuePair<string, string?>>)?.Contains(newItem) ?? false;
        }

        public bool ContainsKey(string? key) => _keyValuePairs?.ContainsKey(key?.ToLowerInvariant() ?? string.Empty) ?? false;

        public void CopyTo(KeyValuePair<string, string?>[] array, int arrayIndex)
        {
            (_keyValuePairs as ICollection<KeyValuePair<string, string?>>)?.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, string?> item)
        {
            var newItem = new KeyValuePair<string, string?>(item.Key.ToLowerInvariant(), item.Value);
            return (_keyValuePairs as ICollection<KeyValuePair<string, string?>>)?.Remove(newItem) ?? false;
        }

        public IEnumerator<KeyValuePair<string, string?>> GetEnumerator()
        {
            return (_keyValuePairs as IEnumerable<KeyValuePair<string, string?>>)!.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_keyValuePairs as IEnumerable)!.GetEnumerator();
        }

        #endregion
    }
}
