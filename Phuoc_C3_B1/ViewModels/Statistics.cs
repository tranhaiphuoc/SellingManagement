using Phuoc_C3_B1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phuoc_C3_B1.ViewModels
{
    public class Statistics
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Total { get { return Product.PriceOutput * Quantity; } }


        public Statistics(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
