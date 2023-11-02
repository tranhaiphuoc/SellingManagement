using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls.ViewReceiptBy
{
    public partial class uc_ViewReceiptDetails : UserControl
    {
        public ObservableCollection<ReceiptDetail> ReceiptDetails { get; set; }


        public uc_ViewReceiptDetails(ObservableCollection<ReceiptDetail> receiptDetails)
        {
            InitializeComponent();

            ReceiptDetails = receiptDetails;

            this.DataContext = this;
        }
    }
}
