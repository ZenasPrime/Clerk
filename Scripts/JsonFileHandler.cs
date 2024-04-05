using System;
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
        /// Writes an object of type T to a JSON file within the Unity Resources folder.
        /// This makes it possible to bundle the JSON file with your project's assets.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize and write.</typeparam>
        /// <param name="filePath">The relative file path within the Resources folder, excluding the '.json' extension.</param>
        /// <param name="data">The object to serialize to JSON.</param>
        public static void WriteToJsonFileInResources<T>(string filePath, T data)
        {
            string json = JsonUtility.ToJson(data, true);
            string fullPath = Application.dataPath + "/Resources/" + filePath + ".json";
            File.WriteAllText(fullPath, json);
        }
        
        /// <summary>
        /// Tries to serialize an object of type T to JSON and write it to the specified file path, handling any exceptions.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        /// <param name="filePath">The file path where the JSON should be saved.</param>
        /// <param name="data">The object to serialize to JSON.</param>
        /// <returns>True if the file was successfully written; otherwise, false.</returns>
        public static bool TryWriteToJsonFile<T>(string filePath, T data)
        {
            try
            {
                string json = JsonUtility.ToJson(data, true);
                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to write JSON file at {filePath}: {e.Message}");
                return false;
            }
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
        /// Reads a JSON file from the Unity Resources folder and deserializes its content into an object of type T.
        /// This method is useful for accessing JSON files included in the project's Resources directory,
        /// allowing for easier asset bundling and deployment.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize the JSON content into.</typeparam>
        /// <param name="filePath">The relative path within the Resources folder to the JSON file, without the file extension.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the JSON file is not found within the Resources folder at the specified filePath.</exception>
        public static T ReadFromJsonFileInResources<T>(string filePath)
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(filePath);
            if (jsonFile != null)
            {
                return JsonUtility.FromJson<T>(jsonFile.text);
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
            try
            {
                string jsonData = File.ReadAllText(filePath);
                data = JsonUtility.FromJson<T>(jsonData);
                return true;
            }
            catch (FileNotFoundException)
            {
                Debug.LogWarning($"File does not exist at {filePath}. Returning default value for type {typeof(T)}.");
                data = default(T);
                return false;
            }
        }
    }
}
