using CharacterCreation.Content;
using CharacterCreation.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using DCCBodyProperties = CharacterCreation.Content.DCCBodyProperties;

namespace CharacterCreation.Models
{
    public class DCCFaceGenVM : ViewModel
    {
        public void SetDCCFaceGenerationParams(DCCFaceGenerationParams DCCFaceGenerationParams)
        {
            this._DCCFaceGenerationParams = DCCFaceGenerationParams;
        }
        
        public DCCFaceGenVM(DCCBodyGenerator DCCBodyGenerator, DCCIFaceGeneratorHandler faceGeneratorScreen, Action<float> onHeightChanged, Action onAgeChanged, TextObject affirmitiveText, TextObject negativeText, int currentStageIndex, int totalStagesCount, int furthestIndex, Action<int> goToIndex, bool canChangeGender, bool openedFromMultiplayer)
        {
            this._DCCBodyGenerator = DCCBodyGenerator;
            this._faceGeneratorScreen = faceGeneratorScreen;
            this._showDebugValues = FaceGen.ShowDebugValues;
            this._affirmitiveText = affirmitiveText;
            this._negativeText = negativeText;
            this._openedFromMultiplayer = openedFromMultiplayer;
            this.CanChangeGender = (canChangeGender || this._showDebugValues);
            this._onHeightChanged = onHeightChanged;
            this._onAgeChanged = onAgeChanged;
            this._goToIndex = goToIndex;
            this.TotalStageCount = totalStagesCount;
            this.CurrentStageIndex = currentStageIndex;
            this.FurthestIndex = furthestIndex;
            this.BodyProperties = new MBBindingList<DCCFaceGenPropertyVM>();
            this.FaceProperties = new MBBindingList<DCCFaceGenPropertyVM>();
            this.EyesProperties = new MBBindingList<DCCFaceGenPropertyVM>();
            this.NoseProperties = new MBBindingList<DCCFaceGenPropertyVM>();
            this.MouthProperties = new MBBindingList<DCCFaceGenPropertyVM>();
            this.HairProperties = new MBBindingList<DCCFaceGenPropertyVM>();
            this.TaintProperties = new MBBindingList<DCCFaceGenPropertyVM>();
            this._tabProperties = new Dictionary<DCCFaceGenVM.FaceGenTabs, MBBindingList<DCCFaceGenPropertyVM>>
            {
                {
                    DCCFaceGenVM.FaceGenTabs.Body,
                    this.BodyProperties
                },
                {
                    DCCFaceGenVM.FaceGenTabs.Face,
                    this.FaceProperties
                },
                {
                    DCCFaceGenVM.FaceGenTabs.Eyes,
                    this.EyesProperties
                },
                {
                    DCCFaceGenVM.FaceGenTabs.Nose,
                    this.NoseProperties
                },
                {
                    DCCFaceGenVM.FaceGenTabs.Mouth,
                    this.MouthProperties
                },
                {
                    DCCFaceGenVM.FaceGenTabs.Hair,
                    this.HairProperties
                },
                {
                    DCCFaceGenVM.FaceGenTabs.Taint,
                    this.TaintProperties
                }
            };
            this.TaintTypes = new MBBindingList<DCCFacegenListItemVM>();
            this.BeardTypes = new MBBindingList<DCCFacegenListItemVM>();
            this.HairTypes = new MBBindingList<DCCFacegenListItemVM>();
            this._tab = -1;
            this.IsDressed = false;
            this._undoCommands = new List<KeyValuePair<int, BodyProperties>>(100);
            this._redoCommands = new List<KeyValuePair<int, BodyProperties>>(100);
            this.RefreshValues();
        }
        
        public override void RefreshValues()
        {
            base.RefreshValues();
            this.BodyHint = new HintViewModel(GameTexts.FindText("str_body", null).ToString(), null);
            this.FaceHint = new HintViewModel(GameTexts.FindText("str_face", null).ToString(), null);
            this.EyesHint = new HintViewModel(GameTexts.FindText("str_eyes", null).ToString(), null);
            this.NoseHint = new HintViewModel(GameTexts.FindText("str_nose", null).ToString(), null);
            this.HairHint = new HintViewModel(GameTexts.FindText("str_hair", null).ToString(), null);
            this.TaintHint = new HintViewModel(GameTexts.FindText("str_face_gen_markings", null).ToString(), null);
            this.MouthHint = new HintViewModel(GameTexts.FindText("str_mouth", null).ToString(), null);
            this.RedoHint = new HintViewModel(GameTexts.FindText("str_redo", null).ToString(), null);
            this.UndoHint = new HintViewModel(GameTexts.FindText("str_undo", null).ToString(), null);
            this.RandomizeHint = new HintViewModel(GameTexts.FindText("str_randomize", null).ToString(), null);
            this.RandomizeAllHint = new HintViewModel(GameTexts.FindText("str_randomize_all", null).ToString(), null);
            this.ResetHint = new HintViewModel(GameTexts.FindText("str_reset", null).ToString(), null);
            this.ResetAllHint = new HintViewModel(GameTexts.FindText("str_reset_all", null).ToString(), null);
            this.ClothHint = new HintViewModel(GameTexts.FindText("str_face_gen_change_cloth", null).ToString(), null);
            this.FlipHairLbl = new TextObject("{=74PKmRWJ}Flip Hair", null).ToString();
            this.SkinColorLbl = GameTexts.FindText("sf_facegen_skin_color", null).ToString();
            this.GenderLbl = GameTexts.FindText("sf_facegen_gender", null).ToString();
            this.Title = GameTexts.FindText("sf_facegen_title", null).ToString();
            this.DoneBtnLbl = this._affirmitiveText.ToString();
            this.CancelBtnLbl = this._negativeText.ToString();
            DCCFacegenListItemVM selectedTaintType = this._selectedTaintType;
            if (selectedTaintType != null)
            {
                selectedTaintType.RefreshValues();
            }
            DCCFacegenListItemVM selectedBeardType = this._selectedBeardType;
            if (selectedBeardType != null)
            {
                selectedBeardType.RefreshValues();
            }
            DCCFacegenListItemVM selectedHairType = this._selectedHairType;
            if (selectedHairType != null)
            {
                selectedHairType.RefreshValues();
            }
            this._bodyProperties.ApplyActionOnAllItems(delegate (DCCFaceGenPropertyVM x)
            {
                x.RefreshValues();
            });
            this._faceProperties.ApplyActionOnAllItems(delegate (DCCFaceGenPropertyVM x)
            {
                x.RefreshValues();
            });
            this._eyesProperties.ApplyActionOnAllItems(delegate (DCCFaceGenPropertyVM x)
            {
                x.RefreshValues();
            });
            this._noseProperties.ApplyActionOnAllItems(delegate (DCCFaceGenPropertyVM x)
            {
                x.RefreshValues();
            });
            this._mouthProperties.ApplyActionOnAllItems(delegate (DCCFaceGenPropertyVM x)
            {
                x.RefreshValues();
            });
            this._hairProperties.ApplyActionOnAllItems(delegate (DCCFaceGenPropertyVM x)
            {
                x.RefreshValues();
            });
            this._taintProperties.ApplyActionOnAllItems(delegate (DCCFaceGenPropertyVM x)
            {
                x.RefreshValues();
            });
            this._taintTypes.ApplyActionOnAllItems(delegate (DCCFacegenListItemVM x)
            {
                x.RefreshValues();
            });
            this._beardTypes.ApplyActionOnAllItems(delegate (DCCFacegenListItemVM x)
            {
                x.RefreshValues();
            });
            this._hairTypes.ApplyActionOnAllItems(delegate (DCCFacegenListItemVM x)
            {
                x.RefreshValues();
            });
            DCCFaceGenPropertyVM soundPreset = this._soundPreset;
            if (soundPreset != null)
            {
                soundPreset.RefreshValues();
            }
            DCCFaceGenPropertyVM faceTypes = this._faceTypes;
            if (faceTypes != null)
            {
                faceTypes.RefreshValues();
            }
            DCCFaceGenPropertyVM teethTypes = this._teethTypes;
            if (teethTypes != null)
            {
                teethTypes.RefreshValues();
            }
            DCCFaceGenPropertyVM eyebrowTypes = this._eyebrowTypes;
            if (eyebrowTypes != null)
            {
                eyebrowTypes.RefreshValues();
            }
            SelectorVM<SelectorItemVM> skinColorSelector = this._skinColorSelector;
            if (skinColorSelector != null)
            {
                skinColorSelector.RefreshValues();
            }
            SelectorVM<SelectorItemVM> hairColorSelector = this._hairColorSelector;
            if (hairColorSelector != null)
            {
                hairColorSelector.RefreshValues();
            }
            SelectorVM<SelectorItemVM> tattooColorSelector = this._tattooColorSelector;
            if (tattooColorSelector == null)
            {
                return;
            }
            tattooColorSelector.RefreshValues();
        }
        
        private void SetColorCodes()
        {
            this._skinColors = DCCBodyProperties.GetSkinColorGradientPoints(this.SelectedGender, (int)this._DCCBodyGenerator.Character.Age);
            this._hairColors = DCCBodyProperties.GetHairColorGradientPoints(this.SelectedGender, (int)this._DCCBodyGenerator.Character.Age);
            this._tattooColors = DCCBodyProperties.GetTatooColorGradientPoints(this.SelectedGender, (int)this._DCCBodyGenerator.Character.Age);
            this.SkinColorSelector = new SelectorVM<SelectorItemVM>(this._skinColors.Select(delegate (uint t)
            {
                t %= 4278190080U;
                return "#" + Convert.ToString((long)((ulong)t), 16).PadLeft(6, '0').ToUpper() + "FF";
            }).ToList<string>(), (int)Math.Round((double)this._DCCFaceGenerationParams._curSkinColorOffset * (double)(this._skinColors.Count - 1)), new Action<SelectorVM<SelectorItemVM>>(this.OnSelectSkinColor));
            this.HairColorSelector = new SelectorVM<SelectorItemVM>(this._hairColors.Select(delegate (uint t)
            {
                t %= 4278190080U;
                return "#" + Convert.ToString((long)((ulong)t), 16).PadLeft(6, '0').ToUpper() + "FF";
            }).ToList<string>(), (int)Math.Round((double)this._DCCFaceGenerationParams._curHairColorOffset * (double)(this._hairColors.Count - 1)), new Action<SelectorVM<SelectorItemVM>>(this.OnSelectHairColor));
            this.TattooColorSelector = new SelectorVM<SelectorItemVM>(this._tattooColors.Select(delegate (uint t)
            {
                t %= 4278190080U;
                return "#" + Convert.ToString((long)((ulong)t), 16).PadLeft(6, '0').ToUpper() + "FF";
            }).ToList<string>(), (int)Math.Round((double)this._DCCFaceGenerationParams._curFaceTattooColorOffset1 * (double)(this._tattooColors.Count - 1)), new Action<SelectorVM<SelectorItemVM>>(this.OnSelectTattooColor));
        }
        
        private void OnSelectSkinColor(SelectorVM<SelectorItemVM> s)
        {
            this.AddCommand();
            this._DCCFaceGenerationParams._curSkinColorOffset = (float)s.SelectedIndex / (float)(this._skinColors.Count - 1);
            this.UpdateFace();
        }
        
        private void OnSelectTattooColor(SelectorVM<SelectorItemVM> s)
        {
            this.AddCommand();
            this._DCCFaceGenerationParams._curFaceTattooColorOffset1 = (float)s.SelectedIndex / (float)(this._tattooColors.Count - 1);
            this.UpdateFace();
        }
        
        private void OnSelectHairColor(SelectorVM<SelectorItemVM> s)
        {
            this.AddCommand();
            this._DCCFaceGenerationParams._curHairColorOffset = (float)s.SelectedIndex / (float)(this._hairColors.Count - 1);
            this.UpdateFace();
        }
        
        private void OnHeightChanged()
        {
            Action<float> onHeightChanged = this._onHeightChanged;
            if (onHeightChanged == null)
            {
                return;
            }
            DCCFaceGenPropertyVM heightSlider = this._heightSlider;
            onHeightChanged((heightSlider != null) ? heightSlider.Value : 0f);
        }
        
        private void OnAgeChanged()
        {
            Action onAgeChanged = this._onAgeChanged;
            if (onAgeChanged == null)
            {
                return;
            }
            onAgeChanged();
        }
        
        private void OnTabClicked(int index)
        {
            this.Tab = index;
        }
        
        public void Refresh()
        {
            if (!this._characterRefreshEnabled)
            {
                return;
            }
            this._characterRefreshEnabled = false;
            base.OnPropertyChanged("FlipHairCb");
            this._selectedGender = this._DCCFaceGenerationParams._currentGender;
            this.SetColorCodes();
            int num = 0;
            DCCBodyProperties.GetParamsMax(this.SelectedGender, (int)this._DCCFaceGenerationParams._curAge, ref num, ref this.beardNum, ref this.faceTextureNum, ref this.mouthTextureNum, ref this.faceTattooNum, ref this._newSoundPresetSize, ref this.eyebrowTextureNum, ref this._scale);
            this.HairNum = num;
            this._isVoiceTypeUsableForOnlyNpc = DCCBodyProperties.GetVoiceTypeUsableForPlayerData(this.SelectedGender, this._DCCBodyGenerator.Character.Age, this._newSoundPresetSize);
            DCCBodyProperties.GetZeroProbabilities(this.SelectedGender, this._DCCFaceGenerationParams._curAge, ref this._DCCFaceGenerationParams._tattooZeroProbability);
            foreach (KeyValuePair<DCCFaceGenVM.FaceGenTabs, MBBindingList<DCCFaceGenPropertyVM>> keyValuePair in this._tabProperties)
            {
                keyValuePair.Value.Clear();
            }
            int faceGenInstancesLength = DCCBodyProperties.GetFaceGenInstancesLength(this._DCCFaceGenerationParams._currentGender, (int)this._DCCFaceGenerationParams._curAge);
            int keyTimePoint = 0;
            int keyTimePoint2 = 0;
            int keyTimePoint3 = 0;
            int keyTimePoint4 = 0;
            DCCFaceGenPropertyVM item;
            for (int i = 0; i < faceGenInstancesLength; i++)
            {
                DCCDeformKeyData deformKeyData = DCCBodyProperties.GetDCCDeformKeyData(i, this._DCCFaceGenerationParams._currentGender, (int)this._DCCFaceGenerationParams._curAge);
                TextObject textObject = new TextObject("{=bsiRNJtk}{NAME}:", null);
                textObject.SetTextVariable("NAME", GameTexts.FindText("str_facegen_skin", deformKeyData.Id));
                GameTexts.FindText("str_facegen_skin", deformKeyData.Id).ToString().Contains("exist");
                if (deformKeyData.Id == "weight")
                {
                    keyTimePoint2 = deformKeyData.KeyTimePoint;
                }
                else if (deformKeyData.Id == "build")
                {
                    keyTimePoint4 = deformKeyData.KeyTimePoint;
                }
                else if (deformKeyData.Id == "height")
                {
                    keyTimePoint = deformKeyData.KeyTimePoint;
                }
                else if (deformKeyData.Id == "age")
                {
                    keyTimePoint3 = deformKeyData.KeyTimePoint;
                }
                else
                {
                    item = new DCCFaceGenPropertyVM(i, 0.0, 1.0, textObject, deformKeyData.KeyTimePoint, deformKeyData.GroupId, (double)this._DCCFaceGenerationParams.KeyWeights[i], new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
                    if (deformKeyData.GroupId > -1 && deformKeyData.GroupId < 7)
                    {
                        this._tabProperties[(DCCFaceGenVM.FaceGenTabs)deformKeyData.GroupId].Add(item);
                    }
                }
            }
            if (this._tab == -1)
            {
                this.Tab = 0;
            }
            item = new DCCFaceGenPropertyVM(-19, 0.0, 1.0, new TextObject("{=G6hYIR5k}Voice Pitch:", null), -19, 0, (double)this._DCCFaceGenerationParams._voicePitch, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
            this._tabProperties[DCCFaceGenVM.FaceGenTabs.Body].Add(item);
            this._heightSlider = new DCCFaceGenPropertyVM(-16, (double)(this._openedFromMultiplayer ? 0.25f : 0f), (double)(this._openedFromMultiplayer ? 0.75f : 1f), new TextObject("{=cLJdeUWz}Height:", null), keyTimePoint, 0, (double)((this._heightSlider == null) ? this._DCCFaceGenerationParams._heightMultiplier : this._heightSlider.Value), new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
            this._tabProperties[DCCFaceGenVM.FaceGenTabs.Body].Add(this._heightSlider);
            if (this._openedFromMultiplayer || this._showDebugValues)
            {
                double min = (double)(this._openedFromMultiplayer ? 25 : 3);
                item = new DCCFaceGenPropertyVM(-11, min, 128.0, new TextObject("{=H1emUb6k}Age:", null), keyTimePoint3, 0, (double)this._DCCFaceGenerationParams._curAge, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
                this._tabProperties[DCCFaceGenVM.FaceGenTabs.Body].Add(item);
            }
            if (this._showDebugValues)
            {
                item = new DCCFaceGenPropertyVM(-17, 0.0, 1.0, new TextObject("{=zBld61ck}Weight:", null), keyTimePoint2, 0, (double)this._DCCFaceGenerationParams._curWeight, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
                this._tabProperties[DCCFaceGenVM.FaceGenTabs.Body].Add(item);
                item = new DCCFaceGenPropertyVM(-18, 0.0, 1.0, new TextObject("{=EUAKPHek}Build:", null), keyTimePoint4, 0, (double)this._DCCFaceGenerationParams._curBuild, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
                this._tabProperties[DCCFaceGenVM.FaceGenTabs.Body].Add(item);
            }
            item = new DCCFaceGenPropertyVM(-12, 0.0, 1.0, new TextObject("{=qXxpITdc}Eye Color:", null), -12, 2, (double)this._DCCFaceGenerationParams._curEyeColorOffset, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
            this._tabProperties[DCCFaceGenVM.FaceGenTabs.Eyes].Add(item);
            this.UpdateGenderBasedResources();
            this._initialSelectedTaintType = this._DCCFaceGenerationParams._curFaceTattoo;
            this._initialSelectedBeardType = this._DCCFaceGenerationParams._curBeard;
            this._initialSelectedHairType = this._DCCFaceGenerationParams._currentHair;
            this._initialSelectedHairColor = this._DCCFaceGenerationParams._curHairColorOffset;
            this._initialSelectedSkinColor = this._DCCFaceGenerationParams._curSkinColorOffset;
            this._initialSelectedTaintColor = this._DCCFaceGenerationParams._curFaceTattooColorOffset1;
            this._characterRefreshEnabled = true;
            this.UpdateFace();
        }
        
        private void UpdateGenderBasedResources()
        {
            int num = 0;
            DCCBodyProperties.GetParamsMax(this.SelectedGender, (int)this._DCCFaceGenerationParams._curAge, ref num, ref this.beardNum, ref this.faceTextureNum, ref this.mouthTextureNum, ref this.faceTattooNum, ref this._newSoundPresetSize, ref this.eyebrowTextureNum, ref this._scale);
            this.HairNum = num;
            this._isVoiceTypeUsableForOnlyNpc = DCCBodyProperties.GetVoiceTypeUsableForPlayerData(this.SelectedGender, this._DCCBodyGenerator.Character.Age, this._newSoundPresetSize);
            this.BeardTypes.Clear();
            for (int i = 0; i < this.beardNum; i++)
            {
                DCCFacegenListItemVM item = new DCCFacegenListItemVM("FaceGen\\Beard\\img" + i, i, new Action<DCCFacegenListItemVM, bool>(this.SetSelectedBeardType));
                this.BeardTypes.Add(item);
            }
            string text = (this._selectedGender == 1) ? "Female" : "Male";
            this.HairTypes.Clear();
            for (int j = 0; j < num; j++)
            {
                DCCFacegenListItemVM item2 = new DCCFacegenListItemVM(string.Concat(new object[]
                {
                    "FaceGen\\Hair\\",
                    text,
                    "\\img",
                    j
                }), j, new Action<DCCFacegenListItemVM, bool>(this.SetSelectedHairType));
                this.HairTypes.Add(item2);
            }
            this.TaintTypes.Clear();
            for (int k = 0; k < this.faceTattooNum; k++)
            {
                DCCFacegenListItemVM item3 = new DCCFacegenListItemVM(string.Concat(new object[]
                {
                    "FaceGen\\Tattoo\\",
                    text,
                    "\\img",
                    k
                }), k, new Action<DCCFacegenListItemVM, bool>(this.SetSelectedTattooType));
                this.TaintTypes.Add(item3);
            }
            this.UpdateFace(-1, (float)this._selectedGender, true, true);
            if (this.BeardTypes.Count > 0)
            {
                this.SetSelectedBeardType(this.BeardTypes[this._DCCFaceGenerationParams._curBeard], false);
            }
            this.SetSelectedHairType(this.HairTypes[this._DCCFaceGenerationParams._currentHair], false);
            this.SetSelectedTattooType(this.TaintTypes[this._DCCFaceGenerationParams._curFaceTattoo], false);
            int num2 = 0;
            for (int l = 0; l < this._isVoiceTypeUsableForOnlyNpc.Count; l++)
            {
                if (!this._isVoiceTypeUsableForOnlyNpc[l])
                {
                    num2++;
                }
            }
            this._DCCFaceGenerationParams._currentVoice = this.GetVoiceRealIndex(0);
            this.SoundPreset = new DCCFaceGenPropertyVM(-9, 0.0, (double)(num2 - 1), new TextObject("{=macpKFaG}Voice", null), -9, 0, (double)this.GetVoiceUIIndex(), new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
            this._DCCFaceGenerationParams._curFaceTexture = MBMath.ClampInt(this._DCCFaceGenerationParams._curFaceTexture, 0, this.faceTextureNum - 1);
            this.FaceTypes = new DCCFaceGenPropertyVM(-3, 0.0, (double)(this.faceTextureNum - 1), new TextObject("{=DmaP2qaR}Skin Type", null), -3, 1, (double)this._DCCFaceGenerationParams._curFaceTexture, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
            this._DCCFaceGenerationParams._curMouthTexture = MBMath.ClampInt(this._DCCFaceGenerationParams._curMouthTexture, 0, this.mouthTextureNum - 1);
            this.TeethTypes = new DCCFaceGenPropertyVM(-14, 0.0, (double)(this.mouthTextureNum - 1), new TextObject("{=l2CNxPXG}Teeth Type", null), -14, 4, (double)this._DCCFaceGenerationParams._curMouthTexture, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
            this._DCCFaceGenerationParams._curEyebrow = MBMath.ClampInt(this._DCCFaceGenerationParams._curEyebrow, 0, this.eyebrowTextureNum - 1);
            this.EyebrowTypes = new DCCFaceGenPropertyVM(-15, 0.0, (double)(this.eyebrowTextureNum - 1), new TextObject("{=bIcFZT6L}Eyebrow Type", null), -15, 4, (double)this._DCCFaceGenerationParams._curEyebrow, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
        }
        
        private void UpdateFace()
        {
            if (this._characterRefreshEnabled)
            {
                this._DCCBodyGenerator.RefreshFace(this._DCCFaceGenerationParams);
                this._faceGeneratorScreen.RefreshCharacterEntity();
            }
        }
        
        private void UpdateFace(int keyNo, float value, bool calledFromInit, bool isNeedRefresh = true)
        {
            if (this._enforceConstraints)
            {
                return;
            }
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            if (keyNo > -1)
            {
                this._DCCFaceGenerationParams.KeyWeights[keyNo] = value;
                this._enforceConstraints = DCCBodyProperties.EnforceConstraints(ref this._DCCFaceGenerationParams);
                flag3 = (this._enforceConstraints && !calledFromInit);
            }
            else
            {
                switch (keyNo)
                {
                    case -19:
                        this._DCCFaceGenerationParams._voicePitch = value;
                        goto IL_236;
                    case -18:
                        this._DCCFaceGenerationParams._curBuild = value;
                        this._enforceConstraints = DCCBodyProperties.EnforceConstraints(ref this._DCCFaceGenerationParams);
                        flag3 = (this._enforceConstraints && !calledFromInit);
                        goto IL_236;
                    case -17:
                        this._DCCFaceGenerationParams._curWeight = value;
                        this._enforceConstraints = DCCBodyProperties.EnforceConstraints(ref this._DCCFaceGenerationParams);
                        flag3 = (this._enforceConstraints && !calledFromInit);
                        goto IL_236;
                    case -16:
                        this._DCCFaceGenerationParams._heightMultiplier = value;
                        this._enforceConstraints = DCCBodyProperties.EnforceConstraints(ref this._DCCFaceGenerationParams);
                        flag3 = (this._enforceConstraints && !calledFromInit);
                        flag2 = true;
                        goto IL_236;
                    case -15:
                        this._DCCFaceGenerationParams._curEyebrow = (int)value;
                        goto IL_236;
                    case -14:
                        this._DCCFaceGenerationParams._curMouthTexture = (int)value;
                        goto IL_236;
                    case -12:
                        this._DCCFaceGenerationParams._curEyeColorOffset = value;
                        goto IL_236;
                    case -11:
                        this._DCCFaceGenerationParams._curAge = value;
                        this._enforceConstraints = DCCBodyProperties.EnforceConstraints(ref this._DCCFaceGenerationParams);
                        flag3 = (this._enforceConstraints && !calledFromInit);
                        flag = true;
                        flag2 = true;
                        goto IL_236;
                    case -10:
                        this._DCCFaceGenerationParams._curFaceTattoo = (int)value;
                        goto IL_236;
                    case -9:
                        this._DCCFaceGenerationParams._currentVoice = this.GetVoiceRealIndex((int)value);
                        goto IL_236;
                    case -7:
                        this._DCCFaceGenerationParams._curBeard = (int)value;
                        goto IL_236;
                    case -6:
                        this._DCCFaceGenerationParams._currentHair = (int)value;
                        goto IL_236;
                    case -3:
                        this._DCCFaceGenerationParams._curFaceTexture = (int)value;
                        goto IL_236;
                    case -1:
                        this._DCCFaceGenerationParams.SetGenderAndAdjustParams((int)value, (int)this._DCCFaceGenerationParams._curAge);
                        goto IL_236;
                }
                MBDebug.ShowWarning("Unknown preset!");
            }
        IL_236:
            if (flag3)
            {
                this.UpdateFacegen();
            }
            if (isNeedRefresh)
            {
                this.UpdateFace();
            }
            if (!calledFromInit && keyNo < 0)
            {
                if (keyNo != -14)
                {
                    if (keyNo == -9)
                    {
                        this._faceGeneratorScreen.MakeVoice(this._DCCFaceGenerationParams._currentGender, this._DCCFaceGenerationParams._voicePitch);
                    }
                }
                else
                {
                    this._faceGeneratorScreen.SetFacialAnimation("facegen_teeth", false);
                }
            }
            this._enforceConstraints = false;
            if (flag)
            {
                this.OnAgeChanged();
            }
            if (flag2)
            {
                this.OnHeightChanged();
            }
        }
        
        private int GetVoiceUIIndex()
        {
            int num = 0;
            for (int i = 0; i < this._DCCFaceGenerationParams._currentVoice; i++)
            {
                if (!this._isVoiceTypeUsableForOnlyNpc[i])
                {
                    num++;
                }
            }
            return num;
        }
        
        private int GetVoiceRealIndex(int UIValue)
        {
            int num = 0;
            for (int i = 0; i < this._newSoundPresetSize; i++)
            {
                if (!this._isVoiceTypeUsableForOnlyNpc[i])
                {
                    if (num == UIValue)
                    {
                        return i;
                    }
                    num++;
                }
            }
            return -1;
        }
        
        private void ExecuteHearCurrentVoiceSample()
        {
            this._faceGeneratorScreen.MakeVoice(this._DCCFaceGenerationParams._currentGender, this._DCCFaceGenerationParams._voicePitch);
        }
        
        private void ExecuteReset()
        {
            this.AddCommand();
            this._characterRefreshEnabled = false;
            switch (this.Tab)
            {
                case 0:
                    this.SelectedGender = this._initialGender;
                    this.SoundPreset.Reset();
                    this.SkinColorSelector.SelectedIndex = (int)Math.Round((double)this._initialSelectedSkinColor * (double)(this._skinColors.Count - 1));
                    break;
                case 1:
                    this.FaceTypes.Reset();
                    break;
                case 2:
                    this.EyebrowTypes.Reset();
                    break;
                case 4:
                    this.TeethTypes.Reset();
                    break;
                case 5:
                    this.SetSelectedBeardType(this.BeardTypes[(this._selectedGender == 1) ? 0 : this._initialSelectedBeardType], false);
                    if (this._initialSelectedHairType > this.HairTypes.Count - 1)
                    {
                        this.SetSelectedHairType(this.HairTypes[this.HairTypes.Count - 1], false);
                    }
                    else
                    {
                        this.SetSelectedHairType(this.HairTypes[this._initialSelectedHairType], false);
                    }
                    this.HairColorSelector.SelectedIndex = (int)Math.Round((double)this._initialSelectedHairColor * (double)(this._hairColors.Count - 1));
                    break;
                case 6:
                    if (this._initialSelectedTaintType > this.TaintTypes.Count - 1)
                    {
                        this.SetSelectedTattooType(this.TaintTypes[this.TaintTypes.Count - 1], false);
                    }
                    else
                    {
                        this.SetSelectedTattooType(this.TaintTypes[this._initialSelectedTaintType], false);
                    }
                    this.TattooColorSelector.SelectedIndex = (int)Math.Round((double)this._initialSelectedTaintColor * (double)(this._tattooColors.Count - 1));
                    break;
            }
            foreach (DCCFaceGenPropertyVM DCCFaceGenPropertyVM in this._tabProperties[(DCCFaceGenVM.FaceGenTabs)this.Tab])
            {
                if (DCCFaceGenPropertyVM.TabID == this.Tab)
                {
                    DCCFaceGenPropertyVM.Reset();
                }
            }
            this._characterRefreshEnabled = true;
            this.UpdateFace();
        }
        
        private void ExecuteResetAll()
        {
            if (this.SelectedGender == this._initialGender)
            {
                this.AddCommand();
            }
            else
            {
                this.SelectedGender = this._initialGender;
            }
            foreach (KeyValuePair<DCCFaceGenVM.FaceGenTabs, MBBindingList<DCCFaceGenPropertyVM>> keyValuePair in this._tabProperties)
            {
                foreach (DCCFaceGenPropertyVM DCCFaceGenPropertyVM in keyValuePair.Value)
                {
                    DCCFaceGenPropertyVM.Reset();
                }
            }
            this.FaceTypes.Reset();
            this.SoundPreset.Reset();
            this.TeethTypes.Reset();
            this.EyebrowTypes.Reset();
            this.SetSelectedBeardType(this.BeardTypes[this._initialSelectedBeardType], false);
            this.SetSelectedHairType(this.HairTypes[this._initialSelectedHairType], false);
            this.SetSelectedTattooType(this.TaintTypes[this._initialSelectedTaintType], false);
            this.SkinColorSelector.SelectedIndex = (int)Math.Round((double)this._initialSelectedSkinColor * (double)(this._skinColors.Count - 1));
            this.HairColorSelector.SelectedIndex = (int)Math.Round((double)this._initialSelectedHairColor * (double)(this._hairColors.Count - 1));
            this.TattooColorSelector.SelectedIndex = (int)Math.Round((double)this._initialSelectedTaintColor * (double)(this._tattooColors.Count - 1));
            this.UpdateFace();
        }
        
        private void ExecuteRandomize()
        {
            this.AddCommand();
            this._characterRefreshEnabled = false;
            foreach (DCCFaceGenPropertyVM DCCFaceGenPropertyVM in this._tabProperties[(DCCFaceGenVM.FaceGenTabs)this.Tab])
            {
                DCCFaceGenPropertyVM.Randomize();
            }
            switch (this.Tab)
            {
                case 0:
                    this.SkinColorSelector.SelectedIndex = MBRandom.RandomInt(this._skinColors.Count);
                    break;
                case 1:
                    this.FaceTypes.Value = (float)MBRandom.RandomInt((int)this.FaceTypes.Max + 1);
                    break;
                case 2:
                    this.EyebrowTypes.Value = (float)MBRandom.RandomInt((int)this.EyebrowTypes.Max + 1);
                    break;
                case 4:
                    this.TeethTypes.Value = (float)MBRandom.RandomInt((int)this.TeethTypes.Max + 1);
                    break;
                case 5:
                    this.SetSelectedBeardType(this.BeardTypes[MBRandom.RandomInt(this.BeardTypes.Count)], false);
                    this.SetSelectedHairType(this.HairTypes[MBRandom.RandomInt(this.HairTypes.Count)], false);
                    this.HairColorSelector.SelectedIndex = MBRandom.RandomInt(this._hairColors.Count);
                    break;
                case 6:
                    this.SetSelectedTattooType(this.TaintTypes[MBRandom.RandomInt(this.TaintTypes.Count)], false);
                    this.TattooColorSelector.SelectedIndex = MBRandom.RandomInt(this._tattooColors.Count);
                    break;
            }
            this._characterRefreshEnabled = true;
            this.UpdateFace();
        }
        
        private void ExecuteRandomizeAll()
        {
            this.AddCommand();
            this._characterRefreshEnabled = false;
            foreach (KeyValuePair<DCCFaceGenVM.FaceGenTabs, MBBindingList<DCCFaceGenPropertyVM>> keyValuePair in this._tabProperties)
            {
                foreach (DCCFaceGenPropertyVM DCCFaceGenPropertyVM in keyValuePair.Value)
                {
                    DCCFaceGenPropertyVM.Randomize();
                }
            }
            this.FaceTypes.Value = (float)MBRandom.RandomInt((int)this.FaceTypes.Max + 1);
            if (this.BeardTypes.Count > 0)
            {
                this.SetSelectedBeardType(this.BeardTypes[MBRandom.RandomInt(this.BeardTypes.Count)], false);
            }
            if (this.HairTypes.Count > 0)
            {
                this.SetSelectedHairType(this.HairTypes[MBRandom.RandomInt(this.HairTypes.Count)], false);
            }
            this.EyebrowTypes.Value = (float)MBRandom.RandomInt((int)this.EyebrowTypes.Max + 1);
            this.TeethTypes.Value = (float)MBRandom.RandomInt((int)this.TeethTypes.Max + 1);
            if (MBRandom.RandomFloat < this._DCCFaceGenerationParams._tattooZeroProbability)
            {
                this.SetSelectedTattooType(this.TaintTypes[0], false);
            }
            else
            {
                this.SetSelectedTattooType(this.TaintTypes[MBRandom.RandomInt(1, this.TaintTypes.Count)], false);
            }
            this.TattooColorSelector.SelectedIndex = MBRandom.RandomInt(this._tattooColors.Count);
            this.HairColorSelector.SelectedIndex = MBRandom.RandomInt(this._hairColors.Count);
            this.SkinColorSelector.SelectedIndex = MBRandom.RandomInt(this._skinColors.Count);
            this._characterRefreshEnabled = true;
            this.UpdateFace();
        }
        
        private void ExecuteCancel()
        {
            this._faceGeneratorScreen.Cancel();
        }
        
        private void ExecuteDone()
        {
            this._faceGeneratorScreen.Done();
        }
        
        private void ExecuteRedo()
        {
            if (this._redoCommands.Count > 0)
            {
                int index = this._redoCommands.Count - 1;
                BodyProperties value = this._redoCommands[index].Value;
                int key = this._redoCommands[index].Key;
                this._redoCommands.RemoveAt(index);
                this._undoCommands.Add(new KeyValuePair<int, BodyProperties>(this._DCCFaceGenerationParams._currentGender, this._DCCBodyGenerator.CurrentBodyProperties));
                this._characterRefreshEnabled = false;
                this.SetBodyProperties(value, false, key);
                this._characterRefreshEnabled = true;
            }
        }
        
        private void ExecuteUndo()
        {
            if (this._undoCommands.Count > 0)
            {
                int index = this._undoCommands.Count - 1;
                BodyProperties value = this._undoCommands[index].Value;
                int key = this._undoCommands[index].Key;
                this._undoCommands.RemoveAt(index);
                this._redoCommands.Add(new KeyValuePair<int, BodyProperties>(this._DCCFaceGenerationParams._currentGender, this._DCCBodyGenerator.CurrentBodyProperties));
                this._characterRefreshEnabled = false;
                this.SetBodyProperties(value, false, key);
                this._characterRefreshEnabled = true;
            }
        }
        
        private void ExecuteChangeClothing()
        {
            if (this.IsDressed)
            {
                this._faceGeneratorScreen.UndressCharacterEntity();
                this.IsDressed = false;
                return;
            }
            this._faceGeneratorScreen.DressCharacterEntity();
            this.IsDressed = true;
        }
        
        private void AddCommand()
        {
            if (this._characterRefreshEnabled)
            {
                if (this._undoCommands.Count + 1 == this._undoCommands.Capacity)
                {
                    this._undoCommands.RemoveAt(0);
                }
                this._undoCommands.Add(new KeyValuePair<int, BodyProperties>(this._DCCFaceGenerationParams._currentGender, this._DCCBodyGenerator.CurrentBodyProperties));
                this._redoCommands.Clear();
            }
        }
        
        private void UpdateTitle()
        {
        }
        
        private void ExecuteGoToIndex(int index)
        {
            this._goToIndex(index);
        }
        
        public void SetBodyProperties(BodyProperties bodyProperties, bool ignoreDebugValues, int gender = -1)
        {
            this._characterRefreshEnabled = false;
            if (gender == -1)
            {
                this._DCCFaceGenerationParams._currentGender = this._selectedGender;
            }
            else
            {
                this._DCCFaceGenerationParams._currentGender = gender;
            }
            if (ignoreDebugValues)
            {
                bodyProperties = new BodyProperties(new DynamicBodyProperties(this._DCCBodyGenerator.CurrentBodyProperties.Age, this._DCCBodyGenerator.CurrentBodyProperties.Weight, this._DCCBodyGenerator.CurrentBodyProperties.Build), bodyProperties.StaticProperties);
            }
            this._DCCBodyGenerator.CurrentBodyProperties = bodyProperties;
            DCCBodyProperties.GetParamsFromKey(ref this._DCCFaceGenerationParams, bodyProperties, this._DCCBodyGenerator.Character.Equipment.EarsAreHidden);
            this.UpdateFacegen();
            this._characterRefreshEnabled = true;
            this.UpdateFace();
        }
        
        private void ResetSliderPrevValues()
        {
            foreach (MBBindingList<DCCFaceGenPropertyVM> mbbindingList in this._tabProperties.Values)
            {
                foreach (DCCFaceGenPropertyVM DCCFaceGenPropertyVM in mbbindingList)
                {
                    DCCFaceGenPropertyVM.PrevValue = -1.0;
                }
            }
        }
        
        public void UpdateFacegen()
        {
            foreach (MBBindingList<DCCFaceGenPropertyVM> mbbindingList in this._tabProperties.Values)
            {
                foreach (DCCFaceGenPropertyVM DCCFaceGenPropertyVM in mbbindingList)
                {
                    if (DCCFaceGenPropertyVM.KeyNo < 0)
                    {
                        switch (DCCFaceGenPropertyVM.KeyNo)
                        {
                            case -18:
                                DCCFaceGenPropertyVM.Value = this._DCCFaceGenerationParams._curBuild;
                                break;
                            case -17:
                                DCCFaceGenPropertyVM.Value = this._DCCFaceGenerationParams._curWeight;
                                break;
                            case -16:
                                DCCFaceGenPropertyVM.Value = this._DCCFaceGenerationParams._heightMultiplier;
                                break;
                            case -12:
                                DCCFaceGenPropertyVM.Value = this._DCCFaceGenerationParams._curEyeColorOffset;
                                break;
                            case -11:
                                DCCFaceGenPropertyVM.Value = this._DCCFaceGenerationParams._curAge;
                                break;
                        }
                    }
                    else
                    {
                        DCCFaceGenPropertyVM.Value = this._DCCFaceGenerationParams.KeyWeights[DCCFaceGenPropertyVM.KeyNo];
                    }
                    DCCFaceGenPropertyVM.PrevValue = -1.0;
                }
            }
            this.SelectedGender = this._DCCFaceGenerationParams._currentGender;
            this.SoundPreset.Value = (float)this.GetVoiceUIIndex();
            this.FaceTypes.Value = (float)this._DCCFaceGenerationParams._curFaceTexture;
            this.EyebrowTypes.Value = (float)this._DCCFaceGenerationParams._curEyebrow;
            this.TeethTypes.Value = (float)this._DCCFaceGenerationParams._curMouthTexture;
            if (this.TaintTypes.Count > this._DCCFaceGenerationParams._curFaceTattoo)
            {
                this.SetSelectedTattooType(this.TaintTypes[this._DCCFaceGenerationParams._curFaceTattoo], false);
            }
            if (this.BeardTypes.Count > this._DCCFaceGenerationParams._curBeard)
            {
                this.SetSelectedBeardType(this.BeardTypes[this._DCCFaceGenerationParams._curBeard], false);
            }
            if (this.HairTypes.Count > this._DCCFaceGenerationParams._currentHair)
            {
                this.SetSelectedHairType(this.HairTypes[this._DCCFaceGenerationParams._currentHair], false);
            }
            this.SkinColorSelector.SelectedIndex = (int)Math.Round((double)this._DCCFaceGenerationParams._curSkinColorOffset * (double)(this._skinColors.Count - 1));
            this.HairColorSelector.SelectedIndex = (int)Math.Round((double)this._DCCFaceGenerationParams._curHairColorOffset * (double)(this._hairColors.Count - 1));
            this.TattooColorSelector.SelectedIndex = (int)Math.Round((double)this._DCCFaceGenerationParams._curFaceTattooColorOffset1 * (double)(this._tattooColors.Count - 1));
        }
        
        private void SetSelectedHairType(DCCFacegenListItemVM item, bool addCommand)
        {
            if (this._selectedHairType != null)
            {
                this._selectedHairType.IsSelected = false;
            }
            this._selectedHairType = item;
            this._selectedHairType.IsSelected = true;
            this._DCCFaceGenerationParams._currentHair = item.Index;
            if (!addCommand)
            {
                return;
            }
            this.AddCommand();
            this.UpdateFace(-6, (float)item.Index, false, true);
        }
        
        private void SetSelectedTattooType(DCCFacegenListItemVM item, bool addCommand)
        {
            if (this._selectedTaintType != null)
            {
                this._selectedTaintType.IsSelected = false;
            }
            this._selectedTaintType = item;
            this._selectedTaintType.IsSelected = true;
            this._DCCFaceGenerationParams._curFaceTattoo = item.Index;
            if (!addCommand)
            {
                return;
            }
            this.AddCommand();
            this.UpdateFace(-10, (float)item.Index, false, true);
        }
        
        private void SetSelectedBeardType(DCCFacegenListItemVM item, bool addCommand)
        {
            if (this._selectedBeardType != null)
            {
                this._selectedBeardType.IsSelected = false;
            }
            this._selectedBeardType = item;
            this._selectedBeardType.IsSelected = true;
            this._DCCFaceGenerationParams._curBeard = item.Index;
            if (!addCommand)
            {
                return;
            }
            this.AddCommand();
            this.UpdateFace(-7, (float)item.Index, false, true);
        }
        
        [DataSourceProperty]
        public string FlipHairLbl
        {
            get
            {
                return this._flipHairLbl;
            }
            set
            {
                if (value != this._flipHairLbl)
                {
                    this._flipHairLbl = value;
                    base.OnPropertyChanged("FlipHairLbl");
                }
            }
        }
        
        [DataSourceProperty]
        public string SkinColorLbl
        {
            get
            {
                return this._skinColorLbl;
            }
            set
            {
                if (value != this._skinColorLbl)
                {
                    this._skinColorLbl = value;
                    base.OnPropertyChanged("SkinColorLbl");
                }
            }
        }
        
        [DataSourceProperty]
        public string GenderLbl
        {
            get
            {
                return this._genderLbl;
            }
            set
            {
                if (value != this._genderLbl)
                {
                    this._genderLbl = value;
                    base.OnPropertyChanged("GenderLbl");
                }
            }
        }
        
        [DataSourceProperty]
        public string CancelBtnLbl
        {
            get
            {
                return this._cancelBtnLbl;
            }
            set
            {
                if (value != this._cancelBtnLbl)
                {
                    this._cancelBtnLbl = value;
                    base.OnPropertyChanged("CancelBtnLbl");
                }
            }
        }
        
        [DataSourceProperty]
        public string DoneBtnLbl
        {
            get
            {
                return this._doneBtnLbl;
            }
            set
            {
                if (value != this._doneBtnLbl)
                {
                    this._doneBtnLbl = value;
                    base.OnPropertyChanged("DoneBtnLbl");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel BodyHint
        {
            get
            {
                return this._bodyHint;
            }
            set
            {
                if (value != this._bodyHint)
                {
                    this._bodyHint = value;
                    base.OnPropertyChanged("BodyHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel FaceHint
        {
            get
            {
                return this._faceHint;
            }
            set
            {
                if (value != this._faceHint)
                {
                    this._faceHint = value;
                    base.OnPropertyChanged("FaceHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel EyesHint
        {
            get
            {
                return this._eyesHint;
            }
            set
            {
                if (value != this._eyesHint)
                {
                    this._eyesHint = value;
                    base.OnPropertyChanged("EyesHint");
                }
            }
        }

        [DataSourceProperty]
        public HintViewModel NoseHint
        {
            get
            {
                return this._noseHint;
            }
            set
            {
                if (value != this._noseHint)
                {
                    this._noseHint = value;
                    base.OnPropertyChanged("NoseHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel HairHint
        {
            get
            {
                return this._hairHint;
            }
            set
            {
                if (value != this._hairHint)
                {
                    this._hairHint = value;
                    base.OnPropertyChanged("HairHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel TaintHint
        {
            get
            {
                return this._taintHint;
            }
            set
            {
                if (value != this._taintHint)
                {
                    this._taintHint = value;
                    base.OnPropertyChanged("TaintHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel MouthHint
        {
            get
            {
                return this._mouthHint;
            }
            set
            {
                if (value != this._mouthHint)
                {
                    this._mouthHint = value;
                    base.OnPropertyChanged("MouthHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel RedoHint
        {
            get
            {
                return this._redoHint;
            }
            set
            {
                if (value != this._redoHint)
                {
                    this._redoHint = value;
                    base.OnPropertyChanged("RedoHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel UndoHint
        {
            get
            {
                return this._undoHint;
            }
            set
            {
                if (value != this._undoHint)
                {
                    this._undoHint = value;
                    base.OnPropertyChanged("UndoHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel RandomizeHint
        {
            get
            {
                return this._randomizeHint;
            }
            set
            {
                if (value != this._randomizeHint)
                {
                    this._randomizeHint = value;
                    base.OnPropertyChanged("RandomizeHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel RandomizeAllHint
        {
            get
            {
                return this._randomizeAllHint;
            }
            set
            {
                if (value != this._randomizeAllHint)
                {
                    this._randomizeAllHint = value;
                    base.OnPropertyChanged("RandomizeAllHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel ResetHint
        {
            get
            {
                return this._resetHint;
            }
            set
            {
                if (value != this._resetHint)
                {
                    this._resetHint = value;
                    base.OnPropertyChanged("ResetHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel ResetAllHint
        {
            get
            {
                return this._resetAllHint;
            }
            set
            {
                if (value != this._resetAllHint)
                {
                    this._resetAllHint = value;
                    base.OnPropertyChanged("ResetAllHint");
                }
            }
        }
        
        [DataSourceProperty]
        public HintViewModel ClothHint
        {
            get
            {
                return this._clothHint;
            }
            set
            {
                if (value != this._clothHint)
                {
                    this._clothHint = value;
                    base.OnPropertyChanged("ClothHint");
                }
            }
        }
        
        [DataSourceProperty]
        public int HairNum
        {
            get
            {
                return this.hairNum;
            }
            set
            {
                if (value != this.hairNum)
                {
                    this.hairNum = value;
                    base.OnPropertyChanged("HairNum");
                }
            }
        }
        
        [DataSourceProperty]
        public SelectorVM<SelectorItemVM> SkinColorSelector
        {
            get
            {
                return this._skinColorSelector;
            }
            set
            {
                if (value != this._skinColorSelector)
                {
                    this._skinColorSelector = value;
                    base.OnPropertyChanged("SkinColorSelector");
                }
            }
        }
        
        [DataSourceProperty]
        public SelectorVM<SelectorItemVM> HairColorSelector
        {
            get
            {
                return this._hairColorSelector;
            }
            set
            {
                if (value != this._hairColorSelector)
                {
                    this._hairColorSelector = value;
                    base.OnPropertyChanged("HairColorSelector");
                }
            }
        }
        
        [DataSourceProperty]
        public SelectorVM<SelectorItemVM> TattooColorSelector
        {
            get
            {
                return this._tattooColorSelector;
            }
            set
            {
                if (value != this._tattooColorSelector)
                {
                    this._tattooColorSelector = value;
                    base.OnPropertyChanged("TattooColorSelector");
                }
            }
        }
        
        [DataSourceProperty]
        public int Tab
        {
            get
            {
                return this._tab;
            }
            set
            {
                if (this._tab != value)
                {
                    this._tab = value;
                    base.OnPropertyChanged("Tab");
                    if (value == 0)
                    {
                        this._faceGeneratorScreen.ChangeToBodyCamera();
                    }
                }
                if (value == 2)
                {
                    this._faceGeneratorScreen.ChangeToEyeCamera();
                }
                else if (value == 3)
                {
                    this._faceGeneratorScreen.ChangeToNoseCamera();
                }
                else if (value == 4)
                {
                    this._faceGeneratorScreen.ChangeToMouthCamera();
                }
                else if (value == 1 || value == 6)
                {
                    this._faceGeneratorScreen.ChangeToFaceCamera();
                }
                else if (value == 5)
                {
                    this._faceGeneratorScreen.ChangeToHairCamera();
                }
                this.UpdateTitle();
            }
        }
        
        [DataSourceProperty]
        public int SelectedGender
        {
            get
            {
                return this._selectedGender;
            }
            set
            {
                if (this._initialGender == -1)
                {
                    this._initialGender = value;
                }
                if (this._selectedGender != value)
                {
                    this.AddCommand();
                    this._selectedGender = value;
                    this.UpdateGenderBasedResources();
                    this.Refresh();
                    base.OnPropertyChanged("SelectedGender");
                    base.OnPropertyChanged("IsFemale");
                }
            }
        }
        
        [DataSourceProperty]
        public bool IsFemale
        {
            get
            {
                return this.SelectedGender != 0;
            }
        }
        
        [DataSourceProperty]
        public MBBindingList<DCCFaceGenPropertyVM> BodyProperties
        {
            get
            {
                return this._bodyProperties;
            }
            set
            {
                if (value != this._bodyProperties)
                {
                    this._bodyProperties = value;
                    base.OnPropertyChanged("BodyProperties");
                }
            }
        }
        
        [DataSourceProperty]
        public bool CanChangeGender
        {
            get
            {
                return this._canChangeGender;
            }
            set
            {
                if (value != this._canChangeGender)
                {
                    this._canChangeGender = value;
                    base.OnPropertyChanged("CanChangeGender");
                }
            }
        }
        
        [DataSourceProperty]
        public MBBindingList<DCCFaceGenPropertyVM> FaceProperties
        {
            get
            {
                return this._faceProperties;
            }
            set
            {
                if (value != this._faceProperties)
                {
                    this._faceProperties = value;
                    base.OnPropertyChanged("FaceProperties");
                }
            }
        }
        
        [DataSourceProperty]
        public MBBindingList<DCCFaceGenPropertyVM> EyesProperties
        {
            get
            {
                return this._eyesProperties;
            }
            set
            {
                if (value != this._eyesProperties)
                {
                    this._eyesProperties = value;
                    base.OnPropertyChanged("EyesProperties");
                }
            }
        }
        
        [DataSourceProperty]
        public MBBindingList<DCCFaceGenPropertyVM> NoseProperties
        {
            get
            {
                return this._noseProperties;
            }
            set
            {
                if (value != this._noseProperties)
                {
                    this._noseProperties = value;
                    base.OnPropertyChanged("NoseProperties");
                }
            }
        }
       
        [DataSourceProperty]
        public MBBindingList<DCCFaceGenPropertyVM> MouthProperties
        {
            get
            {
                return this._mouthProperties;
            }
            set
            {
                if (value != this._mouthProperties)
                {
                    this._mouthProperties = value;
                    base.OnPropertyChanged("MouthProperties");
                }
            }
        }
        
        [DataSourceProperty]
        public MBBindingList<DCCFaceGenPropertyVM> HairProperties
        {
            get
            {
                return this._hairProperties;
            }
            set
            {
                if (value != this._hairProperties)
                {
                    this._hairProperties = value;
                    base.OnPropertyChanged("HairProperties");
                }
            }
        }
        
        [DataSourceProperty]
        public MBBindingList<DCCFaceGenPropertyVM> TaintProperties
        {
            get
            {
                return this._taintProperties;
            }
            set
            {
                if (value != this._taintProperties)
                {
                    this._taintProperties = value;
                    base.OnPropertyChanged("TaintProperties");
                }
            }
        }
        
        [DataSourceProperty]
        public MBBindingList<DCCFacegenListItemVM> TaintTypes
        {
            get
            {
                return this._taintTypes;
            }
            set
            {
                if (value != this._taintTypes)
                {
                    this._taintTypes = value;
                    base.OnPropertyChanged("TaintTypes");
                }
            }
        }
        
        [DataSourceProperty]
        public MBBindingList<DCCFacegenListItemVM> BeardTypes
        {
            get
            {
                return this._beardTypes;
            }
            set
            {
                if (value != this._beardTypes)
                {
                    this._beardTypes = value;
                    base.OnPropertyChanged("BeardTypes");
                }
            }
        }
        
        [DataSourceProperty]
        public MBBindingList<DCCFacegenListItemVM> HairTypes
        {
            get
            {
                return this._hairTypes;
            }
            set
            {
                if (value != this._hairTypes)
                {
                    this._hairTypes = value;
                    base.OnPropertyChanged("HairTypes");
                }
            }
        }
        
        [DataSourceProperty]
        public DCCFaceGenPropertyVM SoundPreset
        {
            get
            {
                return this._soundPreset;
            }
            set
            {
                if (value != this._soundPreset)
                {
                    this._soundPreset = value;
                    base.OnPropertyChanged("SoundPreset");
                }
            }
        }
        
        [DataSourceProperty]
        public DCCFaceGenPropertyVM EyebrowTypes
        {
            get
            {
                return this._eyebrowTypes;
            }
            set
            {
                if (value != this._eyebrowTypes)
                {
                    this._eyebrowTypes = value;
                    base.OnPropertyChanged("EyebrowTypes");
                }
            }
        }
        
        [DataSourceProperty]
        public DCCFaceGenPropertyVM TeethTypes
        {
            get
            {
                return this._teethTypes;
            }
            set
            {
                if (value != this._teethTypes)
                {
                    this._teethTypes = value;
                    base.OnPropertyChanged("TeethTypes");
                }
            }
        }
        
        [DataSourceProperty]
        public bool FlipHairCb
        {
            get
            {
                return this._DCCFaceGenerationParams._isHairFlipped;
            }
            set
            {
                if (value != this._DCCFaceGenerationParams._isHairFlipped)
                {
                    this._DCCFaceGenerationParams._isHairFlipped = value;
                    base.OnPropertyChanged("FlipHairCb");
                    this.UpdateFace();
                }
            }
        }
        
        [DataSourceProperty]
        public bool IsDressed
        {
            get
            {
                return this._isDressed;
            }
            set
            {
                if (value != this._isDressed)
                {
                    this._isDressed = value;
                    base.OnPropertyChanged("IsDressed");
                }
            }
        }
        
        [DataSourceProperty]
        public DCCFaceGenPropertyVM FaceTypes
        {
            get
            {
                return this._faceTypes;
            }
            set
            {
                if (value != this._faceTypes)
                {
                    this._faceTypes = value;
                    base.OnPropertyChanged("FaceTypes");
                }
            }
        }
        
        [DataSourceProperty]
        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                if (value != this._title)
                {
                    this._title = value;
                    base.OnPropertyChanged("Title");
                }
            }
        }
        
        [DataSourceProperty]
        public int TotalStageCount
        {
            get
            {
                return this._totalStageCount;
            }
            set
            {
                if (value != this._totalStageCount)
                {
                    this._totalStageCount = value;
                    base.OnPropertyChanged("TotalStageCount");
                }
            }
        }
        
        [DataSourceProperty]
        public int CurrentStageIndex
        {
            get
            {
                return this._currentStageIndex;
            }
            set
            {
                if (value != this._currentStageIndex)
                {
                    this._currentStageIndex = value;
                    base.OnPropertyChanged("CurrentStageIndex");
                }
            }
        }
        
        [DataSourceProperty]
        public int FurthestIndex
        {
            get
            {
                return this._furthestIndex;
            }
            set
            {
                if (value != this._furthestIndex)
                {
                    this._furthestIndex = value;
                    base.OnPropertyChanged("FurthestIndex");
                }
            }
        }
        
        private readonly DCCIFaceGeneratorHandler _faceGeneratorScreen;
        
        private bool _characterRefreshEnabled = true;
        
        private readonly DCCBodyGenerator _DCCBodyGenerator;
        
        private readonly TextObject _affirmitiveText;
        
        private readonly TextObject _negativeText;
        
        private DCCFaceGenerationParams _DCCFaceGenerationParams = DCCFaceGenerationParams.Create();
        
        private List<KeyValuePair<int, BodyProperties>> _undoCommands;
        
        private List<KeyValuePair<int, BodyProperties>> _redoCommands;
        
        private List<bool> _isVoiceTypeUsableForOnlyNpc;
        
        private Action<float> _onHeightChanged;
        
        private Action _onAgeChanged;
        
        private int _initialGender = -1;
        
        private readonly Action<int> _goToIndex;
        
        private readonly Dictionary<DCCFaceGenVM.FaceGenTabs, MBBindingList<DCCFaceGenPropertyVM>> _tabProperties;
    
        private List<uint> _skinColors;
        
        private List<uint> _hairColors;
        
        private List<uint> _tattooColors;
        
        private readonly bool _showDebugValues;
        
        private readonly bool _openedFromMultiplayer;
        
        private bool _enforceConstraints;
        
        private DCCFacegenListItemVM _selectedTaintType;
        
        private DCCFacegenListItemVM _selectedBeardType;
        
        private DCCFacegenListItemVM _selectedHairType;
        
        private string _cancelBtnLbl;
        
        private string _doneBtnLbl;
        
        private int _initialSelectedTaintType;
        
        private int _initialSelectedHairType;
        
        private int _initialSelectedBeardType;
        
        private float _initialSelectedSkinColor;
        
        private float _initialSelectedHairColor;
        
        private float _initialSelectedTaintColor;
        
        private string _flipHairLbl;
        
        private string _skinColorLbl;
        
        private string _genderLbl;
        
        private DCCFaceGenPropertyVM _heightSlider;
        
        private HintViewModel _bodyHint;
        
        private HintViewModel _faceHint;
        
        private HintViewModel _eyesHint;
        
        private HintViewModel _noseHint;
        
        private HintViewModel _hairHint;
        
        private HintViewModel _taintHint;
        
        private HintViewModel _mouthHint;
        
        private HintViewModel _redoHint;
        
        private HintViewModel _undoHint;
        
        private HintViewModel _randomizeHint;
        
        private HintViewModel _randomizeAllHint;
        
        private HintViewModel _resetHint;
        
        private HintViewModel _resetAllHint;
        
        private HintViewModel _clothHint;
        
        private int hairNum;
        
        private int beardNum;
        
        private int faceTextureNum;
        
        private int mouthTextureNum;
        
        private int eyebrowTextureNum;
        
        private int faceTattooNum;
        
        private int _newSoundPresetSize;
        
        private float _scale = 1f;
        
        private int _tab = -1;
        
        private int _selectedGender = -1;
        
        private bool _canChangeGender;
        
        private bool _isDressed;
        
        private MBBindingList<DCCFaceGenPropertyVM> _bodyProperties;
        
        private MBBindingList<DCCFaceGenPropertyVM> _faceProperties;
        
        private MBBindingList<DCCFaceGenPropertyVM> _eyesProperties;
        
        private MBBindingList<DCCFaceGenPropertyVM> _noseProperties;
        
        private MBBindingList<DCCFaceGenPropertyVM> _mouthProperties;
        
        private MBBindingList<DCCFaceGenPropertyVM> _hairProperties;
        
        private MBBindingList<DCCFaceGenPropertyVM> _taintProperties;
        
        private MBBindingList<DCCFacegenListItemVM> _taintTypes;
        
        private MBBindingList<DCCFacegenListItemVM> _beardTypes;
        
        private MBBindingList<DCCFacegenListItemVM> _hairTypes;
        
        private DCCFaceGenPropertyVM _soundPreset;
        
        private DCCFaceGenPropertyVM _faceTypes;
        
        private DCCFaceGenPropertyVM _teethTypes;
        
        private DCCFaceGenPropertyVM _eyebrowTypes;
        
        private SelectorVM<SelectorItemVM> _skinColorSelector;
        
        private SelectorVM<SelectorItemVM> _hairColorSelector;
        
        private SelectorVM<SelectorItemVM> _tattooColorSelector;
        
        private string _title = "";
        
        private int _totalStageCount = -1;
        
        private int _currentStageIndex = -1;
        
        private int _furthestIndex = -1;
        
        public enum FaceGenTabs
        {
            None = -1,
            Body,
            Face,
            Eyes,
            Nose,
            Mouth,
            Hair,
            Taint,
            NumOfFaceGenTabs
        }
        
        public enum Presets
        {
            Gender = -1,
            FacePresets = -2,
            FaceType = -3,
            EyePresets = -4,
            HairBeardPreset = -5,
            HairType = -6,
            BeardType = -7,
            TaintPresets = -8,
            SoundPresets = -9,
            TaintType = -10,
            Age = -11,
            EyeColor = -12,
            HairAndBeardColor = -13,
            TeethType = -14,
            EyebrowType = -15,
            Scale = -16,
            Weight = -17,
            Build = -18,
            Pitch = -19
        }
    }
}
