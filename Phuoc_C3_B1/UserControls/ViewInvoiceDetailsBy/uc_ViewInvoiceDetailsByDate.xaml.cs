using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls.ViewInvoiceDetailsBy
{
    public partial class uc_ViewInvoiceDetailsByDate : UserControl, INotifyPropertyChanged
    {
        private IInvoiceService _service = new InvoiceService();


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


        public uc_ViewInvoiceDetailsByDate()
        {
            InitializeComponent();

            btn_filter.Click += Btn_filter_Click;

            this.DataContext = this;
        }


        private void Btn_filter_Click(object sender, RoutedEventArgs e)
        {
            uc_details.Children.Clear();

            ObservableCollection<Invoice> temp = _service.GetInvoicesByDate(SelectedDate);

            if (temp.Count == 0)
            {
                MessageBox.Show("There aren't any invoices on this date.");
                return;
            }

            foreach (var item in temp)
            {
                uc_ViewInvoiceDetails receiptDetails = new uc_ViewInvoiceDetails(new ObservableCollection<InvoiceDetail>(item.InvoiceDetails));
                uc_details.Children.Add(receiptDetails);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
