using Caliburn.Micro;
using PhotoCraft.Models;
using PhotoCraft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PhotoCraft.Functions
{
    public class BaseFunctions
    {
        public BaseFunctions()
        {
        }

        public void SaveLocation(BaseViewModel baseView)
        {
            using (FolderBrowserDialog FolderDialog = new FolderBrowserDialog())
            {
                if (FolderDialog.ShowDialog() == DialogResult.OK)
                {
                    if(FolderDialog.SelectedPath.EndsWith("\\"))
                        baseView.SaveLocation = FolderDialog.SelectedPath.Remove(FolderDialog.SelectedPath.Length - 1);
                    else
                        baseView.SaveLocation = FolderDialog.SelectedPath;
                }
            }
        }

        public void BrowseImages(BaseViewModel baseView)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
                BindableCollection<string> comboBox = new BindableCollection<string>();
                List<System.Drawing.Image> imageList = new List<System.Drawing.Image>();
                List<string> SafeNames = new List<string>();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                   
                    baseView.Images = new List<System.Drawing.Image>();
                    baseView.ImagePaths = new List<string>();
                    baseView.SafePaths = new List<string>();
                    baseView.SelectedIndex = 0;
                     
                    baseView.BaseModel.Stacks.Clear();

                    if (openFileDialog.FileNames.Length > 100)
                        MessageBox.Show("The number of imported images is over 100, please import lesser number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        foreach (string file in openFileDialog.FileNames)
                        {
                            imageList.Add(System.Drawing.Image.FromFile(file));
                            baseView.ImagePaths.Add(file);
                        }
                        foreach (string file in openFileDialog.SafeFileNames)
                        {
                            
                            SafeNames.Add(file);
                        }
                        baseView.DisplayedImagePath = openFileDialog.FileNames[0];
                    }
                }
                baseView.SafePaths = SafeNames;
                if (baseView.SelectedBoxItem != null && baseView.ImagePaths.Count != 0 && baseView.SelectedIndex >= 0)
                    baseView.DisplayedImagePath = baseView.ImagePaths[baseView.SelectedIndex];
                baseView.Images = imageList;
                baseView.IsEnabled = true;
            }
        }

        public void Cancelation(BaseViewModel baseView)
        {
            
            if(MessageBox.Show("Are you sure you want to cancel processing?","Process",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                baseView.Cancel = true;
           
        }

        public void OpenProcessWindow(BaseViewModel baseView)
        {
            var window = new ProcessView
            {
                DataContext = baseView
            };

            if (baseView.Images.Count == 0)
                MessageBox.Show("You haven't imported any image, please import at least one image using the \"Browse\" Button","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            if(baseView.SaveLocation == null)
                MessageBox.Show("You haven't selected a save location, please choose one using the \"Save\" Button", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                window.ShowDialog();
        }


    }
}
