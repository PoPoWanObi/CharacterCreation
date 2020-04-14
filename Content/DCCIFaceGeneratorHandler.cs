using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreation.Content
{
    public interface DCCIFaceGeneratorHandler
    {
        void ChangeToBodyCamera();
        
        void ChangeToEyeCamera();
        
        void ChangeToNoseCamera();
        
        void ChangeToMouthCamera();
        
        void ChangeToFaceCamera();
        
        void ChangeToHairCamera();
        
        void RefreshCharacterEntity();
        
        void MakeVoice(int gender, float pitch);
        
        void SetFacialAnimation(string faceAnimation, bool loop);
        
        void Done();
        
        void Cancel();
        
        void UndressCharacterEntity();
        
        void DressCharacterEntity();
    }
}
