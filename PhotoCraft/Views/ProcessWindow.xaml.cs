using System;
using System.Collections.Generic;
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

namespace PhotoCraft.ViewModels
{
    /// <summary>
    /// Interaction logic for ProcessWindow.xaml
    /// </summary>
    public partial class ProcessView : Window
    {
        public ProcessView()
        {
            InitializeComponent();
        }

        private void TextBox_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }
    }
}
