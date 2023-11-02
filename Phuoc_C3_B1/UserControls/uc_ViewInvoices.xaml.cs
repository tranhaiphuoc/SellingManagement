using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls
{
    public partial class uc_ViewInvoices : UserControl, INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        private ObservableCollection<Invoice> _invoices;
        public ObservableCollection<Invoice> Invoices
        {
            get { return _invoices; }
            set
            {
                _invoices = value;
                OnPropertyChanged();
            }
        }


        public uc_ViewInvoices()
        {
            InitializeComponent();

            _invoices = new ObservableCollection<Invoice>(_unitOfWork.Invoices);

            this.DataContext = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
