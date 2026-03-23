using ResumeHandlerGUI.Views;

namespace ResumeHandlerGUI
{
    public class UiManager
    {
        private ResumeRibbon _resumeRibbon = new ResumeRibbon();
        private HeaderRibbon _headerRibbon = new HeaderRibbon();
        private TechnicalSkillsRibbon _technicalSkillsRibbon = new TechnicalSkillsRibbon();
        private ExperienceRibbon _experienceRibbon = new ExperienceRibbon();
        private EducationRibbon _educationRibbon = new EducationRibbon();
        private OtherExperienceRibbon _otherExperienceRibbon = new OtherExperienceRibbon();

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