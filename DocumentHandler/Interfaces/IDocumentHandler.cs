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

        //string CreateSampleDocument(string fileName);

        void LoadResumeFromDocument(string docPath, string fileName);

        string GetResumeFileName();

        public bool SaveResume();

        void AddTechnicalSkill(string skillName, string skillType);
        void AddExperience(Experience experience);
        void AddSocialMediaLink(SocialMediaLink socialMediaLink);
        void AddEducation();
        void AddSkill();
    }
}
