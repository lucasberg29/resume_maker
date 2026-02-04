


namespace DocumentConsoleApp
{



    internal class Program
    {
        static void Main(string[] args)
        {
            string resumeDocument = "Resume.docx";

            DocumentHandler.DocumentReaderWriter.CreateDocument(resumeDocument);

            DocumentHandler.DocumentHandler handler = new DocumentHandler.DocumentHandler();

            handler.InitHandler(resumeDocument);

            Console.WriteLine("Hello, World!");

            
        }
    }
}
