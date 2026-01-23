using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace BGNS_Studios
{
    public class JsonPersistence<T> where T : class
    {
        private string _filename;


        public JsonPersistence(string filename)
        {
            _filename = $"{filename}.json";
        }

        public void Save(T data)
        {
            string path = GetOrCreateFile();
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(json);
            }
            Debug.Log($"Data saved to {path}");
        }

        public T Load()
        {
            string path = GetOrCreateFile();
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                if (string.IsNullOrEmpty(json))
                    return null;
                T data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
        }


        public string GetOrCreateFile()
        {
            string path = Path.Combine(Application.persistentDataPath, _filename);

            if (!File.Exists(path))
                File.Create(path).Dispose();

            return path;
        }
    }
}
