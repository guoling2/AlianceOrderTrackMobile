using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinSharedLibrary.Model;

namespace AlianceOrderTrackMobile.ViewModel
{
   public class MissionDashbordViewModel :ViewModelBase
    {
        public MissionDashbordViewModel(INavigation navigation) : base(navigation)
        {
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var _item = item as Item;
            //    Items.Add(_item);
            //    await DataStore.AddItemAsync(_item);
            //});
        }
    }
}
