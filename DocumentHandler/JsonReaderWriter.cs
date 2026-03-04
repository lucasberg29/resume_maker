using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentHandler.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DocumentHandler
{
    internal class JsonReaderWriter
    {
        public static void ReadResumeFromJson( ref Resume CurrentResume, string jsonFileName)
        {
            string jsonFilePath = Path.Combine(AppContext.BaseDirectory, jsonFileName);

            try
            {
                string json = File.ReadAllText(jsonFilePath);
                CurrentResume = JsonSerializer.Deserialize<Resume>(json);
            }
            catch (Exception ex)
            {

            }

        }

        public static void WriteResumeToJson(Resume currentResume, string jsonFileName)
        {
            string jsonFilePath = Path.Combine(AppContext.BaseDirectory, jsonFileName);

            var json = JsonSerializer.Serialize(currentResume);
            File.WriteAllText(jsonFilePath, json);
        }
    }
}
