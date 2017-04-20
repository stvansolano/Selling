﻿using GalaSoft.MvvmLight.Command;
using Selling.Infrastructure;
using Selling.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Selling.ViewModels
{
    public class CountryViewModel
    {
        public CountryViewModel()
        {
        }

        public Country Model { get; set; }
        public ImageSource FlagSource { get; set; }
        public ICommand BrowseCommand { get; set; }
        public ObservableCollection<CountryStat> Details { get; set; }
        public ObservableCollection<ImageDetail> Photos { get; set; }
        public string Title { get; private set; }

        public CountryViewModel(Country model)
        {
            Title = "Country: " + model.Name;

            Model = model;

            Details = new ObservableCollection<CountryStat>(new[] {
                new CountryStat("Region", Model.Region),
                new CountryStat("SubRegion", Model.SubRegion),
                new CountryStat("NativeName", Model.NativeName),
                new CountryStat("Population", Model.Population),
                new CountryStat("Code", Model.Alpha2Code)
            });

            Photos = new ObservableCollection<ImageDetail>(new ImageDetail[] {
                new ImageDetail { Image = FlagSource},
                new ImageDetail { Image = FlagSource},
                new ImageDetail { Image = FlagSource}
            });
        }

        internal bool Matches(string searchText)
        {
            return Model != null &&
                (Model.Name ?? string.Empty).ToLowerInvariant().Contains(searchText.ToLowerInvariant());
        }
    }
}