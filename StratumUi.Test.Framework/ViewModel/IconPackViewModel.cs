using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StratumUi.Test.Framework.ViewModel.Base;
using StratumUi.Wpf.Controls;

namespace StratumUi.Test.Framework.ViewModel
{
    public class IconPackViewModel : BaseViewModel
    {
        private ObservableCollection<EIcons> _icons;

        public ObservableCollection<EIcons> Icons
        {
            get => _icons;
            set => SetField(ref _icons, value);
        }

        private string _search;

        public string Search
        {
            get => _search;
            set
            {
                SetField(ref _search, value);
                LoadIcons(Search);
            }
        }

        public Dictionary<EIcons, string> Kinds2;

        public IconPackViewModel()
        {
            Icons = new ObservableCollection<EIcons>();
            Kinds2 = IconLibrary.IconsDictionary;
            
            LoadIcons();
            
            Search = "";
        }

        private void LoadIcons(string searchText = "")
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                for (var i = 0; i < 170; i++)
                {
                    Icons.Add(Kinds2.Keys.ToList()[i]);
                }
            }
            else
            {
                Icons.Clear();
                var iconsLibrary = Kinds2.Where(x => x.Key.ToString().ToLower().Contains(searchText.ToLower())).ToList();
            
                var max = iconsLibrary.Count > 170 ? 170 : iconsLibrary.Count;
            
                for (int i = 0; i < max; i++)
                {
                    Icons.Add(iconsLibrary[i].Key);
                }
            }
        }
    }
}