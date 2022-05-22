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
    /// Interaction logic for TutorialWindow.xaml
    /// </summary>
    public partial class TutorialWindow : Window
    {
        List<string> images = new List<string>();
        int selected = 0;

        public TutorialWindow()
        {
            InitializeComponent();

            for(int i = 1; i <= 10; i++)
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Icons\\" + i + ".png"))
                {
                    images.Add(System.Windows.Forms.Application.StartupPath + "\\Icons\\" + i + ".png");
                }
            }

            PictureBox.Source = BitmapImageFromFile(images[selected]);
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

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (selected < images.Count-1)
            {
                selected++;
                PictureBox.Source = BitmapImageFromFile(images[selected]);
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (selected  > 0)
            {
                selected--;
                PictureBox.Source = BitmapImageFromFile(images[selected]);
            }
        }
    }
}
