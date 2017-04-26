using GalaSoft.MvvmLight.Command;
using Selling.ClassSql;
using Selling.Infrastructure;
using Selling.Lenguages;
using Selling.Pages;
using Selling.Services;
using Selling.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Selling.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        Administrador Admin;
        public MainViewModel()
        {
            Admin = new Administrador();
            LoadData();
            LoadMenu();
            NewSettings = new SettingsViewModel();
            NewOrder = new OrderViewModel();
            NewCompany = new CompanyViewModel();                       
        }

        #region Properties
        //paramatros paginas que se establecen em las paginas
        public CompanyViewModel NewCompany { get; private set; }
        public OrderViewModel NewOrder { get; private set; }
        public SettingsViewModel NewSettings { get; private set; }
        //listas y objetos que se consultan de la base de datos
        public ObservableCollection<OrderViewModel> Orders {get;set;}
        public SettingsViewModel Settings { get; set; }
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<CompanyViewModel> Companys { get; set; }
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
                    NewOrder = new OrderViewModel();
                    App.Navigator.PushAsync(new NewOrderPage());
                    break;
                case "NewClientsPage":
                    NewCompany = new CompanyViewModel();
                    App.Navigator.PushAsync(new NewClientsPage());
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
            Companys = new ObservableCollection<CompanyViewModel>();
            CallCompanys();
        }

        //paises metodos
        public ICommand CallCommand
        {
            get { return new RelayCommand(CallCompanys); }
        }

        private async void CallCompanys()
        {
            var list = await Admin.apiService.GetAllCompanys();

            Companys.Clear();

            foreach (var item in list)
            {
                Companys.Add(new CompanyViewModel()
                {
                    Title = item.Title,
                    Description = item.Description,
                    Address = item.Address,
                    FechaCreacion = item.FechaCreacion,
                    BrowseCommand = new Command<CompanyViewModel>(BrowseClient)
                });
            }
        }

        private void BrowseClient(CompanyViewModel obj)
        {
            NewCompany = new CompanyViewModel();
            NewCompany.Address = obj.Address;
            NewCompany.Description = obj.Description;
            NewCompany.FechaCreacion = obj.FechaCreacion;
            NewCompany.Title = obj.Title;
            App.Navigator.PushAsync(new NewClientsPage());
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if (_isRefreshing == value)
                    return;

                _isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(RefreshLV); }
        }

        private async void RefreshLV()
        {

            IsRefreshing = true;

            LoadData();

            IsRefreshing = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}

