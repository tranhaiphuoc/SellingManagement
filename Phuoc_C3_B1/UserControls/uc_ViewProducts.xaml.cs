using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls
{
    public partial class uc_ViewProducts : UserControl, INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        private ObservableCollection<Electronic> _electronics = new ObservableCollection<Electronic>();
        public ObservableCollection<Electronic> Electronics
        {
            get { return _electronics; }
            set
            {
                _electronics = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Food> _food = new ObservableCollection<Models.Food>();
        public ObservableCollection<Food> Food
        {
            get { return _food; }
            set
            {
                _food = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Porcelain> _porcelains = new ObservableCollection<Porcelain>();
        public ObservableCollection<Porcelain> Porcelains
        {
            get { return _porcelains; }
            set
            {
                _porcelains = value;
                OnPropertyChanged();
            }
        }


        public uc_ViewProducts()
        {
            InitializeComponent();

            _unitOfWork.Products.ForEach(p =>
            {
                if (p is Electronic)
                {
                    _electronics.Add(p as Electronic);
                }
                else if (p is Food)
                {
                    _food.Add(p as Food);
                }
                else
                {
                    _porcelains.Add(p as Porcelain);
                }
            });

            this.DataContext = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
