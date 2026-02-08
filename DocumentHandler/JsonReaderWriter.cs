using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DocumentHandler
{
    internal class JsonReaderWriter
    {
        private static string JsonFilePath = Path.Combine(AppContext.BaseDirectory, "data.json");

        public static void ReadResumeFromJson( ref Resume CurrentResume)
        {
            string json = File.ReadAllText(JsonFilePath);
            CurrentResume = JsonSerializer.Deserialize<Resume>(json);
        }

        internal static void WriteResumeToJson(Resume currentResume)
        {
            var json = JsonSerializer.Serialize(currentResume);
            File.WriteAllText(JsonFilePath, json);
        }
    }
}
