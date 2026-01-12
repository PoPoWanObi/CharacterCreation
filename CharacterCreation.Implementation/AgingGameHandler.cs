using CharacterCreation.Implementation.Util;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.Implementation;

public class AgingGameHandler : GameHandler
{
        
    public override void OnAfterSave()
    {
    }

    public override void OnBeforeSave()
    {
        if (Game.Current == null || !(Game.Current.GameType is Campaign)) return;
        SettingsEffects.Instance?.UpdateAllHeroes(Game.Current, true);
    }
}