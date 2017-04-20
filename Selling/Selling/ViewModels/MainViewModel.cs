using GalaSoft.MvvmLight.Command;
using Selling.ClassSql;
using Selling.Infrastructure;
using Selling.Lenguages;
using Selling.Pages;
using Selling.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Selling.ViewModels
{
    public class MainViewModel
    {
        Administrador Admin;
        public MainViewModel()
        {
            Admin = new Administrador();
            LoadData();
            LoadMenu();
            NewSettings = new SettingsViewModel();
            NewOrder = new OrderViewModel();
            NewCountry = new CountryViewModel();                       
        }

        #region Properties
        //paramatros paginas que se establecen em las paginas
        public CountryViewModel NewCountry { get; private set; }
        public OrderViewModel NewOrder { get; private set; }
        public SettingsViewModel NewSettings { get; private set; }
        //listas y objetos que se consultan de la base de datos
        public ObservableCollection<OrderViewModel> Orders {get;set;}
        public SettingsViewModel Settings { get; set; }
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<CountryViewModel> Countries { get; set; }
        #endregion

        #region Commands

        public ICommand GoToCommand
        {
            get { return new RelayCommand<string>(GoTo); }
        }

        private void GoTo(string pageName)
        {
            switch (pageName)
            {
                case "NewOrderPage":
                    App.Navigator.PushAsync(new NewOrderPage());
                    break;
                default:
                    break;
            }
        }

        public ICommand StartCommand
        {
            get { return new RelayCommand(Start); }
        }

        private void Start()
        {
            Admin.navigationService.SetMainPage("MasterPage");
        }

        #endregion

        #region Methods
        public void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();

            Menu.Add(new MenuItemViewModel()
            {
                Icon = "ic_action_orders",
                Title = CalculaTextLenguages("orders"),
                PageName = "MainPage"
            });

            Menu.Add(new MenuItemViewModel()
            {
                Icon = "ic_action_clients",
                Title = CalculaTextLenguages("Clients"),
                PageName = "ClientsPage"
            });

            Menu.Add(new MenuItemViewModel()
            {
                Icon = "ic_action_alarm",
                Title = CalculaTextLenguages("Alarms"),
                PageName = "AlarmsPage"
            });

            Menu.Add(new MenuItemViewModel()
            {
                Icon = "ic_action_settings",
                Title = CalculaTextLenguages("Settings"),
                PageName = "SettingsPage"
            });
        }

        public string CalculaTextLenguages(string value)
        {
            TranslateExtension Translate = new TranslateExtension();
            Translate.Text = value;
            return Convert.ToString( Translate.ProvideValue(null));
        }

        public void LoadData()
        {           
            using (var datos = new DataAcces())
            {
                Orders = new ObservableCollection<OrderViewModel>(datos.GetList(NewOrder));
            };
            using (var datos = new DataAcces())
            {
                Settings =(SettingsViewModel)(datos.GetTop(Settings));
                if (Settings != null)
                {
                    App.Lenguage = Settings.Leguange;
                }
            };
            CallCountries();
        }

        //paises metodos
        public ICommand CallCommand
        {
            get { return new RelayCommand(CallCountries); }
        }

        private async void CallCountries()
        {
            Countries = new ObservableCollection<CountryViewModel>();
            if (Countries.Any())
            {
                //Indicator.IsVisible = false;
            }
            //CallButton.Text = "Calling";
            //IsBusy = true;
            //List.IsVisible = true;
            //Response.Text = string.Empty;

            try
            {
                var result = await Admin.DataService.GetCountries();

                Countries.Clear();

                foreach (var item in result)
                {
                    Countries.Add(new CountryViewModel(item)
                    {
                        FlagSource = ImageSource.FromUri(Admin.DataService.GetFlagSource(item.Alpha2Code)),
                        BrowseCommand = new Command<CountryViewModel>(BrowseCountry)
                    });
                }
                //Response.Text = string.Empty;
                //StatusPanel.IsVisible = false;
            }
            catch (Exception ex)
            {
                //Response.Text = ex.Message;
                //StatusPanel.IsVisible = true;
                //List.IsVisible = false;
            }
            finally
            {
                //IsBusy = false;
                //CallButton.Text = "Refresh";
            }
        }

        private async void BrowseCountry(CountryViewModel obj)
        {
            //await NavigationService.PushAsync<CountryPage>(obj);
        }

        public ICommand StatusCommand
        {
            get { return new RelayCommand(CallPlain); }
        }

        public ICommand SearchCommand
        {
            get { return new RelayCommand(CallSearch); }
        }

        private async void CallPlain()
        {
            try
            {
                await Admin.DataService.GetData();
                //List.IsVisible = false;
                //StatusPanel.IsVisible = true;
            }
            catch (Exception ex)
            {
                //Response.Text = ex.Message;
                //StatusPanel.IsVisible = true;
                //List.IsVisible = false;
            }
            finally
            {
                //IsBusy = false;
                //CallButton.Text = "Refresh";
            }
        }


        private async void CallSearch()
        {
            //var searchPage = new SearchPage();
            //searchPage.Countries = Countries;

            //await NavigationService.PushAsync(searchPage);
        }

        #endregion

    }
}

