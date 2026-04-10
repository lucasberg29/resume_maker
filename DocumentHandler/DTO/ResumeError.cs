using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHandler.DTO
{
    public class ResumeMakerError
    {
        public string Message { get; set; } = string.Empty;
        public DateTime Time { get; set; } = DateTime.Now;

        public string Location { get; set; } = string.Empty;

        public string MethodName { get; set; } = string.Empty;  

        public string ClassName { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public int LineNumber { get; set; } = 0;
    }
}
