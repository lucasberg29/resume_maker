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
        [JsonPropertyName("technicalSkills")]
        public List<TechnicalSkill> TechnicalSkills { get; set; } = new List<TechnicalSkill>();
        [JsonPropertyName("experience")]
        public Experience[] Experience { get; set; } = Array.Empty<Experience>();
        [JsonPropertyName("education")]
        public Education[] Education { get; set; } = Array.Empty<Education>();
        [JsonPropertyName("skills")]
        public Skill[] Skills { get; set; } = Array.Empty<Skill>();

    }
}
