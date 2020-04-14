using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace CharacterCreation.Models
{
    public class DCCFaceGenPropertyVM : ViewModel
    {
        public int KeyTimePoint { get; }
        
        public DCCFaceGenPropertyVM(int keyNo, double min, double max, TextObject name, int keyTimePoint, int tabId, double value, Action<int, float, bool, bool> updateFace, Action addCommand, Action resetSliderPrevValuesCommand, bool isEnabled = true, bool isDiscrete = false)
        {
            this._calledFromInit = true;
            this._updateFace = updateFace;
            this._addCommand = addCommand;
            this._nameObj = name;
            this._resetSliderPrevValuesCommand = resetSliderPrevValuesCommand;
            this.KeyNo = keyNo;
            this.Min = (float)min;
            this.Max = (float)max;
            this.KeyTimePoint = keyTimePoint;
            this.TabID = tabId;
            this._initialValue = (float)value;
            this.Value = (float)value;
            this.PrevValue = -1.0;
            this.IsEnabled = isEnabled;
            this.IsDiscrete = isDiscrete;
            this._calledFromInit = false;
            this.RefreshValues();
        }
        
        public void Reset()
        {
            this._updateOnValueChange = false;
            this.Value = this._initialValue;
            this._updateOnValueChange = true;
        }
        
        public void Randomize()
        {
            this._updateOnValueChange = false;
            float num = 0.5f * (MBRandom.RandomFloat + MBRandom.RandomFloat);
            this.Value = num * (this.Max - this.Min) + this.Min;
            this._updateOnValueChange = true;
        }
        
        public override void RefreshValues()
        {
            base.RefreshValues();
            this.Name = this._nameObj.ToString();
        }
        
        [DataSourceProperty]
        public float Min
        {
            get
            {
                return this._min;
            }
            set
            {
                if (value != this._min)
                {
                    this._min = value;
                    base.OnPropertyChanged("Min");
                }
            }
        }
        
        [DataSourceProperty]
        public int TabID
        {
            get
            {
                return this._tabID;
            }
            set
            {
                if (value != this._tabID)
                {
                    this._tabID = value;
                    base.OnPropertyChanged("TabID");
                }
            }
        }
        
        [DataSourceProperty]
        public float Max
        {
            get
            {
                return this._max;
            }
            set
            {
                if (value != this._max)
                {
                    this._max = value;
                    base.OnPropertyChanged("Max");
                }
            }
        }
        
        [DataSourceProperty]
        public float Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if ((double)Math.Abs(value - this._value) > ((this.KeyNo == -16) ? 0.006000000052154064 : 0.06))
                {
                    if (!this._calledFromInit && this.PrevValue < 0.0 && this._updateOnValueChange)
                    {
                        this._addCommand();
                    }
                    this._resetSliderPrevValuesCommand();
                    if (this.KeyNo >= 0)
                    {
                        this.PrevValue = (double)this._value;
                    }
                    this._value = value;
                    base.OnPropertyChanged("Value");
                    Action<int, float, bool, bool> updateFace = this._updateFace;
                    if (updateFace == null)
                    {
                        return;
                    }
                    updateFace(this.KeyNo, value, this._calledFromInit, this._updateOnValueChange);
                }
            }
        }
        
        [DataSourceProperty]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    base.OnPropertyChanged("Name");
                }
            }
        }
        
        [DataSourceProperty]
        public bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }
            set
            {
                if (value != this._isEnabled)
                {
                    this._isEnabled = value;
                    base.OnPropertyChanged("IsEnabled");
                }
            }
        }
        
        [DataSourceProperty]
        public bool IsDiscrete
        {
            get
            {
                return this._isDiscrete;
            }
            set
            {
                if (value != this._isDiscrete)
                {
                    this._isDiscrete = value;
                    base.OnPropertyChanged("IsDiscrete");
                }
            }
        }
        
        public int KeyNo;
        
        public double PrevValue = -1.0;
        
        private bool _updateOnValueChange = true;
        
        private readonly TextObject _nameObj;
        
        private readonly Action<int, float, bool, bool> _updateFace;
        
        private readonly Action _resetSliderPrevValuesCommand;
        
        private readonly Action _addCommand;
        
        private readonly bool _calledFromInit;
        
        private readonly float _initialValue;
        
        private int _tabID = -1;
        
        private string _name;
        
        private float _value;
        
        private float _max;
        
        private float _min;
        
        private bool _isEnabled;
        
        private bool _isDiscrete;
    }
}
