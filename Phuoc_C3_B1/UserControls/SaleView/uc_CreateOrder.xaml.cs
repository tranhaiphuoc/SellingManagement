using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Phuoc_C3_B1.UserControls.SaleView
{
    public partial class uc_CreateOrder : UserControl, INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        private IOrderService _service = new OrderService();


        private static ObservableCollection<Available> _availables;
        public ObservableCollection<Available> Availables
        {
            get { return _availables; }
            set { _availables = value; }
        }
        public Available SelectedAvailable { get; set; }


        private static ObservableCollection<OrderDetail> _orderDetails;
        public ObservableCollection<OrderDetail> OrderDetails
        {
            get { return _orderDetails; }
            set
            {
                _orderDetails = value;
                OnPropertyChanged();
            }
        }
        public OrderDetail SelectedOrderDetail { get; set; }


        private Customer customer;


        public uc_CreateOrder()
        {
            InitializeComponent();

            if (_availables == null)
            {
                _availables = new ObservableCollection<Available>(_unitOfWork.Availables);
            }

            if (_orderDetails == null)
            {
                _orderDetails = new ObservableCollection<OrderDetail>();
            }

            tb_quantity.PreviewTextInput += Tb_PreviewTextInput;
            tb_phoneNumber.PreviewTextInput += Tb_PreviewTextInput;
            tb_phoneNumber.PreviewTextInput += Tb_PreviewTextInput;

            btn_add.Click += Btn_add_Click;
            btn_save.Click += Btn_save_Click;

            this.DataContext = this;
        }


        private void Tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void btn_deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Proceeding to delete this row?", "Confirming", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                foreach (var item in Availables)
                {
                    if (item.Product.Id == SelectedOrderDetail.Product.Id)
                    {
                        item.InStock += SelectedOrderDetail.Quantity;
                        break;
                    }
                }

                OrderDetails.Remove(SelectedOrderDetail);
            }
        }


        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidForAdding())
            {
                int quantity = int.Parse(tb_quantity.Text.Trim());

                if (quantity > SelectedAvailable.InStock)
                {
                    MessageBox.Show("Quantity must be smaller than or equal to the in-stock.");
                    return;
                }

                OrderDetails.Add(new OrderDetail(Order.GetOrderNextId(), SelectedAvailable.Product, quantity));
                SelectedAvailable.InStock -= quantity;
            }
        }


        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDetails.Count == 0)
            {
                MessageBox.Show("Order's details must have at least one row.");
                return;
            }

            if (IsValidForSaving())
            {
                string fullName = tb_fullName.Text.TrimStart().TrimEnd();
                string ssn = tb_ssn.Text.TrimStart().TrimEnd();
                string phoneNumber = tb_phoneNumber.Text.TrimStart().TrimEnd();

                customer = new Customer(fullName, ssn, phoneNumber, 0);

                BillForOrderWindow billForOrder = new BillForOrderWindow(customer, OrderDetails, SaveOrder);
                billForOrder.Show();
            }
        }


        private void SaveOrder()
        {
            _service.CreateOrder(customer, new List<OrderDetail>(OrderDetails));
            Reset();
            MessageBox.Show("Created order successfully.");
        }


        private void Reset()
        {
            OrderDetails = new ObservableCollection<OrderDetail>();
        }


        private bool IsValidForAdding()
        {
            if (SelectedAvailable == null)
            {
                MessageBox.Show("Please select a product.");
                return false;
            }

            if (string.IsNullOrEmpty(tb_quantity.Text.Trim()))
            {
                MessageBox.Show("Please enter a quantity.");
                return false;
            }

            return true;
        }


        private bool IsValidForSaving()
        {
            if (string.IsNullOrEmpty(tb_fullName.Text.Trim()))
            {
                MessageBox.Show("Please enter customer's full name.");
                return false;
            }

            if (string.IsNullOrEmpty(tb_ssn.Text.Trim()))
            {
                MessageBox.Show("Please enter customer's social security number.");
                return false;
            }

            if (string.IsNullOrEmpty(tb_phoneNumber.Text.Trim()))
            {
                MessageBox.Show("Please enter customer's phone number.");
                return false;
            }

            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
