using CharacterCreation.CampaignSystem;
using CharacterCreation.UI;
using HarmonyLib;
using TaleWorlds.Core;

namespace CharacterCreation
{
    public abstract class CharacterCreationEntryPoint
    {
        public Harmony HarmonyInstance { get; } = new Harmony("mod.bannerlord.popowanobi.dcc");

        public abstract void OnBeforeInitialModuleScreenSetAsRoot();

        public abstract void OnGameLoaded(Game game, object initializerObject);

        public abstract void OnGameInitializationFinished(Game game);

        public abstract void OnGameStart(Game game, IGameStarter gameStarterObject);

        public abstract ICharacterEditorImplementation InitializeCharacterEditor(CharacterEditorState state);
    }
}