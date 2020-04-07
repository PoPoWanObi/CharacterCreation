using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace CharacterCreation.Models
{
    public class HeroBuilderViewModel : ViewModel
    {
        public void SetHero(Hero hero)
        {
            this.selectedHero = hero;
        }

        public HeroBuilderViewModel(HeroBuilderModel heroModel, Action<Hero> editCallback)
        {
            this.heroModel = heroModel;
            this.editCallback = editCallback;
        }

        public void ExecuteEdit()
        {
            if (this.selectedHero == null)
            {
                return;
            }
            this.Edit(this.selectedHero);
            Action<Hero> action = this.editCallback;
            if (action == null)
            {
                return;
            }
            action(this.selectedHero);
        }

        public void Edit(Hero hero)
        {

        }


        private HeroBuilderModel heroModel;
        private Hero selectedHero;
        private Action<Hero> editCallback;
    }
}
