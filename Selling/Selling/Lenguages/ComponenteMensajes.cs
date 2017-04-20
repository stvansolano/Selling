using GalaSoft.MvvmLight.Command;
using Selling.ClassSql;
using Selling.Pages;
using Selling.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Selling.Lenguages
{
    public class ComponentesMensajes
    {
        DialogService dialogService=new DialogService();
        TranslateExtension Translate = new TranslateExtension();

        public void Insert(String sentencia)
        {
            MuestraMensaje(CalculaTextLenguages(sentencia), "Informacion");
        }
        public void Error(String sentencia, Exception e)
        {
            MuestraMensaje(CalculaTextLenguages(sentencia), "Error");
        }

        public bool Validar(object control,string value)
        {
            bool xvalidar = true;
            //if (control.GetType().ToString() == "string")
            //{
                if (string.IsNullOrEmpty((string)control))
                {
                    xvalidar = false;
                }
            //}
            if(xvalidar== false){
                string xnombre1 = CalculaTextLenguages("PregValidar");
                string xnombre2 = CalculaTextLenguages(value);
                MuestraMensaje(xnombre1+xnombre2, "Informacion");
            }
            return xvalidar;
        }

        public async void MuestraMensaje(string value,string tipo)
        {
            await dialogService.ShowMessage(value, tipo);
        }

        public string CalculaTextLenguages(string value)
        {
            Translate.Text = value;
            return Convert.ToString(Translate.ProvideValue(null));
        }
    }
}