
namespace Phuoc_C3_B1.Models
{
    public class ReceiptDetail
    {
        private static int _id = 1;


        public int Id { get; set; }
        public string ReceiptId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Total { get { return Product.PriceInput * Quantity; } }


        public ReceiptDetail() { }

        public ReceiptDetail(string receiptId, Product product, int quantity)
        {
            Id = _id++;
            ReceiptId = receiptId;
            Product = product;
            Quantity = quantity;
        }


        public void GetId()
        {
            Id = _id++;
        }
    }
}
