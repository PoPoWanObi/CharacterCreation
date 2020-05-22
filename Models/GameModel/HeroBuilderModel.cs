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

                CharacterObject? characterObject = Game.Current.PlayerTroop as CharacterObject;

                if (characterObject == null)
                    return null;

                return characterObject.HeroObject;
            }
        }

        public void Initialize()
        {
            Hero? mainHero = MainHero;
            if (mainHero != null)
            {
            }
            this.IsInitialized = true;
        }
    }
}
