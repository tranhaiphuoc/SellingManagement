using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls.SaleView.ViewOrderDetailsBy
{
    public partial class uc_ViewOrderDetails : UserControl
    {
        public ObservableCollection<OrderDetail> OrderDetails { get; set; }


        public uc_ViewOrderDetails(ObservableCollection<OrderDetail> orderDetails)
        {
            InitializeComponent();

            OrderDetails = orderDetails;

            this.DataContext = this;
        }
    }
}
