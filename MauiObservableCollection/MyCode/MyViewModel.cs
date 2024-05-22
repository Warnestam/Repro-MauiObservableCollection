using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MauiObservableCollection.ViewModels
{

    public class MyViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private MyBag fBag;

        public MyViewModel()
        {
            fBag = new MyBag();
            fBag.InitGadgets();
        }



        public MyBag Bag
        {
            get => fBag;
            set
            {
                if (fBag != value)
                {
                    fBag = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
