using Phuoc_C3_B1.Utilities;
using Phuoc_C3_B1.Windows;
using System.Windows;


namespace Phuoc_C3_B1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();
            SeedData.Init();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            this.Close();
        }
    }
}
