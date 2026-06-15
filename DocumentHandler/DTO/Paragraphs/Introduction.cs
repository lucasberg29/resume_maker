using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentHandler.DTO.Paragraphs
{
    public class Introduction: ResumeParagraph
    {
        public Introduction()
        {
            ParagraphTag = "Introduction";
            Elements.Add(new Element("Introduction"));
        }
    }
}