using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace CharacterCreation.Models
{
    public class AgeModel : DefaultAgeModel
    {
        public override int BecomeInfantAge
        {
            get
            {
                return Settings.Instance != null ? Settings.Instance.BecomeInfantAge : base.BecomeInfantAge;
            }
        }
        public override int BecomeChildAge
        {
            get
            {
                return Settings.Instance != null ? Settings.Instance.BecomeChildAge : base.BecomeChildAge;
            }
        }

        public override int BecomeTeenagerAge
        {
            get
            {
                return Settings.Instance != null ? Settings.Instance.BecomeTeenagerAge : base.BecomeTeenagerAge;
            }
        }

        public override int HeroComesOfAge
        {
            get
            {
                return Settings.Instance != null ? Settings.Instance.BecomeAdultAge : base.HeroComesOfAge;
            }
        }

        public override int BecomeOldAge
        {
            get
            {
                return Settings.Instance != null ? Settings.Instance.BecomeOldAge : base.BecomeOldAge;
            }
        }

        public override int MaxAge
        {
            get
            {
                return Settings.Instance != null ? Settings.Instance.MaxAge : base.MaxAge;

            }
        }
    }
}
