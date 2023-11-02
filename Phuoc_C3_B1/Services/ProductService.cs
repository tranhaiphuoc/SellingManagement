using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.ViewModels;
using Phuoc_C5_B2.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;


namespace Phuoc_C3_B1.Services
{
    public class ProductService : IProductService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        public void UpdatePriceInput(Product product, int newPrice)
        {
            switch (product.CategoryType)
            {
                case "Food":
                    DataProvider.PathData = Variables.FoodURL;
                    break;
                case "Electronic":
                    DataProvider.PathData = Variables.ElectronicURL;
                    break;
                case "Porcelain":
                    DataProvider.PathData = Variables.PorcelainURL;
                    break;
            }

            DataProvider.Open();

            XmlNode xmlProduct = DataProvider.GetNode($"//Product[@Id='{product.Id}']");
            xmlProduct.Attributes["PriceInput"].Value = newPrice.ToString();

            DataProvider.Close();
        }


        public ObservableCollection<ExpiredFood> GetExpiredFood()
        {
            ObservableCollection<ExpiredFood> expiredFood = new ObservableCollection<ExpiredFood>();

            foreach (var item in _unitOfWork.FoodReceipt)
            {
                if (DateTime.Now.Subtract(item.ExpDate).Days > 0)
                {
                    Food pd = _unitOfWork.Products.FirstOrDefault(p => p.Id == item.FoodId) as Food;
                    expiredFood.Add(new ExpiredFood(pd, item));
                }
            }

            return expiredFood;
        }
    }


    public interface IProductService
    {
        void UpdatePriceInput(Product product, int newPrice);
        ObservableCollection<ExpiredFood> GetExpiredFood();
    }
}
