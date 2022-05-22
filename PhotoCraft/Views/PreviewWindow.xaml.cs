using PhotoCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhotoCraft.Views
{
    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        public PreviewWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (DataContext != null)
                {
                    if (DataContext is CropperViewModel)
                    {

                        ((CropperViewModel)DataContext).drawingAttributes = new DrawingAttributes
                        {
                            Color = System.Windows.Media.Color.FromArgb(
                            colorDialog.Color.A,
                            colorDialog.Color.R,
                            colorDialog.Color.G,
                            colorDialog.Color.B),

                            Width = ((CropperViewModel)DataContext).StrokeThickness,
                            Height = ((CropperViewModel)DataContext).StrokeThickness,
                            StylusTip = StylusTip.Rectangle,
                            //FitToCurve = true,
                            IsHighlighter = false,
                            IgnorePressure = true,

                        };
                        ((CropperViewModel)DataContext).StrokeColor = colorDialog.Color;
                    }
                    if (DataContext is SlicerViewModel)
                    {
                        ((SlicerViewModel)DataContext).StrokeColor = colorDialog.Color;
                    }
                }
            }
        }

        private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (DataContext != null)
            {
                if (DataContext is CropperViewModel)
                {

                    ((CropperViewModel)DataContext).drawingAttributes = new DrawingAttributes
                    {
                        Color = System.Windows.Media.Color.FromArgb(
                        ((CropperViewModel)DataContext).StrokeColor.A,
                        ((CropperViewModel)DataContext).StrokeColor.R,
                        ((CropperViewModel)DataContext).StrokeColor.G,
                        ((CropperViewModel)DataContext).StrokeColor.B),

                        Width = (int)Slider.Value,
                        Height = (int)Slider.Value,
                        StylusTip = StylusTip.Rectangle,
                        //FitToCurve = true,
                        IsHighlighter = false,
                        IgnorePressure = true,

                    };
                    ((CropperViewModel)DataContext).StrokeThickness = (int)Slider.Value;
                }
                if (DataContext is SlicerViewModel)
                {
                    ((SlicerViewModel)DataContext).StrokeThickness = (int)Slider.Value;
                }
            }
        }
    }
}
