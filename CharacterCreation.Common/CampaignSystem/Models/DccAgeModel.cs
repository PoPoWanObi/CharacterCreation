using CharacterCreation.Common.Settings;
using TaleWorlds.CampaignSystem.GameComponents;

namespace CharacterCreation.Common.CampaignSystem.Models
{
    public class DccAgeModel : DefaultAgeModel
    {
        public override int BecomeInfantAge => DccSettings.Instance!.BecomeInfantAge;

        public override int BecomeChildAge => DccSettings.Instance!.BecomeChildAge;

        public override int BecomeTeenagerAge => DccSettings.Instance!.BecomeTeenagerAge;

        public override int HeroComesOfAge => DccSettings.Instance!.BecomeAdultAge;

        public override int BecomeOldAge => DccSettings.Instance!.BecomeOldAge;

        public override int MaxAge => DccSettings.Instance!.MaxAge;
    }
}
