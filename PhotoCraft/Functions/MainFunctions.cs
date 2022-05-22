using PhotoCraft.ViewModels;
using PhotoCraft.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoCraft.Functions
{
    public class MainFunctions
    {
        public MainFunctions()
        {

        }

        public void OpenThemeWindow(BaseViewModel baseView)
        {
            var window = new ThemeWindow
            {
                DataContext = baseView
            };

            window.ShowDialog();
        }
        public void OpenTutorialWindow(BaseViewModel baseView)
        {
            var window = new TutorialWindow
            {
                DataContext = baseView
            };

            window.ShowDialog();
        }
        public void OpenPreviewWindow(BaseViewModel baseView)
        {
            var window = new PreviewWindow
            {
                DataContext = baseView
            };

            window.ShowDialog();
        }
    }
}
