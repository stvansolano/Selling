using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite.Net;
using System.IO;
using Selling.ViewModels;

namespace Selling.ClassSql
{
    class DataAcces : IDisposable
    {
        private SQLiteConnection conection;
        public DataAcces()
        {
            var config = DependencyService.Get<XPlatform>();
            conection = new SQLiteConnection(config.Plataforma,Path.Combine(config.DirectorioDB,"Selling.db3"));
            //conection.DropTable<SettingsViewModel>();
            conection.CreateTable<OrderViewModel>();
            conection.CreateTable<SettingsViewModel>();
        }

        public void insert<TClass>(TClass[] elementos) where TClass : class
        {
            conection.RunInTransaction(() =>
            {
                foreach (var item in elementos)
                {
                    conection.Insert(item);
                }

            });
        }

        public void Update<TClass>(TClass[] elementos) where TClass : class
        {
            conection.RunInTransaction(() =>
            {
                foreach (var item in elementos)
                {
                    conection.Update(item);
                }

            });
        }

        public void Delete<TClass>(TClass[] elementos) where TClass : class
        {
            conection.RunInTransaction(() =>
            {
                foreach (var item in elementos)
                {
                    conection.Delete(item);
                }

            });
        }

        public void DeleteAll<TClass>() where TClass : class
        {
            conection.DeleteAll<TClass>();
        }

        public TClass GetTop<TClass>(TClass elemento) where TClass : class
        {
            return conection.Table<TClass>().FirstOrDefault();
        }

        public List<TClass> GetList<TClass>(TClass elemento) where TClass : class
        {
            return conection.Table<TClass>().ToList();
        }

        public void Dispose()
        {
            conection.Dispose();
        }
    }
}
