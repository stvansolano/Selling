using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Selling.Infrastructure;
using Selling.Model;
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

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Date_Created")]
        public DateTime FechaCreacion { get; set; }

        [JsonProperty("Address")]
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