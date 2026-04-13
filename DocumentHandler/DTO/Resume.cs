using DocumentFormat.OpenXml.Presentation;
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
    }
}