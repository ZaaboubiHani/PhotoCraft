using System;
using PhotoCraft.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using PhotoCraft.Models;
using System.Windows.Input;
using System.Windows.Controls;
using PhotoCraft.Functions;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Drawing.Imaging;

namespace PhotoCraft.ViewModels
{
    public class SlicerViewModel : BaseViewModel,IDisposable
    {

        private int _strokeThickness;

        public override int StrokeThickness
        {
            get { return _strokeThickness; }
            set
            {
                _strokeThickness = value;
                OnPropertyChanged(nameof(StrokeThickness));
                DiplayImagePreview();
            }
        }

        private System.Drawing.Color _strokeColor ;

        public override System.Drawing.Color StrokeColor
        {
            get { return _strokeColor; }
            set
            {
                _strokeColor = value;
                OnPropertyChanged(nameof(StrokeColor));
                DiplayImagePreview();
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
                DiplayImagePreview();
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
                DiplayImagePreview();
            }
        }

        private string outputName;

        public string OutputName {
            get { return outputName; }
            set
            {
                outputName = value;
                OnPropertyChanged(nameof(OutputName));
                (BaseModel as SlicerModel).OutputName = OutputName;
            }
        }

        private ComboBoxItem slicingAngle;

        public ComboBoxItem SlicingAngle
        {
            get { return slicingAngle; }
            set
            {
                slicingAngle = value;
                OnPropertyChanged(nameof(SlicingAngle));
                (BaseModel as SlicerModel).SlicingAngle = (slicingAngle?.Content.ToString() == "Horizontal") ? SlicerModel.Angle.Horizontal : SlicerModel.Angle.Vertical;
                DiplayImagePreview();

            }
        }

        private int slicesNumber;

        public int SlicesNumber
        {
            get { return slicesNumber; }
            set
            {
                slicesNumber = value;
                OnPropertyChanged(nameof(SlicesNumber));

                (BaseModel as SlicerModel).SlicesNumber = SlicesNumber;
                DiplayImagePreview();
            }
        }


        public new readonly ProcessType processType = ProcessType.Slice;

        private readonly SlicerFunctions slicerFunctions = new SlicerFunctions();

        public ICommand BrowseCommand { get; set; }
        public ICommand SaveLocationCommand { get; set; }
        public ICommand StartProcessCommand { get; set; }
        public ICommand ProcessCommand { get; set; }
        public ICommand CancelationCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand DrawSlicesCommand { get; set; }



        public SlicerViewModel()
        {
            BaseModel = new SlicerModel();

            StrokeColor = System.Drawing.Color.White;
            
            BrowseCommand = new RelayCommand(Browse);
            SaveLocationCommand = new RelayCommand(Save);
            StartProcessCommand = new RelayCommand(OpenProcessWindow);
            ResetCommand = new RelayCommand(ResetInputs);
            ProcessCommand = new RelayCommand(Slice);
            CancelationCommand = new RelayCommand(Cancelation);
        }


        private void Cancelation()
        {
            slicerFunctions.Cancelation(this);
        }

        private async void ResetInputs()
        {
            await slicerFunctions.ResetSlicerInputs(this);
        }

        private void OpenProcessWindow()
        {
            slicerFunctions.OpenProcessWindow(this);
        }

        private void Save()
        {
            slicerFunctions.SaveLocation(this);
        }

        private void Browse()
        {
            slicerFunctions.BrowseImages(this);
            SelectedIndex = 0;
        }

        private void DiplayImagePreview()
        {
            if(DisplayedImage != null && DisplayedImagePath != null && SlicesNumber > 1)
            {
                System.Drawing.Image image = DrawSlices(DisplayedImagePath,SlicesNumber,(BaseModel as SlicerModel).SlicingAngle,StrokeThickness,StrokeColor);

                string CashDir = Application.StartupPath + "\\Cash";

                if (!Directory.Exists(CashDir))
                    Directory.CreateDirectory(CashDir);

                string PreviewImagePath = CashDir + "\\Preview.jpg";
                
                image.Save(PreviewImagePath);

                DisplayedImage = BitmapImageFromFile(PreviewImagePath);

                image.Dispose();
            }
        }

        private static System.Drawing.Image DrawSlices(string path, int num,SlicerModel.Angle angle,int thick,System.Drawing.Color color)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);

            using (Graphics G = Graphics.FromImage(img))
            {
                System.Drawing.Pen pen = new System.Drawing.Pen(color, thick);

                if(angle == SlicerModel.Angle.Vertical)
                    for (int i = 1; i < num; i++)
                    {
                        int x1 = (img.Width / num) * i;
                        int y1 = 0;
                        int x2 = (img.Width / num) * i;
                        int y2 = img.Height;

                        G.DrawLine(pen, x1, y1, x2, y2);
                    }
                else
                    for (int i = 1; i < num; i++)
                    {
                        int x1 = 0;
                        int y1 = (img.Height/num) * i;
                        int x2 = img.Width;
                        int y2 = (img.Height / num) * i;

                        G.DrawLine(pen, x1, y1, x2, y2);
                    }
            }
            return img;
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


        private async void Slice(ProcessView window)
        {
            if ((BaseModel as SlicerModel).SlicingAngle == SlicerModel.Angle.Horizontal)
                await slicerFunctions.SliceHorizontal((BaseModel as SlicerModel), this);
            if((BaseModel as SlicerModel).SlicingAngle == SlicerModel.Angle.Vertical)
                await slicerFunctions.SliceVertical((BaseModel as SlicerModel), this);
            if (window != null)
                window.Close();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
