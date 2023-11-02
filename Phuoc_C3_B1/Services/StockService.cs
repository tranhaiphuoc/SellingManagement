using Phuoc_C3_B1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phuoc_C3_B1.Services
{
    public class StockService : IStockService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        public ObservableCollection<Stock> CloneStocks()
        {
            ObservableCollection<Stock> _stocks = new ObservableCollection<Stock>();

            _unitOfWork.Stocks.ForEach(s =>
            {
                Stock stock = new Stock(s.Product.MakeClone(), DateTime.Now, s.Quantity);
                _stocks.Add(stock);
            });

            return _stocks;
        }
    }


    public interface IStockService
    {
        ObservableCollection<Stock> CloneStocks();
    }
}
