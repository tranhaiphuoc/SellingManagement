using Phuoc_C3_B1.Models;
using Phuoc_C5_B2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Phuoc_C3_B1.Windows
{
    public partial class LoginWindow : Window
    {
        private static readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public LoginWindow()
        {
            InitializeComponent();

            btn_login.Click += Btn_login_Submit;
        }

        private bool IsValid(string username, string password)
        {
            if (username == "")
            {
                MessageBox.Show("Username field is required");
                return false;
            }

            if (password == "")
            {
                MessageBox.Show("Password field is required");
                return false;
            }

            return true;
        }

        private void Redirect(Account account)
        {
            switch (account.Role)
            {
                case 1:
                    Authentication.LogIn(account.Username, account.Role);
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Close();
                    break;

                case 2:
                    Authentication.LogIn(account.Username, account.Role);
                    UserWindow sellerWindow = new UserWindow();
                    sellerWindow.Show();
                    this.Close();
                    break;
            }
        }

        private void Btn_login_Submit(object sender, RoutedEventArgs e)
        {
            string username = txt_box_username.Text;
            string password = pwd_box_password.Password;

            if (IsValid(username, password))
            {
                Account account = _unitOfWork.Accounts.GetByLogIn(username, password);

                if (account == null)
                {
                    MessageBox.Show("Wrong username or password");
                    return;
                }

                Redirect(account);
            }
        }
    }
}
