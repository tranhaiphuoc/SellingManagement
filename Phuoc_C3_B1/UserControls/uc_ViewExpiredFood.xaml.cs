using Phuoc_C3_B1.Services;
using Phuoc_C3_B1.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls
{
    public partial class uc_ViewExpiredFood : UserControl
    {
        private IProductService _service = new ProductService();


        private ObservableCollection<ExpiredFood> _expiredFood;
        public ObservableCollection<ExpiredFood> ExpiredFood
        {
            get { return _expiredFood; }
        }


        public uc_ViewExpiredFood()
        {
            InitializeComponent();

            _expiredFood = _service.GetExpiredFood();

            if (_expiredFood.Count == 0)
            {
                MessageBox.Show("There aren't any food that expires.");
                return;
            }

            this.DataContext = this;
        }
    }
}
