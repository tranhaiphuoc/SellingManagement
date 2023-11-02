using Phuoc_C3_B1.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls.ViewReceiptBy
{
    public partial class uc_ViewReceiptByDate : UserControl, INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }


        public uc_ViewReceiptByDate()
        {
            InitializeComponent();

            btn_filter.Click += Btn_filter_Click;

            this.DataContext = this;
        }


        private void Btn_filter_Click(object sender, RoutedEventArgs e)
        {
            uc_details.Children.Clear();
            ObservableCollection<Receipt> temp = new ObservableCollection<Receipt>();

            _unitOfWork.Receipts.ForEach(r =>
            {
                if (r.CreatedAt.Date == SelectedDate.Date)
                {
                    temp.Add(r);
                }
            });

            if (temp.Count == 0)
            {
                MessageBox.Show("There aren't any receipts on this date.");
                return;
            }

            foreach (var item in temp)
            {
                uc_ViewReceiptDetails receiptDetails = new uc_ViewReceiptDetails(new ObservableCollection<ReceiptDetail>(item.ReceiptDetails));
                uc_details.Children.Add(receiptDetails);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
