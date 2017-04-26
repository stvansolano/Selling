using Selling.Services;
using Selling.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Selling.Pages
{
    public partial class ClientsPage : ContentPage
    {
        MainViewModel mainx = new MainViewModel();
        public ClientsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mainx.LoadData();
            List.ItemsSource = mainx.Companys;
        }
    }
}
