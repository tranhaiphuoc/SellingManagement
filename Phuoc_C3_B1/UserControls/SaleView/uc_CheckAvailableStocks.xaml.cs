using Phuoc_C3_B1.Models;
using Phuoc_C5_B2.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Phuoc_C3_B1.UserControls.SaleView
{
    public partial class uc_CheckAvailableStocks : UserControl
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        private ObservableCollection<Available> _availables = new ObservableCollection<Available>();
        public ObservableCollection<Available> Availables { get { return _availables; } }


        public uc_CheckAvailableStocks()
        {
            InitializeComponent();

            _unitOfWork.Availables.ForEach(a =>
            {
                if (a.InStock <= Variables.outOfStockQuantity)
                {
                    _availables.Add(a);
                }
            });

            if (_availables.Count == 0)
            {
                MessageBox.Show("There's no stock that's running low.");
                return;
            }

            this.DataContext = this;
        }
    }
}
