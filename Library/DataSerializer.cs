using System;
using System.IO;
using System.Text.Json;

namespace Library
{
    public class JsonSerializer<T> where T : class, new()
    {
        public string path;

        public JsonSerializer(string fileName)
        {
            path += ".\\" + fileName;
        }

        public T DeSerialize()
        {
            T obj = new T();
            try
            {
                string jsonFile = File.ReadAllText(path + ".json");

                obj = JsonSerializer.Deserialize<T>(jsonFile);
            }
            catch (Exception)
            {
                return null;
            }
            return obj;
        }

        public bool Serialize(T objectName)
        {
            bool returnValue = false;
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;

                string json = JsonSerializer.Serialize(objectName, options);

                File.WriteAllText(path + ".json", json);

                returnValue = true;
            }
            catch (Exception)
            {
                returnValue = false;
            }
            return returnValue;
        }
    }
}
