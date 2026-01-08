using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace CharacterCreation.CampaignSystem
{
    public sealed class FaceGeneratorFilter : IFaceGeneratorCustomFilter
    {
        private readonly int[] _haircutIndices;
        private readonly int[] _facialHairIndices;

        public FaceGeneratorFilter(BasicCharacterObject character)
        {
            var haircutIndices = new List<int>();
            var facialHairIndices = new List<int>();

            foreach (var objectType in MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
            {
                haircutIndices.AddRange(
                    Campaign.Current.Models.BodyPropertiesModel.GetHairIndicesForCulture(character.Race,
                        character.Race, character.Age, objectType));
                facialHairIndices.AddRange(
                    Campaign.Current.Models.BodyPropertiesModel.GetBeardIndicesForCulture(character.Race,
                        character.Race, character.Age, objectType));
            }
            
            _haircutIndices = haircutIndices.Distinct().ToArray();
            _facialHairIndices = facialHairIndices.Distinct().ToArray();
        }
        
        public int[] GetHaircutIndices(BasicCharacterObject character) => _haircutIndices;
        
        public int[] GetFacialHairIndices(BasicCharacterObject character) => _facialHairIndices;

        public FaceGeneratorStage[] GetAvailableStages()
        {
            return new[]
            {
                FaceGeneratorStage.Body,
                FaceGeneratorStage.Face,
                FaceGeneratorStage.Eyes,
                FaceGeneratorStage.Nose,
                FaceGeneratorStage.Mouth,
                FaceGeneratorStage.Hair,
                FaceGeneratorStage.Taint
            };
        }
    }
}