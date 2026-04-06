using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentHandler.DTO;
using DocumentHandler.DTO.Section;
using DocumentHandler.Interfaces;

namespace DocumentHandler.Handlers
{
    public class DocumentHandler : IDocumentHandler
    {
        public Resume CurrentResume = new Resume();
        public static IErrorHandler ErrorHandler;

        private string DocumentPath = string.Empty;
        private string DataFolderName = "Data";
        private string ResumeFolderName = "Resume";
        public string ResumeFolderPath { get; } = Path.Combine(AppContext.BaseDirectory, "Data", "Resume");

        public string ResumeFileName = "Resume.docx";
        public static string CurrentResumeDataName = "data.json";  

        private void CreateFolder()
        {
            string basePath = AppContext.BaseDirectory;

            string folderPath = Path.Combine(
                basePath,
                DataFolderName,
                ResumeFolderName
            );

            Directory.CreateDirectory(folderPath);
        }

        private void ParseResume()
        {
            JsonHandler.ReadResumeFromJson(ref CurrentResume, CurrentResumeDataName);
        }

        public void InitHandler()
        {
            CreateFolder();
            ErrorHandler = new ErrorHandler();
        }

        public bool SaveResume()
        {
            var socialMendiaLinks = CurrentResume.PersonalInfo.SocialMediaLinks;

            foreach (var socialMediaLink in socialMendiaLinks)
            {
                string result = CopyResumeImage(socialMediaLink.FilePath, ResumeFolderPath);

                if (result != "")
                {
                    socialMediaLink.FilePath = Path.Combine(ResumeFolderPath, socialMediaLink.FileName);
                }
            }

            JsonHandler.WriteResumeToJson(CurrentResume, CurrentResumeDataName);

            XmlHandler.SaveResumeToDocx(CurrentResume, "Resume.docx");
            return true;
        }

        public void LoadResumeFromDocument(string docPath, string safeFileName)
        {
            DocumentPath = docPath;
            ResumeFileName = safeFileName;

            CurrentResumeDataName = safeFileName;

            ParseResume();
        }

        public string GetResumeFileName()
        {
            return ResumeFileName;
        }

        public void AddTechnicalSkill(string skillName, string skillType)
        {


            CurrentResume.PersonalInfo.TechnicalSkills.Add(new TechnicalSkill
            {
                Text = skillName,
                Type = skillType
            });
        }

        public void AddExperience(Experience experience)
        {
            CurrentResume.AllExperiences.Experiences.Add(experience);
        }

        public void AddSocialMediaLink(SocialMediaLink socialMediaLink)
        {
            CurrentResume.PersonalInfo.SocialMediaLinks.Add(socialMediaLink);
        }

        public static string CopyResumeImage(string sourcePath, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            string fileName = System.IO.Path.GetFileName(sourcePath);
            string destinationPath = System.IO.Path.Combine(destinationFolder, fileName);

            while (File.Exists(destinationPath))
            {
                return "";
            }

            File.Copy(sourcePath, destinationPath);

            return System.IO.Path.GetFileName(destinationPath);
        }

        public bool ExportResumeToDOCX(string fileName = "Resume.docx")
        {
            bool result = XmlHandler.ExportResumeToDOCX(fileName, CurrentResume, ResumeFolderPath);
            return result;
        }

        public void AddEducation(Education education)
        {
            CurrentResume.AllEducation.Education.Add(education);
        }

        public void AddOtherExperience(OtherExperience otherExperience)
        {
            CurrentResume.AllOtherExperience.OtherExperience.Add(otherExperience);
        }

        public bool CreateNewResume(string resumeName)
        {
            CurrentResumeDataName = resumeName;
            CurrentResumeDataName = string.Concat(resumeName, ".json");
            ParseResume();
            return true;
        }

        public void SetTechnicalSkillActive(int id, bool isActive)
        {
            for (var i = 0; i < CurrentResume.AllTechnicalSkills.TechnicalSkills.Count; i++)
            {
                if (CurrentResume.AllTechnicalSkills.TechnicalSkills[i].Id == id)
                {
                    CurrentResume.AllTechnicalSkills.TechnicalSkills[i].Active = isActive;
                    break;
                }
            }
        }

        public void SetExperienceActive(string experienceName, bool isActive)
        {
            for (var i = 0; i < CurrentResume.AllExperiences.Experiences.Count; i++)
            {
                if (CurrentResume.AllExperiences.Experiences[i].CompanyName.Text == experienceName)
                {
                    CurrentResume.AllExperiences.Experiences[i].Active = isActive;
                    break;
                }
            }
        }

        public void SetSocialMediaLinkActive(string socialMediaLinkName, bool isActive)
        {
            for (var i = 0; i < CurrentResume.PersonalInfo.SocialMediaLinks.Count; i++)
            {
                if (CurrentResume.PersonalInfo.SocialMediaLinks[i].Name == socialMediaLinkName)
                {
                    CurrentResume.PersonalInfo.SocialMediaLinks[i].Active = isActive;
                    break;
                }
            }
        }

        public bool UpdateTechnicalSkill(TechnicalSkill technicalSkill)
        {
            var technicalSkills = CurrentResume.AllTechnicalSkills.TechnicalSkills;

            for (var i = 0; i < technicalSkills.Count; i++)
            {
                if (technicalSkills[i].Id == technicalSkill.Id)
                {
                    CurrentResume.AllTechnicalSkills.TechnicalSkills[i] = technicalSkill;
                    return true;
                }
            }

            return false;
        }

        public bool UpdateExperience(Experience experience)
        {
            for (var i = 0; i < CurrentResume.AllExperiences.Experiences.Count; i++)
            {
                if (CurrentResume.AllExperiences.Experiences[i].Id == experience.Id)
                {
                    CurrentResume.AllExperiences.Experiences[i] = experience;
                    return true;
                }
            }

            return false;
        }

        public bool UpdateSocialMediaLink(SocialMediaLink socialMediaLink)
        {
            for (var i = 0; i < CurrentResume.PersonalInfo.SocialMediaLinks.Count; i++)
            {
                if (CurrentResume.PersonalInfo.SocialMediaLinks[i].Id == socialMediaLink.Id)
                {
                    CurrentResume.PersonalInfo.SocialMediaLinks[i] = socialMediaLink;
                    return true;
                }
            }

            return false;
        }

        public bool UpdateEducation(Education education)
        {
            for (var i = 0; i < CurrentResume.AllEducation.Education.Count; i++)
            {
                if (CurrentResume.AllEducation.Education[i].Id == education.Id)
                {
                    CurrentResume.AllEducation.Education[i] = education;
                    return true;
                }
            }

            return false;
        }

        public bool UpdateOtherExperience(OtherExperience otherExperience)
        {
            for (var i = 0; i < CurrentResume.AllOtherExperience.OtherExperience.Count; i++)
            {
                if (CurrentResume.AllOtherExperience.OtherExperience[i].Id == otherExperience.Id)
                {
                    CurrentResume.AllOtherExperience.OtherExperience[i] = otherExperience;
                    return true;
                }
            }

            return false;
        }

        public bool DeleteTechnicalSkill(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteExperience(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEducation(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOtherExperience(int id)
        {
            throw new NotImplementedException();
        }

        public void AddTechnicalSkill(TechnicalSkill technicalSkill)
        {
            throw new NotImplementedException();
        }

        public TechnicalSkill GetTechnicalSkillById(int id)
        {
            throw new NotImplementedException();
        }

        public SocialMediaLink GetSocialMediaLinkById(int id)
        {
            throw new NotImplementedException();
        }

        public Experience GetExperienceById(int id)
        {
            throw new NotImplementedException();
        }

        public Education GetEducationById(int id)
        {
            throw new NotImplementedException();
        }

        public OtherExperience GetOtherExperienceById(int id)
        {
            throw new NotImplementedException();
        }

        public void SetExperienceActive(int id, bool isActive)
        {
            throw new NotImplementedException();
        }

        public void SetSocialMediaLinkActive(int id, bool isActive)
        {
            throw new NotImplementedException();
        }

        public void SetEducationActive(int id, bool isActive)
        {
            throw new NotImplementedException();
        }

        public void SetOtherExperienceActive(int id, bool isActive)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSocialMediaLink(int id)
        {
            throw new NotImplementedException();
        }

        public void SetFullName(string fullName)
        {
            throw new NotImplementedException();
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            CurrentResume.PersonalInfo.SetPhoneNumberText(phoneNumber);
        }

        public void SetEmail(string email)
        {
            CurrentResume.PersonalInfo.SetEmailText(email);
        }

        public void SetIntroduction(string introduction)
        {
            CurrentResume.PersonalInfo.SetIntroductionText(introduction);   
        }

        public void SetLocation(string location)
        {
            CurrentResume.PersonalInfo.SetLocationText(location);
        }

        public Element GetFullName()
        {
            throw new NotImplementedException();
        }

        public Element GetPhoneNumber()
        {
            throw new NotImplementedException();
        }

        public Element GetEmail()
        {
            throw new NotImplementedException();
        }

        public Element GetLocation()
        {
            throw new NotImplementedException();
        }

        public Element GetIntroduction()
        {
            return CurrentResume.PersonalInfo.Introduction.Elements.First() ;
        }
    }
}
