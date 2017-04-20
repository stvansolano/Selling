using GalaSoft.MvvmLight.Command;
using Selling.ClassSql;
using Selling.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite.Net.Attributes;
using System.Collections.ObjectModel;
using Selling.Infrastructure;

namespace Selling.ViewModels
{
    public class SettingsViewModel
    {
        public Administrador Admin;
        public SettingsViewModel()
        {
            Admin = new Administrador();
            Leguange = App.Lenguage;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Leguange { get; set; }

        public ICommand SaveSettings
        {
            get { return new RelayCommand(Save); }
        }

        private void Save()
        {
            try
            {
                if (Leguange<0)
                {
                    return;
                }
                Leguange = this.Leguange;
                using (var datos = new DataAcces())
                {
                    datos.DeleteAll<SettingsViewModel>();
                    datos.insert(new[] { this });
                    App.Lenguage = Leguange;
                    Admin.ComponentesMensajes.Insert("SettingCreated");
                    Admin.navigationService.SetMainPage("MasterPage");
                };
            }
            catch(Exception e)
            {
                Admin.ComponentesMensajes.Error("Error", e);
            }
        }
    }
}
