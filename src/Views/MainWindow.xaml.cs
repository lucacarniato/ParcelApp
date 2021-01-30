using Microsoft.Win32;

namespace ParcelApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string GetFilePath()
        {
            var openFileDialog = new OpenFileDialog {Filter = "xml file|*.xml"};
            var showDialogResult = openFileDialog.ShowDialog();

            return showDialogResult.HasValue && showDialogResult.Value ? openFileDialog.FileName : null;
        }
    }
}