using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Services;
using Phuoc_C5_B2;
using Phuoc_C5_B2.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;


namespace Phuoc_C3_B1.UserControls
{
    public partial class uc_CreateInvoice : UserControl, INotifyPropertyChanged
    {
        private IStockService _stockService = new StockService();
        private IInvoiceService _invoiceService = new InvoiceService();


        private static ObservableCollection<InvoiceDetail> _invoiceDetails;
        public ObservableCollection<InvoiceDetail> InvoiceDetails
        {
            get { return _invoiceDetails; }
            set
            {
                _invoiceDetails = value;
                OnPropertyChanged();
            }
        }


        private InvoiceDetail _selectedInvoiceDetail;
        public InvoiceDetail SelectedInvoiceDetail
        {
            get { return _selectedInvoiceDetail; }
            set
            {
                _selectedInvoiceDetail = value;
                OnPropertyChanged();
            }
        }


        private static ObservableCollection<Stock> _stocks;
        public ObservableCollection<Stock> Stocks
        {
            get { return _stocks; }
            set
            {
                _stocks = value;
                OnPropertyChanged();

            }
        }


        private Stock _selectedStock;
        public Stock SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                _selectedStock = value;
                OnPropertyChanged();
            }
        }


        public uc_CreateInvoice()
        {
            InitializeComponent();

            if (_invoiceDetails == null)
            {
                _invoiceDetails = new ObservableCollection<InvoiceDetail>();
            }

            if (_stocks == null)
            {
                _stocks = _stockService.CloneStocks();
            }

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


        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                int quantity = int.Parse(tb_quantity.Text);
                InvoiceDetails.Add(new InvoiceDetail(Invoice.GetInvoiceNextId(), SelectedStock.Product, quantity));
                SelectedStock.Quantity -= quantity;
            }
        }


        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceDetails.Count > 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Saving this receipt?", "Confirming", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _invoiceService.CreateInvoice(_invoiceDetails);
                    Reset();
                }

                MessageBox.Show("Created invoice succesfully.");
            }
            else
            {
                MessageBox.Show("Please add at least one row in the invoice.");
            }
        }


        private void btn_deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Proceeding to delete this row?", "Confirming", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                foreach (var item in _stocks)
                {
                    if (item.Product.Id == SelectedInvoiceDetail.Product.Id)
                    {
                        item.Quantity += SelectedInvoiceDetail.Quantity;
                        break;
                    }
                }
                _invoiceDetails.Remove(SelectedInvoiceDetail);
            }
        }


        private void Reset()
        {
            InvoiceDetails = new ObservableCollection<InvoiceDetail>();
        }


        private bool IsValid()
        {
            if (SelectedStock == null)
            {
                MessageBox.Show("Please select a product.");
                return false;
            }

            if (string.IsNullOrEmpty(tb_quantity.Text))
            {
                MessageBox.Show("Please enter a quantity.");
                return false;
            }

            if (SelectedStock.Quantity < int.Parse(tb_quantity.Text))
            {
                MessageBox.Show("Export quantity can't be greater than the quantiy that's available.");
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
