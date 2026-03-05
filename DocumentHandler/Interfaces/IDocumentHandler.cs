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

        TechnicalSkill GetTechnicalSkillByName(string technicalSkillName);
        SocialMediaLink GetSocialMediaLinkByName(string socialMediaLinkName);
        Experience GetExperienceByName(string experienceName);
        Education GetEducationByName(string educationName);
        OtherExperience GetOtherExperienceByName(string otherExperienceName);

        void SetTechnicalSkillActive(string technicalSkillName, bool isActive);
        void SetExperienceActive(string experienceName, bool isActive);
        void SetSocialMediaLinkActive(string socialMediaLinkName, bool isActive);
        void SetEducationActive(string educationName, bool isActive);
        void SetOtherExperienceActive(string otherExperienceName, bool isActive);

        bool UpdateTechnicalSkill(TechnicalSkill technicalSkill);
        bool UpdateExperience(Experience experience);
        bool UpdateSocialMediaLink(SocialMediaLink socialMediaLink);
        bool UpdateEducation(Education education);
        bool UpdateOtherExperience(OtherExperience otherExperience);
    }
}
