using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace CharacterCreation.Models
{
    public class AgeModel : DefaultAgeModel
    {
        public override int BecomeInfantAge => DCCSettings.Instance != null ? DCCSettings.Instance.BecomeInfantAge : base.BecomeInfantAge;
        public override int BecomeChildAge => DCCSettings.Instance != null ? DCCSettings.Instance.BecomeChildAge : base.BecomeChildAge;
        public override int BecomeTeenagerAge => DCCSettings.Instance != null ? DCCSettings.Instance.BecomeTeenagerAge : base.BecomeTeenagerAge;
        public override int HeroComesOfAge => DCCSettings.Instance != null ? DCCSettings.Instance.BecomeAdultAge : base.HeroComesOfAge;
        public override int BecomeOldAge => DCCSettings.Instance != null ? DCCSettings.Instance.BecomeOldAge : base.BecomeOldAge;
        public override int MaxAge => DCCSettings.Instance != null ? DCCSettings.Instance.MaxAge : base.MaxAge;
    }
}
