using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentHandler.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace DocumentHandler.Handlers
{
    internal class JsonHandler
    {
        public static void ReadResumeFromJson( ref Resume CurrentResume, string jsonFileName)
        {
            try
            {
                string jsonFilePath = Path.Combine(AppContext.BaseDirectory, jsonFileName);
                string json = File.ReadAllText(jsonFilePath);
                CurrentResume = JsonSerializer.Deserialize<Resume>(json);
                CurrentResume.Init();
            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(ex, true);
                DocumentHandler.ErrorHandler.AddError(ex, stackTrace);
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
