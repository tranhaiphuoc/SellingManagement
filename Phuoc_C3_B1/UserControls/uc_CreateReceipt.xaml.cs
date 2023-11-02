using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Phuoc_C3_B1.UserControls
{
    public partial class uc_CreateReceipt : UserControl, INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        private IReceiptService _service = new ReceiptService();


        private ObservableCollection<ReceiptDetail> _receiptDetails = new ObservableCollection<ReceiptDetail>();
        public ObservableCollection<ReceiptDetail> ReceiptDetails
        {
            get { return _receiptDetails; }
            set
            {
                _receiptDetails = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<FoodReceipt> _foodReceipts = new ObservableCollection<FoodReceipt>();
        public ObservableCollection<FoodReceipt> FoodReceipts
        {
            get { return _foodReceipts; }
            set
            {
                _foodReceipts = value;
                OnPropertyChanged();
            }
        }


        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }


        private DateTime _mfgDate = DateTime.Now;
        public DateTime MfgDate
        {
            get { return _mfgDate; }
            set
            {
                _mfgDate = value;
                OnPropertyChanged();
            }
        }


        private DateTime _expDate = DateTime.Now;
        public DateTime ExpDate
        {
            get { return _expDate; }
            set
            {
                _expDate = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Product> Products { get; }


        public uc_CreateReceipt()
        {
            InitializeComponent();

            Products = new ObservableCollection<Product>(_unitOfWork.Products);

            tb_quantity.PreviewTextInput += Tb_quantity_PreviewTextInput;

            btn_add.Click += Btn_add_Click;
            btn_save.Click += Btn_save_Click;

            this.DataContext = this;
        }


        private void Tb_quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void btn_deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Proceeding to delete this row?", "Confirming", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ReceiptDetail temp = dg_receiptDetails.SelectedItem as ReceiptDetail;

                if (temp.Product is Food)
                {
                    FoodReceipt foodTemp = _foodReceipts
                        .FirstOrDefault(item => item.FoodId == temp.Product.Id);

                    _foodReceipts.Remove(foodTemp);
                }

                _receiptDetails.Remove(temp);
            }
        }


        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                if (SelectedProduct is Food)
                {
                    if (IsValidForFood())
                    {
                        _foodReceipts.Add(new FoodReceipt(_selectedProduct.Id, Receipt.GetReceiptNextId(), MfgDate, ExpDate));
                    }
                    else return;
                }

                _receiptDetails.Add(new ReceiptDetail()
                {
                    Product = SelectedProduct,
                    Quantity = int.Parse(tb_quantity.Text.Trim())
                });
            }
        }


        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (ReceiptDetails.Count > 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Saving this receipt?", "Confirming", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _service.CreateReceipt(_receiptDetails, _foodReceipts);
                    Reset();
                }

                MessageBox.Show("Created receipt succesfully.");
            }
            else
            {
                MessageBox.Show("Please add at least one row in the receipt.");
            }
        }


        private void Reset()
        {
            ReceiptDetails = new ObservableCollection<ReceiptDetail>();
            FoodReceipts = new ObservableCollection<FoodReceipt>();
        }


        private bool IsValidForFood()
        {
            if (ExpDate.Subtract(MfgDate).Days < 0)
            {
                MessageBox.Show("Expiry date must be greater than or equal to manufacturing date.");
                return false;
            }

            return true;
        }


        private bool IsValid()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a product.");
                return false;
            }

            if (string.IsNullOrEmpty(tb_quantity.Text))
            {
                MessageBox.Show("Please enter the quantity.");
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
