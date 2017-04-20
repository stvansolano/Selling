using Selling.Lenguages;
using Selling.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selling.Infrastructure
{
    public class Administrador
    {
        public NavigationService navigationService;
        public ComponentesMensajes ComponentesMensajes;
        public CountryService DataService;

        public Administrador()
        {
            ComponentesMensajes = new ComponentesMensajes();
            navigationService = new NavigationService();
            DataService = new CountryService();
        }
    }
}
