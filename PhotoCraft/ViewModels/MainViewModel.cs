using PhotoCraft.Commands;
using PhotoCraft.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;
using PhotoCraft.Functions;
using MaterialDesignThemes.Wpf;
using System.Drawing;
using System.Data.Linq;

namespace PhotoCraft.ViewModels
{
    public class MainViewModel : BaseViewModel 
    {
        private string cropperHeight = "0";

        public string CropperHeight
        {
            get { return cropperHeight; }
            set
            {
                cropperHeight = value;
                OnPropertyChanged(nameof(CropperHeight));
            }
        }

        private string slicerHeight = "0";

        public string SlicerHeight
        {
            get { return slicerHeight; }
            set { slicerHeight = value;
            OnPropertyChanged(nameof(SlicerHeight));
            }
        }



        private string homeHeight = "auto";

        public string HomeHeight
        {
            get { return homeHeight; }
            set
            {
                homeHeight = value;
                OnPropertyChanged(nameof(HomeHeight));
            }
        }


        private string _theme;

        public string Theme
        {
            get { return _theme; }
            set { _theme = value;
                OnPropertyChanged(nameof(Theme));
                if (value == "Default")
                {
                    ChangeTheme(new Uri("/Themes/Default.xaml", UriKind.Relative));
                    ChangeThemeInAppSettings(value);
                }
                else
                {
                    ChangeTheme(new Uri("/Themes/Light.xaml", UriKind.Relative));
                    ChangeThemeInAppSettings(value);
                }

            }
        }


        private readonly MainFunctions mainFunctions = new MainFunctions();
        private readonly ConfigFunctions configFunctions = new ConfigFunctions();

        public ICommand UpdateViewCommand { get; set; }
        public ICommand OpenThemeWindowCommand { get; set; }
        public ICommand OpenTutorialWindowCommand { get; set; }
        public ICommand ChangeThemeCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
            OpenThemeWindowCommand = new RelayCommand(OpenThemeWindow);
            OpenTutorialWindowCommand = new RelayCommand(OpenTutorialWindow);
            ChangeThemeCommand = new RelayCommand(ChangeTheme);
            GetThemes();
        }

        private void OpenTutorialWindow()
        {
            mainFunctions.OpenTutorialWindow(this);
        }

        private async void ChangeTheme()
        {
            if(Theme == "Light")
                await configFunctions.SetValue("Theme", "Default");
            else
                await configFunctions.SetValue("Theme", "Light");
        }

        private async void GetThemes()
        {
            Theme = await configFunctions.GetValue("Theme");
        }

        private void OpenThemeWindow()
        {
            mainFunctions.OpenThemeWindow(this);
        }


        private async void ChangeThemeInAppSettings(string theme)
        {

            if (theme == "Default")
                await configFunctions.SetValue("Theme", "Default");
            else
                await configFunctions.SetValue("Theme", "Light");
        }


        private void ChangeTheme(Uri uri)
        {
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
