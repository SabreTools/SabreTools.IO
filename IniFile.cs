using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SabreTools.IO.Readers;
using SabreTools.IO.Writers;

namespace SabreTools.IO
{
    /// <summary>
    /// Key-value pair INI file
    /// </summary>
#if NET48
    public class IniFile : IDictionary<string, string>
#else
    public class IniFile : IDictionary<string, string?>
#endif
    {
#if NET48
        private Dictionary<string, string> _keyValuePairs = new Dictionary<string, string>();
#else
        private Dictionary<string, string?>? _keyValuePairs = new Dictionary<string, string?>();
#endif

#if NET48
        public string this[string key]
#else
        public string? this[string? key]
#endif
        {
            get
            {
                if (_keyValuePairs == null)
#if NET48
                    _keyValuePairs = new Dictionary<string, string>();
#else
                    _keyValuePairs = new Dictionary<string, string?>();
#endif

                key = key?.ToLowerInvariant() ?? string.Empty;
                if (_keyValuePairs.ContainsKey(key))
                    return _keyValuePairs[key];

                return null;
            }
            set
            {
                if (_keyValuePairs == null)
#if NET48
                    _keyValuePairs = new Dictionary<string, string>();
#else
                    _keyValuePairs = new Dictionary<string, string?>();
#endif

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
            this.Parse(path);
        }

        /// <summary>
        /// Populate an INI file from stream
        /// </summary>
        public IniFile(Stream stream)
        {
            this.Parse(stream);
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
            if (_keyValuePairs != null && _keyValuePairs.ContainsKey(key))
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

            using (var fileStream = File.OpenRead(path))
            {
                return Parse(fileStream);
            }
        }

        /// <summary>
        /// Read an INI file from a stream
        /// </summary>
#if NET48
        public bool Parse(Stream stream)
#else
        public bool Parse(Stream? stream)
#endif
        {
            // If the stream is invalid or unreadable, we can't process it
            if (stream == null || !stream.CanRead || stream.Position >= stream.Length - 1)
                return false;

            // Keys are case-insensitive by default
            try
            {
                using (var reader = new IniReader(stream, Encoding.UTF8))
                {
                    // TODO: Can we use the section header in the reader?
#if NET48
                    string section = string.Empty;
#else
                    string? section = string.Empty;
#endif
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
#if NET48
                                string key = reader.KeyValuePair?.Key;
#else
                                string? key = reader.KeyValuePair?.Key;
#endif

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
            if (_keyValuePairs == null || _keyValuePairs.Count == 0)
                return false;

            using (var fileStream = File.OpenWrite(path))
            {
                return Write(fileStream);
            }
        }

        /// <summary>
        /// Write an INI file to a stream
        /// </summary>
        public bool Write(Stream stream)
        {
            // If we don't have a valid dictionary with values, we can't write out
            if (_keyValuePairs == null || _keyValuePairs.Count == 0)
                return false;

            // If the stream is invalid or unwritable, we can't output to it
            if (stream == null || !stream.CanWrite || stream.Position >= stream.Length - 1)
                return false;

            try
            {
                using (IniWriter writer = new IniWriter(stream, Encoding.UTF8))
                {
                    // Order the dictionary by keys to link sections together
                    var orderedKeyValuePairs = _keyValuePairs.OrderBy(kvp => kvp.Key);

                    string section = string.Empty;
                    foreach (var keyValuePair in orderedKeyValuePairs)
                    {
                        // Extract the key and value
                        string key = keyValuePair.Key;
#if NET48
                        string value = keyValuePair.Value;
#else
                        string? value = keyValuePair.Value;
#endif

                        // We assume '.' is a section name separator
                        if (key.Contains('.'))
                        {
                            // Split the key by '.'
                            string[] data = keyValuePair.Key.Split('.');

                            // If the key contains an '.', we need to put them back in
                            string newSection = data[0].Trim();
                            key = string.Join(".", data.Skip(1)).Trim();

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
            }
            catch
            {
                // We don't care what the error was, just catch and return
                return false;
            }

            return true;
        }

        #region IDictionary Impelementations

#if NET48
        public ICollection<string> Keys => _keyValuePairs?.Keys;

        public ICollection<string> Values => _keyValuePairs?.Values;

        public int Count => _keyValuePairs?.Count ?? 0;

        public bool IsReadOnly => false;

        public void Add(string key, string value) => this[key] = value;

        bool IDictionary<string, string>.Remove(string key) => Remove(key);

        public bool TryGetValue(string key, out string value)
        {
            value = null;
            return _keyValuePairs?.TryGetValue(key.ToLowerInvariant(), out value) ?? false;
        }

        public void Add(KeyValuePair<string, string> item) => this[item.Key] = item.Value;

        public void Clear() => _keyValuePairs?.Clear();

        public bool Contains(KeyValuePair<string, string> item)
        {
            var newItem = new KeyValuePair<string, string>(item.Key.ToLowerInvariant(), item.Value);
            return (_keyValuePairs as ICollection<KeyValuePair<string, string>>)?.Contains(newItem) ?? false;
        }

        public bool ContainsKey(string key) => _keyValuePairs?.ContainsKey(key?.ToLowerInvariant() ?? string.Empty) ?? false;

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            (_keyValuePairs as ICollection<KeyValuePair<string, string>>)?.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            var newItem = new KeyValuePair<string, string>(item.Key.ToLowerInvariant(), item.Value);
            return (_keyValuePairs as ICollection<KeyValuePair<string, string>>)?.Remove(newItem) ?? false;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return (_keyValuePairs as IEnumerable<KeyValuePair<string, string>>)?.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_keyValuePairs as IEnumerable)?.GetEnumerator();
        }
#else
        public ICollection<string> Keys => _keyValuePairs?.Keys?.ToArray() ?? Array.Empty<string>();

        public ICollection<string?> Values => _keyValuePairs?.Values?.ToArray() ?? Array.Empty<string?>();

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
        #endif

        #endregion
    }
}
