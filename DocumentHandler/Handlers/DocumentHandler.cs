using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentHandler.DTO;
using DocumentHandler.DTO.Attribute;
using DocumentHandler.DTO.Paragraphs;
using DocumentHandler.DTO.Section;
using DocumentHandler.Interfaces;

namespace DocumentHandler.Handlers
{
    public class DocumentHandler : IDocumentHandler
    {
        public Resume CurrentResume = new();
        public static IErrorHandler ErrorHandler { get; set; } = new ErrorHandler();

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
            CurrentResume.AllOtherExperience.OtherExperiences.Add(otherExperience);
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
            for (var i = 0; i < CurrentResume.AllOtherExperience.OtherExperiences.Count; i++)
            {
                if (CurrentResume.AllOtherExperience.OtherExperiences[i].Id == otherExperience.Id)
                {
                    CurrentResume.AllOtherExperience.OtherExperiences[i] = otherExperience;
                    return true;
                }
            }

            return false;
        }

        public bool DeleteTechnicalSkill(int id)
        {
            int numberOfDeleted = CurrentResume.AllTechnicalSkills.TechnicalSkills.RemoveAll(ts => ts.Id == id);

            if (numberOfDeleted == 0)
            {

            }
            else if (numberOfDeleted == 1)
            {

            }
            else
            {
                ErrorHandler.AddError(new Exception($"Multiple technical skills with the same id {id} were deleted."), new System.Diagnostics.StackTrace());
            }

            throw new NotImplementedException();
        }

        public bool DeleteExperience(int id)
        {
            bool deletedSuccessfully = CurrentResume.AllExperiences.DeleteExperience(id);
            return deletedSuccessfully;
        }

        public bool DeleteEducation(int id)
        {
            bool deletedSuccessfully = CurrentResume.AllEducation.DeleteEducation(id);
            return deletedSuccessfully;
        }

        public bool DeleteOtherExperience(int id)
        {
            bool deletedSuccessfully = CurrentResume.AllOtherExperience.DeleteOtherExperience(id);
            return deletedSuccessfully;
        }

        public void AddTechnicalSkill(TechnicalSkill technicalSkill)
        {
            CurrentResume.AllTechnicalSkills.TechnicalSkills.Add(technicalSkill);
        }

        public TechnicalSkill? GetTechnicalSkillById(int id)
        {
            var technical = CurrentResume.AllTechnicalSkills.TechnicalSkills.FirstOrDefault(ts => ts.Id == id);

            if (technical == null)
            {
                ErrorHandler.AddError(new Exception("Technical skill not found!"), new System.Diagnostics.StackTrace());
                return null;
            }

            return technical;
        }

        public SocialMediaLink? GetSocialMediaLinkById(int id)
        {
            var socialMediaLink = CurrentResume.PersonalInfo.SocialMediaLinks.FirstOrDefault(ts => ts.Id == id);

            if (socialMediaLink == null)
            {
                ErrorHandler.AddError(new Exception("social media link not found!"), new System.Diagnostics.StackTrace());
                return null;
            }

            return socialMediaLink;
        }

        public Experience? GetExperienceById(int id)
        {
            var experience = CurrentResume.AllExperiences.Experiences.FirstOrDefault(ts => ts.Id == id);

            if (experience == null)
            {
                ErrorHandler.AddError(new Exception("Experience not found!"), new System.Diagnostics.StackTrace());
                return null;
            }

            return experience;
        }

        public Education? GetEducationById(int id)
        {
            var education = CurrentResume.AllEducation.Education.FirstOrDefault(ts => ts.Id == id);

            if (education == null)
            {
                ErrorHandler.AddError(new Exception("Education not found!"), new System.Diagnostics.StackTrace());
                return null;
            }

            return education;
        }

        public OtherExperience? GetOtherExperienceById(int id)
        {
            var otherExperience = CurrentResume.AllOtherExperience.OtherExperiences.FirstOrDefault(ts => ts.Id == id);

            if (otherExperience == null)
            {
                ErrorHandler.AddError(new Exception("Other experience not found!"), new System.Diagnostics.StackTrace());
                return null;
            }

            return otherExperience;
        }

        public void SetExperienceActive(int id, bool isActive)
        {
            for (var i = 0; i < CurrentResume.AllExperiences.Experiences.Count; i++)
            {
                if (CurrentResume.AllExperiences.Experiences[i].Id == id)
                {
                    CurrentResume.AllExperiences.Experiences[i].Active = isActive;
                    break;
                }
            }
        }

        public void SetSocialMediaLinkActive(int id, bool isActive)
        {
            for (var i = 0; i < CurrentResume.PersonalInfo.SocialMediaLinks.Count; i++)
            {
                if (CurrentResume.PersonalInfo.SocialMediaLinks[i].Id == id)
                {
                    CurrentResume.PersonalInfo.SocialMediaLinks[i].Active = isActive;
                    break;
                }
            }
        }

        public void SetEducationActive(int id, bool isActive)
        {
            for (var i = 0; i < CurrentResume.AllEducation.Education.Count; i++)
            {
                if (CurrentResume.AllEducation.Education[i].Id == id)
                {
                    CurrentResume.AllEducation.Education[i].Active = isActive;
                    break;
                }
            }
        }

        public void SetOtherExperienceActive(int id, bool isActive)
        {
            for (var i = 0; i < CurrentResume.AllOtherExperience.OtherExperiences.Count; i++)
            {
                if (CurrentResume.AllOtherExperience.OtherExperiences[i].Id == id)
                {
                    CurrentResume.AllOtherExperience.OtherExperiences[i].Active = isActive;
                    break;
                }
            }
        }

        public bool DeleteSocialMediaLink(int id)
        {
            int numberOfLinksDeleted = CurrentResume.PersonalInfo.SocialMediaLinks.RemoveAll(sml => sml.Id == id);

            if (numberOfLinksDeleted == 0)
            {
                return false;
            }
            else if (numberOfLinksDeleted == 1)
            {
                return true;
            }
            else
            {
                ErrorHandler.AddError(new Exception($"Multiple social media links with the same id {id} were deleted."), new System.Diagnostics.StackTrace());
                return true;
            }
        }

        public void SetFullName(string fullName)
        {
            CurrentResume.PersonalInfo.SetFullNameText(fullName);   
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

        public Element? GetFullName()
        {
            return CurrentResume.PersonalInfo.FullName.Elements.First();    
        }

        public Element? GetPhoneNumber()
        {
            var phoneNumber = CurrentResume.PersonalInfo.Contact.GetElementByTag("PhoneNumber");

            if (phoneNumber == null)
            {
                ErrorHandler.AddError(new Exception("Phone number element not found."), new System.Diagnostics.StackTrace());
                return null;
            }
            else
            {
                return phoneNumber;
            }
        }

        public Element? GetEmail()
        {
            var email = CurrentResume.PersonalInfo.Contact.GetElementByTag("Email");

            if (email == null)
            {
                ErrorHandler.AddError(new Exception("Email element not found."), new System.Diagnostics.StackTrace());
                return null;
            }
            else
            {
                return email;
            }
        }

        public Element? GetLocation()
        {
            var location = CurrentResume.PersonalInfo.Contact.GetElementByTag("Location");

            if (location == null)
            {
                ErrorHandler.AddError(new Exception("Location element not found."), new System.Diagnostics.StackTrace());
                return null;
            }
            else
            {
                return location;
            }
        }

        public Element? GetIntroduction()
        {
            return CurrentResume.PersonalInfo.Introduction.Elements.First();
        }

        public void SetTechnicalSkillsHeader(string technicalSkillsHeader)
        {
            CurrentResume.AllTechnicalSkills.TechnicalSkillsHeader.Elements.First().Text = technicalSkillsHeader;
        }

        public void SetExperienceHeader(string experienceHeader)
        {
            CurrentResume.AllExperiences.ExperienceHeader.Elements.First().Text = experienceHeader;  
        }

        public void SetEducationHeader(string educationHeader)
        {
            CurrentResume.AllEducation.EducationHeader.Elements.First().Text = educationHeader;  
        }

        public void SetOtherExperienceHeader(string otherExperienceHeader)
        {
            CurrentResume.AllOtherExperience.OtherExperienceHeader.Elements.First().Text = otherExperienceHeader;    
        }

        public Element? GetElementById(int id)
        {
            var element = ElementHandler.GetById(id);
            return element;
        }

        public ResumeParagraph? GetParagraphById(int id)
        {
            var resumeParagraph = ParagraphHandler.GetById(id);
            return resumeParagraph;
        }

        public bool UpdateParagraphStyling(ParagraphStyle paragraphStyle, int id)
        {

            return ParagraphHandler.UpdateParagraphStyling(id, paragraphStyle);
        }

        public bool UpdateElementStyling(ElementStyle elementStyle, int id)
        {
            var element = ElementHandler.GetById(id);

            if (element != null)
            {
                element.ElementStyle = elementStyle;
                return true;
            }
            return false;
        }

        public void UpdateParagraph(ResumeParagraph paragraph)
        {
            var updatedParagraph = ParagraphHandler.GetById(paragraph.Id);
            updatedParagraph = paragraph;
        }

        public void UpdateElement(Element element)
        {
            var updatedElement = ElementHandler.GetById(element.Id);
            updatedElement = element;
        }   

        public PersonalInfo GetPersonalInfo()
        {
            return CurrentResume.PersonalInfo;
        }

        public AllTechnicalSkills GetAllTechnicalSkills()
        {
            return CurrentResume.AllTechnicalSkills;
        }

        public AllExperiences GetAllExperience()
        {
            return CurrentResume.AllExperiences;
        }

        public AllEducation GetAllEducation()
        {
            return CurrentResume.AllEducation;  
        }

        public AllOtherExperience GetAllOtherExperience()
        {
            return CurrentResume.AllOtherExperience;
        }
    }
}
