using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ZenTools.Clerk
{
    /// <summary>
    /// Provides static methods for writing to and reading from JSON files.
    /// </summary>
    public static class JsonFileHandler
    {
        /// <summary>
        // /// Serializes an object of type T to JSON and writes it to the specified file.
        // /// </summary>
        // /// <typeparam name="T">The type of object to serialize.</typeparam>
        // /// <param name="filePath">The file path where the JSON should be saved.</param>
        // /// <param name="data">The object to serialize to JSON.</param>
        public static void WriteToJsonFile<T>(string filePath, T data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
        }
        
        /// <summary>
        // /// Reads a JSON file and deserializes its content into an object of type T.
        // /// </summary>
        // /// <typeparam name="T">The type of object to deserialize to.</typeparam>
        // /// <param name="filePath">The file path from which to read the JSON.</param>
        // /// <returns>The deserialized object of type T. Returns the default value of T if the file does not exist.</returns>
        public static T ReadFromJsonFile<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(jsonData);
            }
            else
            {
                throw new FileNotFoundException("JSON file not found at " + filePath);
            }
        }
        
        /// <summary>
        /// Attempts to read from a JSON file and deserialize its content into an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to.</typeparam>
        /// <param name="filePath">The file path from which to read the JSON.</param>
        /// <param name="data">The output parameter where the deserialized object of type T will be stored if reading is successful.</param>
        /// <returns>True if the file is successfully read and deserialized; otherwise, false.</returns>
        public static bool TryReadFromJsonFile<T>(string filePath, out T data)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                data = JsonUtility.FromJson<T>(jsonData);
                return true;
            }
            else
            {
                data = default(T);
                return false;
            }
        }
    }
}
