using PhotoCraft.Models;
using PhotoCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PhotoCraft.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action methodToExecute;
        private Func<bool> canExecuteEvaluator;
        private Action<ProcessView> methodWithParameter;

        public RelayCommand(Action<ProcessView> methodWithParameter, Func<bool> canExecuteEvaluator)
        {
            this.methodWithParameter = methodWithParameter;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }



        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }


        public RelayCommand(Action<ProcessView> methodWithParameter)
        {
            this.methodWithParameter = methodWithParameter;
        }


        public RelayCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }


        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }
        public void Execute(object parameter)
        {
            if (this.methodToExecute != null)
                this.methodToExecute.Invoke();

            if (parameter != null)
                    this.methodWithParameter.Invoke((parameter as ProcessView));
                
            
        }


    }
}
