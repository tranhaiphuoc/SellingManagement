using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls
{
    public partial class uc_ViewReceipts : UserControl, INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        private ObservableCollection<Receipt> _receipts;
        public ObservableCollection<Receipt> Receipts
        {
            get { return _receipts; }
            set
            {
                _receipts = value;
                OnPropertyChanged();
            }
        }


        public uc_ViewReceipts()
        {
            InitializeComponent();

            _receipts = new ObservableCollection<Receipt>(_unitOfWork.Receipts);

            this.DataContext = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
