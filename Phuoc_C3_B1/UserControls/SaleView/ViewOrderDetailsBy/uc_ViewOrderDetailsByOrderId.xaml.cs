using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Phuoc_C3_B1.UserControls.SaleView.ViewOrderDetailsBy
{
    public partial class uc_ViewOrderDetailsByOrderId : UserControl
    {
        private IOrderService _service = new OrderService();


        public uc_ViewOrderDetailsByOrderId()
        {
            InitializeComponent();

            tb_orderId.PreviewTextInput += Tb_orderId_PreviewTextInput;
            btn_filter.Click += Btn_filter_Click; ;
        }

        
        private void Tb_orderId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void Btn_filter_Click(object sender, RoutedEventArgs e)
        {
            uc_details.Children.Clear();

            if (string.IsNullOrEmpty(tb_orderId.Text))
            {
                MessageBox.Show("Please enter an id.");
                return;
            }

            Order temp = _service.GetById("HD" + tb_orderId.Text.Trim());

            if (temp == null)
            {
                MessageBox.Show("There aren't any orders with this id.");
                return;
            }

            uc_ViewOrderDetails invoiceDetails = new uc_ViewOrderDetails(new ObservableCollection<OrderDetail>(temp.OrderDetails));
            uc_details.Children.Add(invoiceDetails);
        }
    }
}
