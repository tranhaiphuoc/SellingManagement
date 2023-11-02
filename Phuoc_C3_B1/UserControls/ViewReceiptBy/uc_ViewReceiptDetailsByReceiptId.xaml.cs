using Phuoc_C3_B1.Models;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Phuoc_C3_B1.UserControls.ViewReceiptBy
{
    public partial class uc_ViewReceiptDetailsByReceiptId : UserControl
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        public uc_ViewReceiptDetailsByReceiptId()
        {
            InitializeComponent();

            tb_receiptId.PreviewTextInput += Tb_receiptId_PreviewTextInput;
            btn_filter.Click += Btn_filter_Click;
        }


        private void Tb_receiptId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void Btn_filter_Click(object sender, RoutedEventArgs e)
        {
            uc_details.Children.Clear();

            if (string.IsNullOrEmpty(tb_receiptId.Text))
            {
                MessageBox.Show("Please enter an id.");
                return;
            }

            string id = "PNK" + tb_receiptId.Text.Trim();
            Receipt temp = null;

            foreach (var item in _unitOfWork.Receipts)
            {
                if (item.Id == id)
                {
                    temp = item;
                }
            }

            if (temp == null)
            {
                MessageBox.Show("There aren't any receipts with this id.");
                return;
            }

            uc_ViewReceiptDetails receiptDetails = new uc_ViewReceiptDetails(new ObservableCollection<ReceiptDetail>(temp.ReceiptDetails));
            uc_details.Children.Add(receiptDetails);
        }
    }
}
