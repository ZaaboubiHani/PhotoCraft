using PhotoCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhotoCraft.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private readonly MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.ToString() == "Home")
            {

                viewModel.HomeHeight = "auto";
                viewModel.SlicerHeight = "0";
                viewModel.CropperHeight = "0";
                
            }
            else if (parameter.ToString() == "Slicer")
            {
                if (viewModel.SlicerHeight == "0")
                {
                    viewModel.SlicerHeight = "auto";
                    viewModel.HomeHeight = "0";
                    viewModel.CropperHeight = "0";
                }
                else
                {
                    viewModel.SlicerHeight = "0";
                    viewModel.HomeHeight = "auto";
                    viewModel.CropperHeight = "0";
                }
            }
            else if (parameter.ToString() == "Cropper")
            {
                if (viewModel.CropperHeight == "0")
                {
                    viewModel.SlicerHeight = "0";
                    viewModel.HomeHeight = "0";
                    viewModel.CropperHeight = "auto";
                }
                else
                {
                    viewModel.SlicerHeight = "0";
                    viewModel.HomeHeight = "auto";
                    viewModel.CropperHeight = "0";
                }
            }
        }

    }
}
