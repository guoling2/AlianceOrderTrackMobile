using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlianceOrderTrackMobile.ViewControl
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    //public partial class BarScanControl : ContentPage
    //{
    //    public BarScanControl()
    //    {
    //        InitializeComponent();
    //    }
    //    //暴露子元素
    //    public string Placeholder
    //    {
    //        get { return this.HandSearchOrder.Placeholder; }
    //        set { this.HandSearchOrder.Placeholder = value; }
    //    }
    //    private void HandSearchOrder_TextChanged(object sender, TextChangedEventArgs e)
    //    {
    //        if (string.IsNullOrWhiteSpace(e.NewTextValue))
    //        {
    //            return;
    //        }
    //        if (e.NewTextValue.Contains("\n"))
    //        {
    //          //  sender as Editor
    //          //  xiehuoViewModel.SearchOrderCommand.Execute(e.OldTextValue);

    //            (sender as Editor).Text = "";


    //        }

    //    }
    //}


    public class BaeScanEdit : Entry
    {
        public static readonly BindableProperty SpCharProperty =
            BindableProperty.Create("SpChar", typeof(string), typeof(BaeScanEdit), null);

        public static readonly BindableProperty ScanResultCommandProperty =
            BindableProperty.Create("ScanResultCommand", typeof(ICommand), typeof(BaeScanEdit), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(BaeScanEdit), null);
        public string SpChar
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }


        public ICommand ScanResultCommand
        {
            get { return (ICommand)GetValue(ScanResultCommandProperty); }
            set { SetValue(ScanResultCommandProperty, value); }
        }
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public BaeScanEdit()
        {
            this.TextChanged += BaeScanEdit_TextChanged;

           
            
        }

        private void BaeScanEdit_TextChanged(object sender, TextChangedEventArgs e)
        {


            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                return;
            }

            if (ScanResultCommand == null)
            {
                return;
            }

            if (e.NewTextValue.Contains("\n"))
            {
                //  xiehuoViewModel.SearchOrderCommand.Execute(e.OldTextValue);
              
                ScanResultCommand.Execute(e.NewTextValue);
                
                try
                {
                   
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                this.Text = "";
                this.Focus();
            }
        }
    }
}