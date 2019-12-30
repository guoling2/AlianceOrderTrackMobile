using System;
using AlianceOrderTrackMobile.Views;

namespace AlianceOrderTrackMobile.ViewModel
{

    public class MyDashbordExMenuItem
    {
        public MyDashbordExMenuItem()
        {
           // TargetType = typeof(MyDashbordExDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public string TargetTypeString { get; set; }
    }
}