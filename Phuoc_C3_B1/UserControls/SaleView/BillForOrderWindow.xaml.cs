using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.Windows;


namespace Phuoc_C3_B1.UserControls.SaleView
{
    public partial class BillForOrderWindow : Window
    {
        public delegate void Save();
        private Save save;


        public Customer Customer { get; set; }
        public ObservableCollection<OrderDetail> OrderDetails { get; set; }
        public int Total { get; set; }


        public BillForOrderWindow(Customer customer, ObservableCollection<OrderDetail> orderDetails, Save saveOrder)
        {
            InitializeComponent();

            save = saveOrder;

            Customer = customer;
            OrderDetails = orderDetails;

            foreach (var item in orderDetails)
            {
                Total += item.Total;
            }

            btn_confirm.Click += Btn_confirm_Click;
            btn_cancel.Click += Btn_cancel_Click;

            this.DataContext = this;
        }


        private void Btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            save();
        }


        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
