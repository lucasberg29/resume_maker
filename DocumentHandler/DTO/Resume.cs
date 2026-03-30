using DocumentHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Resume
    {
        [JsonPropertyName("fullName")]
        public Element FullName { get; set; } = new Element();
        [JsonPropertyName("contact")]
        public Contact Contact { get; set; } = new Contact();   
        [JsonPropertyName("introduction")]
        public Element Introduction { get; set; } = new Element();
        [JsonPropertyName("socialMediaLinks")]
        public List<SocialMediaLink> SocialMediaLinks { get; set; } = new List<SocialMediaLink>();
        [JsonPropertyName("technicalSkillsHeader")]
        public Element TechnicalSkillsHeader { get; set; } = new Element("Technical Skills");
        [JsonPropertyName("technicalSkills")]
        public List<TechnicalSkill> TechnicalSkills { get; set; } = new List<TechnicalSkill>();
        [JsonPropertyName("experienceHeader")]
        public Element ExperienceHeader { get; set; } = new Element("Experience");
        [JsonPropertyName("experience")]
        public List<Experience> Experience { get; set; } = new List<Experience>();
        [JsonPropertyName("educationHeader")]
        public Element EducationHeader { get; set; } = new Element("Education");
        [JsonPropertyName("education")]
        public List<Education> Education { get; set; } = new List<Education>();
        [JsonPropertyName("skillsHeader")]
        public Element SkillsHeader { get; set; } = new Element("Skills");
        [JsonPropertyName("otherExperienceHeader")]
        public Element OtherExperienceHeader { get; set; } = new Element("Other Experience");
        [JsonPropertyName("otherExperience")]
        public List<OtherExperience> OtherExperience { get; set; } = new List<OtherExperience>();
    }
}