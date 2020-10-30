using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace CharacterCreation.Models
{
    public class AgeModel : DefaultAgeModel
    {
        public override int BecomeInfantAge => DCCSettingsUtil.Instance.BecomeInfantAge;

        public override int BecomeChildAge => DCCSettingsUtil.Instance.BecomeChildAge;

        public override int BecomeTeenagerAge => DCCSettingsUtil.Instance.BecomeTeenagerAge;

        public override int HeroComesOfAge => DCCSettingsUtil.Instance.BecomeAdultAge;

        public override int BecomeOldAge => DCCSettingsUtil.Instance.BecomeOldAge;

        public override int MaxAge => DCCSettingsUtil.Instance.MaxAge;
    }
}
