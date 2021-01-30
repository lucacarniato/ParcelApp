﻿using System;
using System.Windows.Input;

namespace ParcelApp.ViewModels.Tools
{
    internal sealed class Command : ICommand
    {
        private readonly Action<object> _action;

        public Command(Action<object> action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}