using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.MountAndBlade.LegacyGUI;
using TaleWorlds.MountAndBlade.LegacyGUI.Missions.Multiplayer;
using TaleWorlds.MountAndBlade.LegacyGUI.Missions.Order;
using TaleWorlds.MountAndBlade.LegacyGUI.Missions.Singleplayer;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.MountAndBlade.View.Missions.Multiplayer;
using TaleWorlds.MountAndBlade.View.Missions.Singleplayer;
using TaleWorlds.MountAndBlade.View.Screen;
using TaleWorlds.MountAndBlade.ViewModelCollection;

namespace CharacterCreation.Content
{
    public static class DCCViewCreator
    {
        public static ScreenBase DCCCreateMBFaceGeneratorScreen(BasicCharacterObject character, bool openedFromMultiplayer = false)
        {
            return ViewCreatorManager.CreateScreenView<MBFaceGeneratorScreen>(new object[]
            {
                character,
                openedFromMultiplayer
            });
        }
    }
}
