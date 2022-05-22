using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PhotoCraft.Commands;
using PhotoCraft.ViewModels;
using PhotoCraft.Views;

namespace PhotoCraft
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

            var window = new TutorialWindow
            {
                DataContext = Cropper.DataContext
            };

            window.ShowDialog();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CropperView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(((MainViewModel)DataContext).SlicerHeight != "0")
            {
                var window = new PreviewWindow
                {
                    DataContext = Slicer.DataContext
                };

                window.ShowDialog();
            }
            else if(((MainViewModel)DataContext).CropperHeight != "0")
            {
                var window = new PreviewWindow
                {
                    DataContext = Cropper.DataContext
                };

                window.ShowDialog();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/profile.php?id=100011456821734");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var window = new AboutWindow
            {
                DataContext = this.DataContext
            };

            window.ShowDialog();
        }
    }
}
