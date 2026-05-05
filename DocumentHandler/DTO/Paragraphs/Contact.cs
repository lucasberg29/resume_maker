
namespace DocumentHandler.DTO.Paragraphs
{
    public class Contact : ResumeParagraph
    {
        public Contact()
        {
            ParagraphTag = "Contact";

            Elements.Add(new Element("Email", "Email"));
            Elements.Add(new Element("PhoneNumber", "PhoneNumber"));
            Elements.Add(new Element("Location", "Location"));
        }

        internal void Init()
        {
            Elements[0].Tag = "Email";
            Elements[1].Tag = "PhoneNumber";
            Elements[2].Tag = "Location";
        }
    }
}
