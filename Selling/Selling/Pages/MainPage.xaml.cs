using Selling.ClassSql;
using Selling.Services;
using Selling.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Selling.Pages
{
    public partial class MainPage : ContentPage
    {
        MainViewModel mainx = new MainViewModel();
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mainx.LoadData();
            Lista.ItemsSource = mainx.Orders;
        }
    }
}
