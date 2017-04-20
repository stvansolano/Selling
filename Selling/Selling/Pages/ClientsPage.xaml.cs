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
            List.ItemTapped += (sender, e) =>
            {
                var viewModel = ((ListView)sender).BindingContext as CountryViewModel;

                if (viewModel != null && viewModel.BrowseCommand != null)
                {
                    viewModel.BrowseCommand.Execute(((ListView)sender).BindingContext);
                }
            };
            //CallButton.Text = "Calling";
            //IsBusy = true;
            //List.IsVisible = true;
            //Response.Text = string.Empty;
            //StatusPanel.IsVisible = false;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            mainx.LoadData();
            List.ItemsSource = mainx.Countries;
        }


    }
}
