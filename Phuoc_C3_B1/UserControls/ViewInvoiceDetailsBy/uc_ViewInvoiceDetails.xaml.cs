using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls.ViewInvoiceDetailsBy
{
    public partial class uc_ViewInvoiceDetails : UserControl
    {
        public ObservableCollection<InvoiceDetail> InvoiceDetails { get; set; }


        public uc_ViewInvoiceDetails(ObservableCollection<InvoiceDetail> invoiceDetails)
        {
            InitializeComponent();

            InvoiceDetails = invoiceDetails;

            this.DataContext = this;
        }
    }
}
