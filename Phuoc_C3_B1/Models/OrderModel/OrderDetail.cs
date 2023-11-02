

namespace Phuoc_C3_B1.Models
{
    public class OrderDetail
    {
        private static int _id = 1;


        public int Id { get; }
        public string OrderId { get; }
        public Product Product { get; }
        public int Quantity { get; }
        public int Total
        {
            get
            {
                if (Product is Electronic)
                {
                    Electronic e = Product as Electronic;
                    return Product.PriceOutput * Quantity - e.GetDiscount();
                }
                if (Product is Food)
                {
                    Food f = Product as Food;
                    return Product.PriceOutput * Quantity - f.GetDiscount();
                }
                if (Product is Porcelain)
                {
                    Porcelain p = Product as Porcelain;
                    return Product.PriceOutput * Quantity - p.GetDiscount();
                }
                return Product.PriceOutput * Quantity;
            }
        }


        public OrderDetail(string orderId, Product product, int quantity)
        {
            Id = _id++;
            OrderId = orderId;
            Product = product;
            Quantity = quantity;
        }
    }
}
