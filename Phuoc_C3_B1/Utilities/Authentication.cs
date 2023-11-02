using Phuoc_C3_B1.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Phuoc_C5_B2
{
    static class Authentication
    {
        public static string Username { get; private set; }
        public static int Role { get; private set; }
        public static bool IsLoggedIn { get; private set; }


        public static void LogIn(string username, int role)
        {
            Username = username;
            Role = role;
            IsLoggedIn = true;
        }


        public static void LogOut()
        {
            Username = "";
            Role = -1;
            IsLoggedIn = false;

            LoginWindow login = new LoginWindow();
            login.Show();

            for (int counter = 1; counter < Application.Current.Windows.Count / 2; counter++)
            {
                Application.Current.Windows[0].Close();
            }
        }
    }
}
