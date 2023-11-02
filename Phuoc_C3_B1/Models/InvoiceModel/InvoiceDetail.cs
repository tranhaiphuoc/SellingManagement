

namespace Phuoc_C3_B1.Models
{
    public class InvoiceDetail
    {
        private static int _id = 1;


        public int Id { get; }
        public string InvoiceId { get; }
        public Product Product { get; }
        public int Quantity { get; }
        public int Total { get { return Product.PriceOutput * Quantity; } }


        public InvoiceDetail(string invoiceId, Product product, int quantity)
        {
            Id = _id++;
            InvoiceId = invoiceId;
            Product = product;
            Quantity = quantity;
        }
    }
}
