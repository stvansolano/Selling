using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite.Net.Attributes;
using System.Collections.ObjectModel;
using Selling.Lenguages;
using Selling.Infrastructure;
using Selling.ClassSql;

namespace Selling.ViewModels
{
    public class OrderViewModel 
    {
        Administrador Admin;
        public OrderViewModel()
        {
            Admin = new Administrador();
            DeliveryDate = DateTime.Today;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryInformation { get; set; }
        public string Client { get; set; }
        public string Phone { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime MinimumDate { get; private set; }

        public ICommand SaveCommand
        {
            get { return new RelayCommand(Save); }
        }

        private void Save()
        {
            try
            {
                if (Admin.ComponentesMensajes.Validar(Title, "Title") ==false){return;}
                if (Admin.ComponentesMensajes.Validar(Client, "Client") == false) { return; }
                if (Admin.ComponentesMensajes.Validar(Description, "Description") == false) { return; }
                Title = this.Title;
                Client = this.Client;                
                DeliveryDate = this.DeliveryDate;
                DeliveryInformation = this.DeliveryInformation;
                Description = this.Description;
                IsDelivered = false;
                using (var datos = new DataAcces())
                {
                    datos.insert(new[] {this});
                    Admin.ComponentesMensajes.Insert("OrderCreated");
                    Admin.navigationService.Navigate("MainPage");
                };
            }
            catch(Exception e)
            {
                Admin.ComponentesMensajes.Error("Error", e);
            }
        }
    }
}
