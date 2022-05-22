using PhotoCraft.Models;
using PhotoCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PhotoCraft.Functions
{
    public class SlicerFunctions : BaseFunctions
    {
        public SlicerFunctions()
        {

        }

        public async Task SliceHorizontal(SlicerModel slicer,SlicerViewModel slicerView)
        {
            await Task.Run(() =>
            {   
                try
                {
                    int counter = 1;
                    foreach (BaseModel.Pair image in slicer.Stacks)
                    {
                        Bitmap bitmap = (Bitmap)image.Image;

                        int startX = 0, startY = 0, Width = 0, Height = 0;
                        slicerView.ProcessedImage = image.Name;
                        for (int i = 0; i < slicer.SlicesNumber; i++)
                        {
                            startX = 0;
                            startY = (int)(Math.Floor((decimal)(bitmap.Height / slicer.SlicesNumber)) * i);
                            Width = bitmap.Width;
                            Height = (int)(Math.Floor((decimal)(bitmap.Height / slicer.SlicesNumber)));
                            Rectangle rectangle = new Rectangle(startX, startY, Width, Height);
                            Bitmap slicedBit = new Bitmap(Width, Height);
                            string FilePath = slicer.SaveLocation + "\\" + slicer.OutputName + "_" + counter + ".jpg";

                            slicedBit = bitmap.Clone(rectangle, System.Drawing.Imaging.PixelFormat.DontCare);
                            slicedBit.Save(FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            slicedBit.Dispose();

                            counter++;
                        }
                        slicerView.Progress += 100.0f / slicer.Stacks.Count;
                        Thread.Sleep(100);
                        bitmap.Dispose();
                        if (slicerView.Cancel)
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            MessageBox.Show("Your process is complete", "Process", MessageBoxButton.OK, MessageBoxImage.Information);
            slicerView.Cancel = false;
            slicerView.Progress = 0;
            Process.Start(slicer.SaveLocation);
            await ResetSlicerInputs(slicerView);
        }

        public async Task SliceVertical(SlicerModel slicer, SlicerViewModel slicerView)
        {
            await Task.Run(() =>
            {
                try
                {
                    int counter = 1;
                    foreach (BaseModel.Pair image in slicer.Stacks)
                    {
                        Bitmap bitmap = (Bitmap)image.Image;

                        int startX = 0, startY = 0, Width = 0, Height = 0;
                        slicerView.ProcessedImage = image.Name;
                        for (int i = 0; i < slicer.SlicesNumber; i++)
                        {
                            startX = (int)(Math.Floor((decimal)(bitmap.Width / slicer.SlicesNumber)) * i);
                            startY = 0;
                            Width = (int)(Math.Floor((decimal)(bitmap.Width / slicer.SlicesNumber)));
                            Height = bitmap.Height;
                            Rectangle rectangle = new Rectangle(startX, startY, Width, Height);
                            Bitmap slicedBit = new Bitmap(Width, Height);
                            string FilePath = slicer.SaveLocation + "\\" + slicer.OutputName + "_" + counter + ".jpg";

                            slicedBit = bitmap.Clone(rectangle, System.Drawing.Imaging.PixelFormat.DontCare);
                            slicedBit.Save(FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            slicedBit.Dispose();

                            counter++;
                        }
                        slicerView.Progress += 100.0f / slicer.Stacks.Count;
                        Thread.Sleep(100);
                        bitmap.Dispose();
                        if (slicerView.Cancel)
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            MessageBox.Show("Your process is complete", "Process", MessageBoxButton.OK, MessageBoxImage.Information);
            slicerView.Cancel = false;
            slicerView.Progress = 0;
            Process.Start(slicer.SaveLocation);
            await ResetSlicerInputs(slicerView);

        }

        public async Task ResetSlicerInputs(SlicerViewModel slicerViewModel)
        {
            await Task.Run(() =>
            {
                try
                {
                    slicerViewModel.SlicesNumber = 0;
                    slicerViewModel.OutputName = null;
                    slicerViewModel.SaveLocation = null;
                    slicerViewModel.Images = new List<System.Drawing.Image>();
                    slicerViewModel.ImagePaths = new List<string>();
                    slicerViewModel.SafePaths.Clear();
                    slicerViewModel.SafePaths = new List<string>();
                    (slicerViewModel.BaseModel as SlicerModel).Stacks = new List<BaseModel.Pair>();
                    slicerViewModel.SlicingAngle = null;
                    slicerViewModel.SelectedIndex = 0;
                    slicerViewModel.SelectedBoxItem = null;
                    slicerViewModel.DisplayedImage = new BitmapImage();
                    slicerViewModel.DisplayedImage.Freeze();
                    slicerViewModel.DisplayedImagePath = null;
                    slicerViewModel.IsEnabled = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
