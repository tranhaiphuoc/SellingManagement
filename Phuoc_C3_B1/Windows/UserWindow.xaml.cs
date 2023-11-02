using Phuoc_C3_B1.UserControls.SaleView;
using Phuoc_C3_B1.UserControls.SaleView.ViewOrderDetailsBy;
using System.Windows;
using System.Windows.Controls;


namespace Phuoc_C3_B1.Windows
{
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();

            btn_logOut.Click += Btn_logOut_Click;

            lb_Create.SelectionChanged += Lb_Create_SelectionChanged;
            lb_Check.SelectionChanged += Lb_Check_SelectionChanged;
            lb_View.SelectionChanged += Lb_View_SelectionChanged;
        }

        
        private void Btn_logOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }


        private void Lb_Create_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Create.SelectedItem == null) return;

            sp_userControlContent.Children.Clear();
            lb_Check.SelectedItem = null;
            lb_View.SelectedItem = null;

            ListBoxItem item = lb_Create.SelectedItem as ListBoxItem;

            switch (item.Name)
            {
                case "create_order":
                    uc_CreateOrder createReceipt = new uc_CreateOrder();
                    sp_userControlContent.Children.Add(createReceipt);
                    break;
            }
        }


        private void Lb_Check_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Check.SelectedItem == null) return;

            sp_userControlContent.Children.Clear();
            lb_Create.SelectedItem = null;
            lb_View.SelectedItem = null;

            ListBoxItem item = lb_Check.SelectedItem as ListBoxItem;

            switch (item.Name)
            {
                case "check_availableStocks":
                    uc_CheckAvailableStocks availableStocks = new uc_CheckAvailableStocks();
                    sp_userControlContent.Children.Add(availableStocks);
                    break;
            }
        }


        private void Lb_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_View.SelectedItem == null) return;

            sp_userControlContent.Children.Clear();
            lb_Create.SelectedItem = null;
            lb_Check.SelectedItem = null;

            ListBoxItem item = lb_View.SelectedItem as ListBoxItem;

            switch (item.Name)
            {
                case "view_customers":
                    uc_ViewCustomers viewCustomers = new uc_ViewCustomers();
                    sp_userControlContent.Children.Add(viewCustomers);
                    break;
                case "view_availableStocks":
                    uc_ViewAvailableStocks availableStocks = new uc_ViewAvailableStocks();
                    sp_userControlContent.Children.Add(availableStocks);
                    break;
                case "view_orderDetailsByDate":
                    uc_ViewOrderDetailsByDate orderDetailsByDate = new uc_ViewOrderDetailsByDate();
                    sp_userControlContent.Children.Add(orderDetailsByDate);
                    break;
                case "view_orderDetailsByOrderId":
                    uc_ViewOrderDetailsByOrderId orderDetailsByOrderId = new uc_ViewOrderDetailsByOrderId();
                    sp_userControlContent.Children.Add(orderDetailsByOrderId);
                    break;
                case "view_todayRevenue":
                    uc_ViewTodayRevenue todayRevenue = new uc_ViewTodayRevenue();
                    sp_userControlContent.Children.Add(todayRevenue);
                    break;
            }
        }
    }
}
