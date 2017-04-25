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
        }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("FechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

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
                    Id = Guid.NewGuid().ToString(),
                    Title = this.Title,
                    Description = this.Description,
                    FechaCreacion = this.FechaCreacion,
                    Address = this.Address
                });

                //await dialogService.ShowMessage("El pedido ha sido creado.", "Informacion");

                //await App.Navigator.PopToRootAsync();

            }
            catch (Exception e)
            {

                Admin.ComponentesMensajes.Error("Error", e);
            }

        }

    }
}