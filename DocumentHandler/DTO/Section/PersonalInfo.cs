using DocumentHandler.DTO.Paragraphs;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO.Section
{
    public class PersonalInfo
    {
        [JsonPropertyName("fullName")]
        public ResumeParagraph FullName { get; set; } = new ResumeParagraph() { Elements = new() {new Element("FullName") }  };
        [JsonPropertyName("contact")]
        public Contact Contact { get; set; } = new Contact();
        [JsonPropertyName("introduction")]
        public Introduction Introduction { get; set; } = new Introduction();
        [JsonPropertyName("socialMediaLinks")]
        public List<SocialMediaLink> SocialMediaLinks { get; set; } = new List<SocialMediaLink>();
        [JsonPropertyName("technicalSkillsHeader")]
        public Element TechnicalSkillsHeader { get; set; } = new Element("Technical Skills");
        [JsonPropertyName("technicalSkills")]
        public List<TechnicalSkill> TechnicalSkills { get; set; } = new List<TechnicalSkill>();

        public void Init()
        {
            Contact.Init();
        }

        public void SetFullNameText(string text)
        {
            FullName.Elements.First().Text = text;  
        }

        public void SetEmailText(string text)
        {
            Contact.Elements[0].Text = text;
        }   

        public void SetPhoneNumberText(string text)
        {
            Contact.Elements[1].Text = text;
        }

        public void SetLocationText(string text)
        {
            Contact.Elements[2].Text = text;
        }
         public void SetIntroductionText(string text)
        {
            Introduction.Elements[0].Text = text;
        }   
    }
}