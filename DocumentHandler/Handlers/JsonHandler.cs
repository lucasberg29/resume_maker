using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentHandler.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DocumentHandler.Handlers
{
    internal class JsonHandler
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
                //TODO: Handle exceptions (e.g., file not found, invalid JSON format)

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
