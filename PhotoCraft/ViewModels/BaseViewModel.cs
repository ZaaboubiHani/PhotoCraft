using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using PhotoCraft.Models;
using Caliburn.Micro;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace PhotoCraft.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private int _strokeThickness ;

        public virtual int StrokeThickness
        {
            get { return _strokeThickness; }
            set { _strokeThickness = value;
            OnPropertyChanged(nameof(StrokeThickness)); 
            }
        }

        private Color _strokeColor;

        public virtual Color StrokeColor
        {
            get { return _strokeColor; }
            set { _strokeColor = value;
            OnPropertyChanged(nameof(StrokeColor));
            }
        }


        public readonly ProcessType processType;

        public enum ProcessType
        {
            Slice,
            Crop
        }

        private bool cancel = false;

        public bool Cancel
        {
            get { return cancel; }
            set
            {
                cancel = value;
                OnPropertyChanged(nameof(Cancel));
            }
        }

        private BaseModel _base;

        public BaseModel BaseModel
        {
            get { return _base; }
            set
            {
                _base = value;
                OnPropertyChanged(nameof(BaseModel));
            }
        }

        private float progress;

        public float Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }


        private string displayedImagePath;

        public string DisplayedImagePath
        {
            get { return displayedImagePath; }
            set
            {
                displayedImagePath = value;
                OnPropertyChanged(nameof(DisplayedImagePath));
                if(DisplayedImagePath != null)
                {
                    DisplayedImage = new BitmapImage();
                    DisplayedImage.BeginInit();
                    DisplayedImage.CacheOption = BitmapCacheOption.OnLoad;
                    DisplayedImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    DisplayedImage.UriSource = new Uri(DisplayedImagePath);
                    DisplayedImage.EndInit();
                }

            }
        }


        private int selectedIndex;

        public virtual int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));

            }
        }


        private string selectedBoxItem;

        public virtual string SelectedBoxItem
        {
            get { return selectedBoxItem; }
            set
            {
                selectedBoxItem = value;
                OnPropertyChanged(nameof(SelectedBoxItem));
                if (SelectedBoxItem != null && ImagePaths.Count != 0 && selectedIndex >= 0)
                    DisplayedImagePath = ImagePaths[SelectedIndex];

            }
        }

        private bool _IsEnabled;

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                _IsEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private BitmapImage _DisplayedImage;

        public BitmapImage DisplayedImage
        {
            get { return _DisplayedImage; }
            set
            {
                _DisplayedImage = value;
                OnPropertyChanged(nameof(DisplayedImage));
            }
        }

        private string processedImage;

        public string ProcessedImage
        {
            get { return processedImage; }
            set
            {
                processedImage = value;
                OnPropertyChanged(nameof(ProcessedImage));
            }
        }


        

        private string saveLocation;

        public string SaveLocation
        {
            get { return saveLocation; }
            set
            {
                saveLocation = value;
                OnPropertyChanged(nameof(SaveLocation));
                BaseModel.SaveLocation = SaveLocation;
            }
        }


        private List<System.Drawing.Image> images = new List<System.Drawing.Image>();

        public List<System.Drawing.Image> Images
        {
            get { return images; }
            set
            {
                images = value;
                OnPropertyChanged(nameof(Images));

                if (BaseModel.Stacks.Count == 0)
                {
                    for (int i = 0; i < Images?.Count; i++)
                    {
                        BaseModel.Stacks.Add(new BaseModel.Pair(null, Images[i]));
                    }

                }
                else
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        BaseModel.Stacks[i].Image = Images[i];
                    }
                }

                if (BaseModel.Stacks.Count == 0)
                {
                    for (int i = 0; i < SafePaths?.Count; i++)
                    {
                        BaseModel.Stacks.Add(new BaseModel.Pair(SafePaths[i], null));
                    }

                }
                else
                {
                    for (int i = 0; i < SafePaths.Count; i++)
                    {
                        BaseModel.Stacks[i].Name = SafePaths[i];
                    }
                }

            }
        }



        private List<string> imagePaths = new List<string>();

        public List<string> ImagePaths
        {
            get { return imagePaths; }
            set
            {
                imagePaths = value;
                OnPropertyChanged(nameof(ImagePaths));
            }
        }

        private List<string> safePaths = new List<string>();

        public List<string> SafePaths
        {
            get { return safePaths; }
            set
            {
                safePaths = value;
                OnPropertyChanged(nameof(SafePaths));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
