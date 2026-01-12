using System.Collections.Generic;
using CharacterCreation.Common.CampaignSystem;
using CharacterCreation.Common.CampaignSystem.Models;
using CharacterCreation.Common.Settings;
using CharacterCreation.Common.Util;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using static CharacterCreation.Common.Util.DccLocalization;

namespace CharacterCreation.Common;

public class SubModule : MBSubModuleBase
{
    public static string GetFormattedAgeDebugMessage(Hero hero, float expectedAge)
    {
        var attributes = new Dictionary<string, object>
        {
            ["HERO_NAME"] = hero.Name,
            ["AGE1"] = expectedAge,
            ["AGE2"] = hero.Age
        };
        return new TextObject(ExpectedActualAgeMessage, attributes).ToString();
    }

    //Registers before the first module appears (main menu)
    protected override void OnBeforeInitialModuleScreenSetAsRoot()
    {
        base.OnBeforeInitialModuleScreenSetAsRoot();
    }

    // Called when loading save game
    public override void OnGameLoaded(Game game, object initializerObject)
    {
        if (game.GameType is not Campaign || !DccSettings.Instance!.DebugMode) return;

        var player = Hero.MainHero;
        if (player is null) return;
        InformationManager.DisplayMessage(
            new InformationMessage(GetFormattedAgeDebugMessage(player, player.Age),
                ColorManager.Red));
        Debug.Print(GetFormattedAgeDebugMessage(player, player.Age));
    }

    protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
    {
        if (!(game.GameType is Campaign) || !(gameStarterObject is CampaignGameStarter gameStarter))
            return;

        // add behaviors
        gameStarter.AddBehavior(new CharacterCreationCampaignBehavior());

        // add game models
        if (DccSettings.Instance!.CustomAgeModel)
            gameStarter.AddModel(new DccAgeModel());
    }
}