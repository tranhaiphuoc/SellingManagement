using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Phuoc_C3_B1.UserControls.ViewInvoiceDetailsBy
{
    public partial class uc_ViewInvoiceDetailsByInvoiceId : UserControl
    {
        private IInvoiceService _service = new InvoiceService();


        public uc_ViewInvoiceDetailsByInvoiceId()
        {
            InitializeComponent();

            tb_invoiceId.PreviewTextInput += Tb_invoiceId_PreviewTextInput;
            btn_filter.Click += Btn_filter_Click;
        }

        private void Tb_invoiceId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void Btn_filter_Click(object sender, RoutedEventArgs e)
        {
            uc_details.Children.Clear();

            if (string.IsNullOrEmpty(tb_invoiceId.Text))
            {
                MessageBox.Show("Please enter an id.");
                return;
            }

            Invoice temp = _service.GetById("PXK" + tb_invoiceId.Text.Trim());

            if (temp == null)
            {
                MessageBox.Show("There aren't any invoices with this id.");
                return;
            }

            uc_ViewInvoiceDetails invoiceDetails = new uc_ViewInvoiceDetails(new ObservableCollection<InvoiceDetail>(temp.InvoiceDetails));
            uc_details.Children.Add(invoiceDetails);
        }
    }
}
