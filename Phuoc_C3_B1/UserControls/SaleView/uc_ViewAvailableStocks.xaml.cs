using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls.SaleView
{
    public partial class uc_ViewAvailableStocks : UserControl
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        private ObservableCollection<Available> _availables;
        public ObservableCollection<Available> Availablea
        {
            get { return _availables; }
        }


        public uc_ViewAvailableStocks()
        {
            InitializeComponent();

            _availables = new ObservableCollection<Available>(_unitOfWork.Availables);

            this.DataContext = this;
        }
    }
}
