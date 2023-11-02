using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Phuoc_C3_B1.Services
{
    public class StatisticsService : IStatisticsService
    {
        public ObservableCollection<Statistics> GetStatistics(ObservableCollection<Order> ordersToday, ref int revenue, ref int profitVariable)
        {
            List<Statistics> statistics = new List<Statistics>();

            foreach (Order order in ordersToday)
            {
                foreach (OrderDetail detail in order.OrderDetails)
                {
                    int idx = statistics.FindIndex(stat =>
                    {
                        return (stat.Product.Id == detail.Product.Id) ? true : false;
                    });

                    if (idx == -1)
                    {
                        Statistics p = new Statistics(detail.Product, detail.Quantity);
                        statistics.Add(p);
                    }
                    else
                    {
                        statistics[idx].Quantity += detail.Quantity;
                    }

                    revenue += detail.Quantity * detail.Product.PriceOutput;
                    profitVariable += detail.Quantity * detail.Product.PriceInput;
                }

            }

            return new ObservableCollection<Statistics>(statistics);
        }
    }


    public interface IStatisticsService
    {
        ObservableCollection<Statistics> GetStatistics(ObservableCollection<Order> ordersToday, ref int revenue, ref int profitVariable);
    }
}
