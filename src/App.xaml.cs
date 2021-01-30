using System.Windows;
using ParcelApp.Models;
using ParcelApp.ViewModels;

namespace ParcelApp
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var model = new Model();
            var orderWindow = new MainWindow();
            var viewModel = new ViewModel(model, orderWindow.GetFilePath);
            orderWindow.DataContext = viewModel;

            orderWindow.Show();
        }
    }
}