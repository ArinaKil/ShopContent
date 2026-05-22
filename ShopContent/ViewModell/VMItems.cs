 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ShopContent.Classes;
using ShopContent.Context;
using ShopContent.Modell;

namespace ShopContent.ViewModell
{
    public class VMItems : INotifyPropertyChanged
    {
        public ObservableCollection<ItemsContext> Items { get; set; }

        public RelayCommand NewItem
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    ItemsContext newModell = new ItemsContext(true);
                    Items.Add(newModell);

                    MainWindow.init.frame.Navigate(new View.Add(newModell));
                });
            }
        }
        public VMItems() =>
            Items = ItemsContext.AllItems();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
