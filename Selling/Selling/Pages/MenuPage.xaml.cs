using Selling.Infrastructure;
using Selling.Lenguages;
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
    public partial class MenuPage : ContentPage
    {
        MainViewModel mainx = new MainViewModel();

        public MenuPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            mainx.LoadMenu();
            Lista.ItemsSource = mainx.Menu;      
        }
    }
}
