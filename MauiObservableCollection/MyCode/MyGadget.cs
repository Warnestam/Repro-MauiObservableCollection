
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiObservableCollection
{
    public class MyGadget : INotifyPropertyChanged
    {

        private MyBag fBag;
        private bool fCanRemove;
        private bool fCanAdd;
        private string fIcon = String.Empty;
        private string fCaption = String.Empty;
        private string fDescription = String.Empty;
        private bool fIsInUse;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanRemove
        {
            get => fCanRemove;
            set
            {
                if (fCanRemove != value)
                {
                    fCanRemove = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CanAdd
        {
            get => fCanAdd;
            set
            {
                if (fCanAdd != value)
                {
                    fCanAdd = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Icon
        {
            get => fIcon;
            set
            {
                if (fIcon != value)
                {
                    fIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Caption
        {
            get => fCaption;
            set
            {
                if (fCaption != value)
                {
                    fCaption = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => fDescription;
            set
            {
                if (fDescription != value)
                {
                    fDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsInUse
        {
            get => fIsInUse;
            set
            {
                if (fIsInUse != value)
                {
                    fIsInUse = value;
                    OnPropertyChanged();
                    OnPropertyChanged("IsNotInUse");
                    OnPropertyChanged("BackgroundColor");
                }
            }
        }

        public bool IsNotInUse
        {
            get => !fIsInUse;
        }

        public Color BackgroundColor
        {
            get
            {
                if (IsInUse)
                {
                    return Colors.Green;
                }
                else
                {
                    return Colors.Red;
                }
            }
        }

        public MyGadget(MyBag bag, string gadget, bool inUse)
        {
            fBag = bag;
            Caption = gadget;
            Description = gadget;
            SetInUse(inUse);
            InitCommands();
        }

        public void SetInUse(bool inUse)
        {
            CanRemove = inUse;
            CanAdd = !inUse;
            IsInUse = inUse;
        }
        
        public ICommand? RemoveCommand { get; set; }
        public ICommand? AddCommand { get; set; }
                
        private void InitCommands()
        {
            RemoveCommand = new Command(() => DoRemove(), () => CanRemove);
            AddCommand = new Command(() => DoAdd(), () => CanAdd);
        }

        private void DoRemove()
        {
            fBag.Remove(this);
        }

        private void DoAdd()
        {
            fBag.Add(this);
        }

    }

}
