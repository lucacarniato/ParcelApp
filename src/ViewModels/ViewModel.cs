using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ParcelApp.Models;
using ParcelApp.ViewModels.Tools;

namespace ParcelApp.ViewModels
{
    internal sealed class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     The model implementing the business logic
        /// </summary>
        private readonly Model Model;

        /// <summary>
        ///     Deliveries backing field
        /// </summary>
        private IList<string> deliveries;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="model"> The model implementation</param>
        /// <param name="getFilePath">
        ///     The function to open a file dialog and get the path of the file, injected from
        ///     MainWindow.xaml.cs
        /// </param>
        public ViewModel(Model model, Func<string> getFilePath)
        {
            Model = model;
            GetFilePath = getFilePath;
        }

        /// <summary>
        ///     The command binded to the view for triggering a file read
        /// </summary>
        public ICommand ReadParcelsCommand => new Command(_ => ReadParcels());

        /// <summary>
        ///     The Deliveries binded to the view
        /// </summary>
        public IList<string> Deliveries
        {
            get => deliveries;
            set
            {
                deliveries = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The current department binded to the view
        /// </summary>
        public Departments Department
        {
            get => Model.Department;
            set
            {
                Model.Department = value;
                Deliveries = Model.GetDeliveries();
            }
        }


        public IEnumerable<Departments> DepartmentEnumType => Enum.GetValues(typeof(Departments)).Cast<Departments>();

        /// <summary>
        ///     The function to open a file dialog and get the file path
        /// </summary>
        private Func<string> GetFilePath { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     The function used for reading a file, and updating the deliveries
        /// </summary>
        private void ReadParcels()
        {
            var filePath = GetFilePath?.Invoke();
            if (filePath == null) return;
            Model.ReadXmlParcels(filePath);
            Deliveries = Model.GetDeliveries();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}