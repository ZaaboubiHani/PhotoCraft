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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Ink;

namespace PhotoCraft.Views
{
    /// <summary>
    /// Interaction logic for CropperView.xaml
    /// </summary>
    public partial class CropperView : System.Windows.Controls.UserControl
    {
        

        System.Windows.Point start,end;

        private System.Windows.Point iniP;

        public CropperView()
        {
            InitializeComponent();

            
        }



        private void inkCanvasMeasure_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                
                iniP = e.GetPosition(inkCanvasMeasure);
                start = e.GetPosition(PictureBox);
            }
        }

        private void inkCanvasMeasure_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Draw square
                
                System.Windows.Point endP = e.GetPosition(inkCanvasMeasure);
                List<System.Windows.Point> pointList = new List<System.Windows.Point>
                {
                  new System.Windows.Point(iniP.X, iniP.Y),
                  new System.Windows.Point(iniP.X, endP.Y),
                  new System.Windows.Point(endP.X, endP.Y),
                  new System.Windows.Point(endP.X, iniP.Y),
                  new System.Windows.Point(iniP.X, iniP.Y),
                };

                StylusPointCollection point = new StylusPointCollection(pointList);
                Stroke stroke = new Stroke(point)
                {
                    DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone()
                };
                
                

                ((CropperViewModel)DataContext).InkStrokes.Clear();
                ((CropperViewModel)DataContext).InkStrokes.Add(stroke);
                
            }
        }

        

        private void inkCanvasMeasure_MouseUp(object sender, MouseButtonEventArgs e)
        {
            end = e.GetPosition(PictureBox);

            System.Drawing.Image img = System.Drawing.Image.FromFile(((CropperViewModel)DataContext).DisplayedImagePath);

            float X1 = (float)Math.Min(start.X, end.X);
            float Y1 = (float)Math.Min(start.Y, end.Y);

            float x = (img.Width * (float)X1) / (float)PictureBox.ActualWidth;
            float y = (img.Height * (float)Y1) / (float)PictureBox.ActualHeight;

            float width = (float)Math.Abs(start.X - end.X);
            float height = (float)Math.Abs(start.Y - end.Y);

            float finalWidth = (img.Width / (float)PictureBox.ActualWidth) * width;
            float finalHeight = (img.Height / (float)PictureBox.ActualHeight) * height;

            ((CropperViewModel)DataContext).X = (int)Math.Floor(x);
            ((CropperViewModel)DataContext).Y = (int)Math.Floor(y);
            ((CropperViewModel)DataContext).Width = (int)Math.Floor(finalWidth);
            ((CropperViewModel)DataContext).Height = (int)Math.Floor(finalHeight);
            ((CropperViewModel)DataContext).DisplayPreviewedImage();


        }

        
    }
}
