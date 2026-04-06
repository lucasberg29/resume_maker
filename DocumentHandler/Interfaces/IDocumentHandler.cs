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

        void AddTechnicalSkill(TechnicalSkill technicalSkill);  
        void AddExperience(Experience experience);
        void AddSocialMediaLink(SocialMediaLink socialMediaLink);
        void AddEducation(Education education);
        void AddOtherExperience(OtherExperience otherExperience);

        TechnicalSkill GetTechnicalSkillById(int id );
        SocialMediaLink GetSocialMediaLinkById(int id);
        Experience GetExperienceById(int id);
        Education GetEducationById(int id);
        OtherExperience GetOtherExperienceById(int id);

        void SetFullName(string fullName);
        void SetPhoneNumber(string phoneNumber);    
        void SetEmail(string email);
        void SetLocation(string location);
        void SetIntroduction(string introduction);

        Element GetFullName();
        Element GetPhoneNumber();
        Element GetEmail();  
        Element GetLocation();
        Element GetIntroduction();

        void SetTechnicalSkillActive(int id, bool isActive);
        void SetExperienceActive(int id, bool isActive);
        void SetSocialMediaLinkActive(int id, bool isActive);
        void SetEducationActive(int id, bool isActive);
        void SetOtherExperienceActive(int id, bool isActive);

        bool UpdateTechnicalSkill(TechnicalSkill technicalSkill);
        bool UpdateExperience(Experience experience);
        bool UpdateSocialMediaLink(SocialMediaLink socialMediaLink);
        bool UpdateEducation(Education education);
        bool UpdateOtherExperience(OtherExperience otherExperience);

        bool DeleteTechnicalSkill(int id);
        bool DeleteExperience(int id);
        bool DeleteSocialMediaLink(int id);
        bool DeleteEducation(int id);
        bool DeleteOtherExperience(int id);
    }
}
