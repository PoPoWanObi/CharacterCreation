using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace CharacterCreation.Models
{
    public class AgeModel : DefaultAgeModel
    {
        public override int BecomeInfantAge
        {
            get
            {
                return Settings.Instance.BecomeInfantAge;
            }
        }
        public override int BecomeChildAge
        {
            get
            {
                return Settings.Instance.BecomeChildAge;
            }
        }

        public override int BecomeTeenagerAge
        {
            get
            {
                return Settings.Instance.BecomeTeenagerAge;
            }
        }

        public override int HeroComesOfAge
        {
            get
            {
                return Settings.Instance.BecomeAdultAge;
            }
        }

        public override int BecomeOldAge
        {
            get
            {
                return Settings.Instance.BecomeOldAge;
            }
        }

        public override int MaxAge
        {
            get
            {
                return Settings.Instance.MaxAge;

            }
        }
    }
}
