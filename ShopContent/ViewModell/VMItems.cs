using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopContent.Context;
using ShopContent.Modell;

namespace ShopContent.ViewModell
{
    public class VMItems
    {
        public ObservableCollection<ItemsContext> Items { get; set; }

    }
}
