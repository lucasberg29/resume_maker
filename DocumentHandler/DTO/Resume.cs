using DocumentFormat.OpenXml.Presentation;
using DocumentHandler.DTO.Paragraphs;
using DocumentHandler.DTO.Section;
using DocumentHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler.DTO
{
    public class Resume
    {
        [JsonPropertyName("personalInfo")]
        public PersonalInfo PersonalInfo { get; set; } = new PersonalInfo();
        [JsonPropertyName("allTechnicalSkills")]
        public AllTechnicalSkills AllTechnicalSkills { get; set; } = new AllTechnicalSkills();  
        [JsonPropertyName("allExperiences")]
        public AllExperiences AllExperiences { get; set; } = new AllExperiences();
        [JsonPropertyName("allEducation")]
        public AllEducation AllEducation { get; set; } = new AllEducation();
        [JsonPropertyName("allOtherExperience")]
        public AllOtherExperience AllOtherExperience { get; set; } = new AllOtherExperience();

        public void Init()
        {
            PersonalInfo.Init();
        }

        public List<ResumeParagraph> GetAllParagraphs()
        {
            var paragraphs = new List<ResumeParagraph>
            {
                PersonalInfo.FullName,
                PersonalInfo.Contact,
                PersonalInfo.Introduction,
                AllTechnicalSkills.TechnicalSkillsHeader,
                AllExperiences.ExperienceHeader,
                AllEducation.EducationHeader,
                AllOtherExperience.OtherExperienceHeader,
            };

            foreach (var exp in AllExperiences.Experiences)
            {
                paragraphs.AddRange(exp.GetAllParagraphs());
            }

            foreach (var edu in AllEducation.Education)
            {
                paragraphs.AddRange(edu.GetAllParagraphs());
            }

            return paragraphs;
        }

        public List<Element> GetAllElements()
        {
            return GetAllParagraphs().SelectMany(p => p.Elements).ToList();
        }
    }
}