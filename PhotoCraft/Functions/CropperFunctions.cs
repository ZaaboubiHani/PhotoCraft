using PhotoCraft.Models;
using PhotoCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PhotoCraft.Functions
{
    public class CropperFunctions : BaseFunctions
    {
        public CropperFunctions()
        {

        }


        public void DisplayPreviewedImage()
        {
            
        }


        public async Task Crop(CropperModel cropper, CropperViewModel cropperView)
        {
            await Task.Run(() =>
            {
                try
                {
                    int counter = 1;
                    foreach (BaseModel.Pair image in cropper.Stacks)
                    {
                        Bitmap bitmap = (Bitmap)image.Image;
                        cropperView.ProcessedImage = image.Name;

                        if (image != null && cropperView.Width > 0 && cropperView.Height > 0)
                        {
                            
                            if (cropperView.Width <= bitmap.Width-cropperView.X && cropperView.Height <= bitmap.Height-cropperView.Y && cropperView.X < bitmap.Width && cropperView.Y < bitmap.Height)
                            {
                                System.Drawing.Image outputImage;
                                using (Graphics G = Graphics.FromImage(bitmap))
                                {
                                    var rectangle = new System.Drawing.Rectangle(Math.Abs(cropperView.X), Math.Abs(cropperView.Y), Math.Abs(cropperView.Width), Math.Abs(cropperView.Height));
                                    outputImage = new Bitmap(Math.Abs(cropperView.Width), Math.Abs(cropperView.Height));

                                    outputImage = bitmap.Clone(rectangle, bitmap.PixelFormat);
                                    bitmap.Dispose();
                                }

                                string FilePath = cropper.SaveLocation + "\\" + cropper.OutputName + "_" + counter + ".jpg";
                                outputImage.Save(FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                outputImage.Dispose();
                                counter++;
                            }
                            else 
                            {
                                int Yholder = cropperView.Y, Xholder = cropperView.X, WidthHolder = cropperView.Width,HeightHolder = cropperView.Height;
                                if (cropperView.Y > bitmap.Height)
                                    Yholder = bitmap.Height;
                                if (cropperView.X > bitmap.Width)
                                    Xholder = bitmap.Width;
                                if (cropperView.Height > bitmap.Height-cropperView.Y)
                                    HeightHolder = bitmap.Height;
                                if (cropperView.Width > bitmap.Width-cropperView.X)
                                    WidthHolder = bitmap.Width;

                                System.Drawing.Image outputImage;
                                using (Graphics G = Graphics.FromImage(bitmap))
                                {
                                    var rectangle = new System.Drawing.Rectangle(Math.Abs(Xholder), Math.Abs(Yholder), Math.Abs(WidthHolder), Math.Abs(HeightHolder));
                                    outputImage = new Bitmap(Math.Abs(WidthHolder), Math.Abs(HeightHolder));

                                    outputImage = bitmap.Clone(rectangle, bitmap.PixelFormat);
                                    bitmap.Dispose();
                                }

                                string FilePath = cropper.SaveLocation + "\\" + cropper.OutputName + "_" + counter + ".jpg";
                                outputImage.Save(FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                outputImage.Dispose();
                                counter++;
                            }
                        }

                        cropperView.Progress += 100.0f / cropper.Stacks.Count;
                        Thread.Sleep(100);
                        bitmap.Dispose();
                        if (cropperView.Cancel)
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            MessageBox.Show("Your process is complete", "Process", MessageBoxButton.OK, MessageBoxImage.Information);
            cropperView.Cancel = false;
            cropperView.Progress = 0;
            Process.Start(cropper.SaveLocation);
        }

        public async Task ResetCropperInputs(CropperViewModel cropperViewModel)
        {
            await Task.Run(() =>
            {
                try
                {
                    cropperViewModel.X = 0;
                    cropperViewModel.Y = 0;
                    cropperViewModel.Width = 0;
                    cropperViewModel.Height = 0;
                    cropperViewModel.OutputName = null;
                    cropperViewModel.SaveLocation = null;
                    cropperViewModel.Images = new List<System.Drawing.Image>();
                    cropperViewModel.ImagePaths = new List<string>();
                    cropperViewModel.SafePaths.Clear();
                    cropperViewModel.SafePaths = new List<string>();
                    (cropperViewModel.BaseModel as CropperModel).Stacks = new List<BaseModel.Pair>();
                    cropperViewModel.SelectedIndex = 0;
                    cropperViewModel.SelectedBoxItem = null;
                    cropperViewModel.DisplayedImage = new BitmapImage();
                    cropperViewModel.DisplayedImage.Freeze();
                    cropperViewModel.PreviewedImage = new BitmapImage();
                    cropperViewModel.PreviewedImage.Freeze();
                    cropperViewModel.DisplayedImagePath = null;
                    cropperViewModel.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
