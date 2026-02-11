using DocumentHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocumentHandler
{
    public class Resume : IResume
    {
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = "";
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = "";
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";
        [JsonPropertyName("location")]
        public string Location { get; set; } = "";
        [JsonPropertyName("introduction")]
        public string Introduction { get; set; } = "";
        [JsonPropertyName("socialMediaLinks")]
        public List<SocialMediaLink> SocialMediaLinks { get; set; } = new List<SocialMediaLink>();
        [JsonPropertyName("technicalSkills")]
        public List<TechnicalSkill> TechnicalSkills { get; set; } = new List<TechnicalSkill>();
        [JsonPropertyName("experience")]
        public List<Experience> Experience { get; set; } = new List<Experience>();
        [JsonPropertyName("education")]
        public List<Education> Education { get; set; } = new List<Education>();
        [JsonPropertyName("skills")]
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}