using Caliburn.Micro;
using PhotoCraft.Commands;
using PhotoCraft.Functions;
using PhotoCraft.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Ink;

namespace PhotoCraft.ViewModels
{
    public class CropperViewModel : BaseViewModel, IDisposable
    {
        private int _strokeThickness ;

        public override int StrokeThickness
        {
            get { return _strokeThickness; }
            set
            {
                _strokeThickness = value;
                OnPropertyChanged(nameof(StrokeThickness));
                drawingAttributes = new DrawingAttributes
                {
                    Color = System.Windows.Media.Color.FromArgb(
                    StrokeColor.A,
                    StrokeColor.R,
                    StrokeColor.G,
                    StrokeColor.B),
                    Width = value,
                    Height = value,
                    StylusTip = StylusTip.Rectangle,
                    IsHighlighter = false,
                    IgnorePressure = true,

                };

            }
        }

        private System.Drawing.Color _strokeColor;

        public override System.Drawing.Color StrokeColor
        {
            get { return _strokeColor; }
            set
            {
                _strokeColor = value;
                OnPropertyChanged(nameof(StrokeColor));
                drawingAttributes = new DrawingAttributes
                {
                    Color = System.Windows.Media.Color.FromArgb(
                    value.A,
                    value.R,
                    value.G,
                    value.B),

                    Width = StrokeThickness,
                    Height = StrokeThickness,
                    StylusTip = StylusTip.Rectangle,
                    IsHighlighter = false,
                    IgnorePressure = true,

                };

            }
        }

        private DrawingAttributes _drawingAttributes;

        public DrawingAttributes drawingAttributes
        {
            get { return _drawingAttributes; }
            set { _drawingAttributes = value; 
            OnPropertyChanged(nameof(drawingAttributes));
            }
        }


        private StrokeCollection _InkStrokes;
        public StrokeCollection InkStrokes
        {
            get { return _InkStrokes; }
            set { _InkStrokes = value; 
            OnPropertyChanged(nameof(InkStrokes));
            }
        }

        private int selectedIndex;

        public override int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                DisplayPreviewedImage();
            }
        }

        private string selectedBoxItem;

        public override string SelectedBoxItem
        {
            get { return selectedBoxItem; }
            set
            {
                selectedBoxItem = value;
                OnPropertyChanged(nameof(SelectedBoxItem));
                if (SelectedBoxItem != null && ImagePaths.Count != 0 && ImagePaths.Count != 0 && SelectedIndex >= 0)
                    DisplayedImagePath = ImagePaths[SelectedIndex];
                DisplayPreviewedImage();
            }
        }

        private int _X;

        public int X
        {
            get { return _X; }
            set { _X = value;
            OnPropertyChanged(nameof(X));
                
            }
        }



        private int _Y;

        public int Y
        {
            get { return _Y; }
            set { _Y = value;
            OnPropertyChanged(nameof(Y));
                
            }
        }

        private int _Width;

        public int Width
        {
            get { return _Width; }
            set { _Width = value; 
            OnPropertyChanged(nameof(Width));
               
            }
        }

        private int _Height;

        public int Height
        {
            get { return _Height; }
            set { _Height = value; 
            OnPropertyChanged(nameof(Height));
                
            }
        }


        private string outputName;

        public string OutputName
        {
            get { return outputName; }
            set
            {
                outputName = value;
                OnPropertyChanged(nameof(OutputName));
                (BaseModel as CropperModel).OutputName = OutputName;
            }
        }

        private BitmapImage _PreviewedImage;

        public BitmapImage PreviewedImage
        {
            get { return _PreviewedImage; }
            set
            {
                _PreviewedImage = value;
                OnPropertyChanged(nameof(PreviewedImage));
            }
        }


        public new readonly ProcessType processType = ProcessType.Crop;

        private readonly CropperFunctions cropperFunctions = new CropperFunctions();

        public ICommand BrowseCommand { get; set; }
        public ICommand SaveLocationCommand { get; set; }
        public ICommand StartProcessCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand CancelationCommand { get; set; }
        public ICommand ProcessCommand { get; set; }

        public CropperViewModel()
        {
            BaseModel = new CropperModel();

            BrowseCommand = new RelayCommand(Browse);
            SaveLocationCommand = new RelayCommand(Save);
            StartProcessCommand = new RelayCommand(OpenProcessWindow);
            ResetCommand = new RelayCommand(ResetInputs);
            ProcessCommand = new RelayCommand(Cropping);
            CancelationCommand = new RelayCommand(Cancelation);

            StrokeThickness = 1;
            StrokeColor = Color.White;

            drawingAttributes = new DrawingAttributes
            {
                Color = System.Windows.Media.Color.FromArgb(
                    StrokeColor.A,
                    StrokeColor.R,
                    StrokeColor.G,
                    StrokeColor.B),

                Width = StrokeThickness,
                Height =StrokeThickness,
                StylusTip = StylusTip.Rectangle,
                IsHighlighter = false,
                IgnorePressure = true,

            };

            InkStrokes = new StrokeCollection();

        }

        private async void Cropping(ProcessView window)
        {
            await cropperFunctions.Crop((BaseModel as CropperModel), this);
        }

            private void Cancelation()
        {
            cropperFunctions.Cancelation(this);
        }

        private async void ResetInputs()
        {
            await cropperFunctions.ResetCropperInputs(this);
        }

        
        public void DisplayPreviewedImage()
        {
            if(DisplayedImagePath != null && Width > 0 && Height > 0)
            {
                Bitmap img = (Bitmap)System.Drawing.Image.FromFile(DisplayedImagePath);
                if(Width <= img.Width-X && Height <= img.Height-Y )
                {
                    System.Drawing.Image image;
                    using (Graphics G = Graphics.FromImage(img))
                    {
                        var rectangle = new System.Drawing.Rectangle(Math.Abs(X), Math.Abs(Y), Math.Abs(Width), Math.Abs(Height));
                        image = new Bitmap(Math.Abs(Width), Math.Abs(Height));

                        image = img.Clone(rectangle, img.PixelFormat);
                        img.Dispose();
                    }
                    

                    string CashDir = System.Windows.Forms.Application.StartupPath + "\\Cash";

                    if (!Directory.Exists(CashDir))
                        Directory.CreateDirectory(CashDir);

                    string PreviewImagePath = CashDir + "\\Preview.jpg";

                    image.Save(PreviewImagePath);

                    PreviewedImage = BitmapImageFromFile(PreviewImagePath);

                    image.Dispose();
                }
            }
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

        private void Save()
        {
            cropperFunctions.SaveLocation(this);
        }

        private void Browse()
        {
            cropperFunctions.BrowseImages(this);
        }

        private void OpenProcessWindow()
        {
            cropperFunctions.OpenProcessWindow(this);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
