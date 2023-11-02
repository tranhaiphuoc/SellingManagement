using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls.SaleView
{
    public partial class uc_ViewCustomers : UserControl
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
        }


        public uc_ViewCustomers()
        {
            InitializeComponent();

            _customers = new ObservableCollection<Customer>(_unitOfWork.Customers);

            this.DataContext = this;
        }
    }
}
