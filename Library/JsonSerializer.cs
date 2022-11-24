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
            path = ".\\" + fileName;
        }

        
        /// <summary>
        /// DESERIALIZES A JSON FILE AND RETURNS THE DATA AS WHATEVER OBJECT WAS PASSED IN THE GENERIC VALUE.
        /// ---
        /// DESERIALIZA UN ARCHIVO JSON Y RETORNA LOS DATOS COMO CUALQUIER OBJETO QUE HAYA SIDO PASADO EN EL VALOR GENERICO.
        /// </summary>
        /// <returns></returns>
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

        
        /// <summary>
        /// SERIALIZES AN OBJECT INTO A JSON FILE.
        /// ---
        /// SERIALIZA UN OBJETO EN UN ARCHIVO JSON.
        /// </summary>
        /// <param name="objectName"></param>
        /// <exception cref="Exception"></exception>
        public void Serialize(T objectName)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;

                string json = JsonSerializer.Serialize(objectName, options);

                File.WriteAllText(path + ".json", json);                                
            }
            catch (Exception e)
            {                                
                throw new Exception(e.Message, e);
            }            
        }
    }
}
