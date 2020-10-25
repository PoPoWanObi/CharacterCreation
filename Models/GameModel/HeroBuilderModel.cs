#nullable enable
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.Models
{
    public class HeroBuilderModel : GameModel
    {
        public bool IsInitialized { get; private set; }

        public static Hero? MainHero
        {
            get
            {
                if (Game.Current == null)
                    return null;

                var characterObject = Game.Current.PlayerTroop as CharacterObject;

                return characterObject?.HeroObject;
            }
        }

        public void Initialize()
        {
            var mainHero = MainHero;
            if (mainHero != null)
            {
            }
            IsInitialized = true;
        }
    }
}
