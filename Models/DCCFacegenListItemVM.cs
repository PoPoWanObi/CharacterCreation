using System;
using TaleWorlds.Library;

namespace CharacterCreation.Models
{
    public class DCCFacegenListItemVM : ViewModel
    {
        private void ExecuteAction()
        {
            this._setSelected(this, true);
        }
        
        public DCCFacegenListItemVM(string imagePath, int index, Action<DCCFacegenListItemVM, bool> setSelected)
        {
            this.ImagePath = imagePath;
            this.Index = index;
            this.IsSelected = false;
            this._setSelected = setSelected;
        }
        
        [DataSourceProperty]
        public string ImagePath
        {
            get
            {
                return this._imagePath;
            }
            set
            {
                if (value != this._imagePath)
                {
                    this._imagePath = value;
                    base.OnPropertyChanged("ImagePath");
                }
            }
        }
        
        [DataSourceProperty]
        public bool IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                if (value != this._isSelected)
                {
                    this._isSelected = value;
                    base.OnPropertyChanged("IsSelected");
                }
            }
        }
        
        [DataSourceProperty]
        public int Index
        {
            get
            {
                return this._index;
            }
            set
            {
                if (value != this._index)
                {
                    this._index = value;
                    base.OnPropertyChanged("Index");
                }
            }
        }
        
        private readonly Action<DCCFacegenListItemVM, bool> _setSelected;
        
        private string _imagePath;
        
        private bool _isSelected = true;
        
        private int _index = -1;
    }
}
