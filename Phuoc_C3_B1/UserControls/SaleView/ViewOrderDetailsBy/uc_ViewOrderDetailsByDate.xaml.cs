using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls.SaleView.ViewOrderDetailsBy
{
    public partial class uc_ViewOrderDetailsByDate : UserControl, INotifyPropertyChanged
    {
        private IOrderService _service = new OrderService();


        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }


        public uc_ViewOrderDetailsByDate()
        {
            InitializeComponent();

            btn_filter.Click += Btn_filter_Click; ;

            this.DataContext = this;
        }


        private void Btn_filter_Click(object sender, RoutedEventArgs e)
        {
            uc_details.Children.Clear();

            ObservableCollection<Order> temp = _service.GetOrdersByDate(SelectedDate);

            if (temp.Count == 0)
            {
                MessageBox.Show("There aren't any orders on this date.");
                return;
            }

            foreach (var item in temp)
            {
                uc_ViewOrderDetails ordertDetails = new uc_ViewOrderDetails(new ObservableCollection<OrderDetail>(item.OrderDetails));
                uc_details.Children.Add(ordertDetails);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
