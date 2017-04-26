using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Selling.Infrastructure;
using Selling.Model;
using Selling.Pages;
using Selling.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Selling.ViewModel
{
    public class CompanyViewModel
    {
        Administrador Admin;
        public CompanyViewModel()
        {
            Admin = new Administrador();
            FechaCreacion = DateTime.Today;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Address { get; set; }

        public ICommand BrowseCommand { get; set; }

        public ICommand SaveCommand
        {
            get { return new RelayCommand(Save); }
        }

        private async void Save()
        {
            try
            {
                await Admin.apiService.CreateCompany(new Company()
                {
                    Title = this.Title,
                    Description = this.Description,
                    FechaCreacion = this.FechaCreacion,
                    Address = this.Address
                });

                Admin.ComponentesMensajes.Insert("OrderCreated");
                Admin.navigationService.Navigate("ClientsPage");
            }
            catch (Exception e)
            {

                Admin.ComponentesMensajes.Error("Error", e);
            }

        }

    }
}