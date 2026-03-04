using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentHandler.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHandler.Interfaces
{
    public interface IDocumentHandler
    {
        void InitHandler();

        void LoadResumeFromDocument(string docPath, string fileName);

        string GetResumeFileName();

        public bool SaveResume();

        public bool CreateNewResume(string resumeName);

        public bool ExportResumeToDOCX(string fileName);

        void AddTechnicalSkill(string skillName, string skillType);
        void AddExperience(Experience experience);
        void AddSocialMediaLink(SocialMediaLink socialMediaLink);
        void AddEducation(Education education);
        void AddOtherExperience(OtherExperience otherExperience);

        void SetTechnicalSkillActive(string technicalSkillName, bool isActive);
        void SetExperienceActive(string experienceName, bool isActive);
        void SetSocialMediaLinkActive(string socialMediaLinkName, bool isActive);
        void SetEducationActive(string educationName, bool isActive);
        void SetOtherExperienceActive(string otherExperienceName, bool isActive);

    }
}
