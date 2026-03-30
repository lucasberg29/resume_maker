using DocumentFormat.OpenXml.Presentation;
using ResumeHandlerGUI.Views;
using System.Windows.Controls.Ribbon;

namespace ResumeHandlerGUI.Managers
{
    public class UiManager
    {
        public ResumeRibbon _resumeRibbon = new ResumeRibbon();
        public HeaderRibbon _headerRibbon = new HeaderRibbon();
        public TechnicalSkillsRibbon _technicalSkillsRibbon = new TechnicalSkillsRibbon();
        public ExperienceRibbon _experienceRibbon = new ExperienceRibbon();
        public EducationRibbon _educationRibbon = new EducationRibbon();
        public OtherExperienceRibbon _otherExperienceRibbon = new OtherExperienceRibbon();

        public void Init()
        {

        }

        public void Update()
        {
            _resumeRibbon.UpdateFields();
            _headerRibbon.UpdateFields();
            _technicalSkillsRibbon.UpdateFields();
            _experienceRibbon.UpdateFields();
            _educationRibbon.UpdateFields();
            _otherExperienceRibbon.UpdateFields();
        }

    }
}