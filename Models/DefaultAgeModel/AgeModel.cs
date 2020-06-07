using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace CharacterCreation.Models
{
    public class AgeModel : DefaultAgeModel
    {
        public override int BecomeInfantAge
        {
            get
            {
                return DCCSettings.Instance != null ? DCCSettings.Instance.BecomeInfantAge : base.BecomeInfantAge;
            }
        }
        public override int BecomeChildAge
        {
            get
            {
                return DCCSettings.Instance != null ? DCCSettings.Instance.BecomeChildAge : base.BecomeChildAge;
            }
        }

        public override int BecomeTeenagerAge
        {
            get
            {
                return DCCSettings.Instance != null ? DCCSettings.Instance.BecomeTeenagerAge : base.BecomeTeenagerAge;
            }
        }

        public override int HeroComesOfAge
        {
            get
            {
                return DCCSettings.Instance != null ? DCCSettings.Instance.BecomeAdultAge : base.HeroComesOfAge;
            }
        }

        public override int BecomeOldAge
        {
            get
            {
                return DCCSettings.Instance != null ? DCCSettings.Instance.BecomeOldAge : base.BecomeOldAge;
            }
        }

        public override int MaxAge
        {
            get
            {
                return DCCSettings.Instance != null ? DCCSettings.Instance.MaxAge : base.MaxAge;

            }
        }
    }
}
