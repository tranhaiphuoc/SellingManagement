using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Phuoc_C3_B1.UserControls
{
    public partial class uc_UpdateProductPriceInput : UserControl, INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        private IProductService _service = new ProductService();


        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }


        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }


        public uc_UpdateProductPriceInput()
        {
            InitializeComponent();

            _products = new ObservableCollection<Product>(_unitOfWork.Products);

            tb_price.PreviewTextInput += Tb_price_PreviewTextInput;
            btn_save.Click += Btn_save_Click;

            this.DataContext = this;
        }


        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Saving this new price?", "Confirming", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    int newPrice = int.Parse(tb_price.Text.Trim());

                    SelectedProduct.ChangePriceInput(newPrice);
                    _service.UpdatePriceInput(SelectedProduct, newPrice);

                    MessageBox.Show("Changed successfully.");
                }
            }
        }


        private void Tb_price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private bool IsValid()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a product.");
                return false;
            }

            if (string.IsNullOrEmpty(tb_price.Text.Trim()))
            {
                MessageBox.Show("Please enter a new price.");
                return false;
            }

            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
