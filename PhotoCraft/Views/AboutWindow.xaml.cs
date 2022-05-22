using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace PhotoCraft.Views
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            PictureBox.Source = BitmapImageFromFile(System.Windows.Forms.Application.StartupPath + "\\Images\\logo.png");
        }


        private static BitmapImage BitmapImageFromFile(string filepath)
        {
            var bi = new BitmapImage();

            using (var fs = new FileStream(filepath, FileMode.Open))
            {
                bi.BeginInit();
                bi.StreamSource = fs;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
            }

            bi.Freeze();

            return bi;
        }
    }

}
