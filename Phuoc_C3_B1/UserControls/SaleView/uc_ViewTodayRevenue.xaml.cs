using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Services;
using Phuoc_C3_B1.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace Phuoc_C3_B1.UserControls.SaleView
{
    public partial class uc_ViewTodayRevenue : UserControl
    {
        private IOrderService _orderService = new OrderService();
        private IStatisticsService _statisticsService = new StatisticsService();


        public ObservableCollection<Statistics> Statistics { get; set; }
        public int Revenue { get; set; }
        public int Profit { get; set; }


        public uc_ViewTodayRevenue()
        {
            ObservableCollection<Order> ordersToday = _orderService.GetOrdersByDate(DateTime.Now);

            if (ordersToday.Count > 0)
            {
                InitializeComponent();

                int revenue = 0;
                int profitVariable = 0;

                Statistics = _statisticsService.GetStatistics(ordersToday, ref revenue, ref profitVariable);

                Revenue = revenue;
                Profit = revenue - profitVariable;

                this.DataContext = this;
            }
            else
            {
                MessageBox.Show("There aren't any orders today...");
            }
        }
    }
}
