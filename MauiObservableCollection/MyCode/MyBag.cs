using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiObservableCollection
{
    public class MyBag : INotifyPropertyChanged
    {

        private ObservableCollection<MyGadget> fGadgets = new ObservableCollection<MyGadget>();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<MyGadget> Gadgets
        {
            get => fGadgets;
            set
            {
                if (fGadgets != value)
                {
                    fGadgets = value;
                    OnPropertyChanged();
                }
            }
        }

        public void InitGadgets()
        {
            Gadgets.Clear();
            Gadgets.Add(new MyGadget(this, "Gadget 1", true));
            Gadgets.Add(new MyGadget(this, "Gadget 2", true));
            Gadgets.Add(new MyGadget(this, "Gadget 3", false));
            Gadgets.Add(new MyGadget(this, "Gadget 4", false));
            Gadgets.Add(new MyGadget(this, "Gadget 5", false));
            Gadgets.Add(new MyGadget(this, "Gadget 6", false));
        }

        

        public void Remove(MyGadget gadget)
        {
            // Remove a gadget that is in use, at the end of the list (of enabled gadgets)
            gadget.SetInUse(false);
            fGadgets.Remove(gadget);
            int index = GetMaxIndex(false) + 1;
            fGadgets.Insert(index, gadget);
        }

        public void Add(MyGadget gadget)
        {
            // Remove a gadget that is not in use, as the last used gadget
            gadget.SetInUse(true);
            fGadgets.Remove(gadget);
            int index = GetMaxIndex(true) + 1;
            fGadgets.Insert(index, gadget);
        }

        public List<MyGadget> GetSelectedGadgets()
        {
            List<MyGadget> result = new List<MyGadget>();
            foreach (var gadget in Gadgets)
            {
                if (gadget.IsInUse)
                {
                    result.Add(gadget);
                }
            }
            return result;
        }

        private bool IsValidIndex(int index, bool onlyGadgetsInUse)
        {
            bool result = false;
            if (index >= 0 && index <= (fGadgets.Count - 1))
            {
                if (onlyGadgetsInUse)
                {
                    result = fGadgets[index].IsInUse;
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        private bool IsFirstIndex(int index)
        {
            return index == 0;
        }

        private int GetMaxIndex(bool onlyGadgetsInUse)
        {
            int index = fGadgets.Count - 1;
            if (onlyGadgetsInUse)
            {
                int i = 0;
                while (i < fGadgets.Count)
                {
                    if (fGadgets[i].IsInUse)
                    {
                        i++;
                    }
                    else
                    {
                        index = i - 1;
                        break;
                    }
                }
            }
            return index;
        }

        private bool IsLastIndex(int index, bool onlyGadgetsInUse)
        {
            return index == GetMaxIndex(onlyGadgetsInUse);
        }


    }
}
