using Phuoc_C3_B1.Models;
using Phuoc_C5_B2.Utilities;
using System;
using System.Xml;


namespace Phuoc_C3_B1.Utilities
{
    class SeedData
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();

        public static void Init()
        {
            LoadAccounts();
            LoadProducts();
            LoadStock();
            LoadFoodReceipt();
            LoadReceipt();
            LoadInvoice();
            LoadAvailable();
            LoadCustomer();
            LoadOrder();
        }


        // ----------------- Load Date From XML ----------------- //
        private static void LoadAccounts()
        {
            DataProvider.PathData = Variables.AccountURL;
            DataProvider.Open();

            XmlNodeList xmlAccountList = DataProvider.GetNodeList("//Account");

            foreach (XmlNode xmlAccount in xmlAccountList)
            {
                string username = xmlAccount.Attributes["Username"].Value;
                string password = xmlAccount.Attributes["Password"].Value;
                int role = int.Parse(xmlAccount.Attributes["Role"].Value);

                unitOfWork.Accounts.Add(new Account(username, password, role));
            }

            DataProvider.Close();
        }

        #region Products
        private static void LoadProducts()
        {
            LoadElectronic();
            LoadFood();
            LoadPorcelain();
        }

        private static void LoadElectronic()
        {
            DataProvider.PathData = Variables.ElectronicURL;
            DataProvider.Open();

            XmlNodeList xmlProductList = DataProvider.GetNodeList("Product");

            foreach (XmlNode xmlProduct in xmlProductList)
            {
                string id = xmlProduct.Attributes["Id"].Value;
                string name = xmlProduct.Attributes["Name"].Value;
                string producer = xmlProduct.Attributes["Producer"].Value;
                int priceInput = int.Parse(xmlProduct.Attributes["PriceInput"].Value);
                int warranty = int.Parse(xmlProduct.Attributes["Warranty"].Value);
                int electricPower = int.Parse(xmlProduct.Attributes["ElectricPower"].Value);

                unitOfWork.Products.Add(new Electronic(id, name, producer, priceInput, warranty, electricPower));
            }

            DataProvider.Close();
        }

        private static void LoadFood()
        {
            DataProvider.PathData = Variables.FoodURL;
            DataProvider.Open();

            XmlNodeList xmlProductList = DataProvider.GetNodeList("Product");

            foreach (XmlNode xmlProduct in xmlProductList)
            {
                string id = xmlProduct.Attributes["Id"].Value;
                string name = xmlProduct.Attributes["Name"].Value;
                string producer = xmlProduct.Attributes["Producer"].Value;
                int priceInput = int.Parse(xmlProduct.Attributes["PriceInput"].Value);
                //DateTime mfgDate = DateTime.Parse(xmlProduct.Attributes["MfgDate"].Value);
                //DateTime expDate = DateTime.Parse(xmlProduct.Attributes["ExpDate"].Value);

                unitOfWork.Products.Add(new Food(id, name, producer, priceInput));
            }

            DataProvider.Close();
        }

        private static void LoadPorcelain()
        {
            DataProvider.PathData = Variables.PorcelainURL;
            DataProvider.Open();

            XmlNodeList xmlProductList = DataProvider.GetNodeList("Product");

            foreach (XmlNode xmlProduct in xmlProductList)
            {
                string id = xmlProduct.Attributes["Id"].Value;
                string name = xmlProduct.Attributes["Name"].Value;
                string producer = xmlProduct.Attributes["Producer"].Value;
                int priceInput = int.Parse(xmlProduct.Attributes["PriceInput"].Value);
                string material = xmlProduct.Attributes["Material"].Value;

                unitOfWork.Products.Add(new Porcelain(id, name, producer, priceInput, material));
            }

            DataProvider.Close();
        }
        #endregion

        private static void LoadStock()
        {
            DataProvider.PathData = Variables.StockURL;
            DataProvider.Open();

            XmlNodeList xmlStockList = DataProvider.GetNodeList("Stock");

            foreach (XmlNode xmlStock in xmlStockList)
            {
                string productId = xmlStock.Attributes["ProductId"].Value;
                Product product = unitOfWork.Products.Find(p => p.Id == productId);

                int prevIQ = int.Parse(xmlStock.Attributes["PrevIQ"].Value);
                int prevIT = int.Parse(xmlStock.Attributes["PrevIT"].Value);

                int iQ = int.Parse(xmlStock.Attributes["IQ"].Value);
                int iT = int.Parse(xmlStock.Attributes["IT"].Value);
                DateTime iD = DateTime.Parse(xmlStock.Attributes["ID"].Value);

                int prevEQ = int.Parse(xmlStock.Attributes["PrevEQ"].Value);
                int prevET = int.Parse(xmlStock.Attributes["PrevET"].Value);

                int eQ = int.Parse(xmlStock.Attributes["EQ"].Value);
                int eT = int.Parse(xmlStock.Attributes["ET"].Value);
                DateTime eD = DateTime.Parse(xmlStock.Attributes["ED"].Value);

                unitOfWork.Stocks.Add(new Stock(product,
                    prevIQ, prevIT, iQ, iT, iD,
                    prevEQ, prevET, eQ, eT, eD));
            }

            DataProvider.Close();
        }

        private static void LoadFoodReceipt()
        {
            DataProvider.PathData = Variables.FoodReceiptURL;
            DataProvider.Open();

            XmlNodeList xmlStockList = DataProvider.GetNodeList("Receipt");

            foreach (XmlNode xmlStock in xmlStockList)
            {
                string foodId = xmlStock.Attributes["FoodId"].Value;
                string receitptId = xmlStock.Attributes["ReceiptId"].Value;
                DateTime mfgDate = DateTime.Parse(xmlStock.Attributes["MfgDate"].Value);
                DateTime expDate = DateTime.Parse(xmlStock.Attributes["ExpDate"].Value);

                unitOfWork.FoodReceipt.Add(new FoodReceipt(foodId, receitptId, mfgDate, expDate));
            }

            DataProvider.Close();
        }

        private static void LoadReceipt()
        {
            DataProvider.PathData = Variables.ReceiptURL;
            DataProvider.Open();

            XmlNodeList xmlReceiptList = DataProvider.GetNodeList("Receipt");

            foreach (XmlNode xmlReceipt in xmlReceiptList)
            {
                string username = xmlReceipt.Attributes["Username"].Value;
                DateTime createdAt = DateTime.Parse(xmlReceipt.Attributes["CreatedAt"].Value);

                Receipt receipt = new Receipt(username, createdAt);

                foreach (XmlNode detail in xmlReceipt.ChildNodes)
                {
                    string productId = detail.Attributes["ProductId"].Value;
                    int priceInput = int.Parse(detail.Attributes["PriceInput"].Value);

                    Product product = unitOfWork.Products.Find(p => p.Id == productId);
                    product = CloneNewProduct(product, priceInput);

                    int quantity = int.Parse(detail.Attributes["Quantity"].Value);

                    receipt.ReceiptDetails.Add(new ReceiptDetail(receipt.Id, product, quantity));
                }

                unitOfWork.Receipts.Add(receipt);
            }

            DataProvider.Close();
        }

        private static void LoadInvoice()
        {
            DataProvider.PathData = Variables.InvoiceURL;
            DataProvider.Open();

            XmlNodeList xmlInvoiceList = DataProvider.GetNodeList("Invoice");

            foreach (XmlNode xmlInvoice in xmlInvoiceList)
            {
                string username = xmlInvoice.Attributes["Username"].Value;
                DateTime createdAt = DateTime.Parse(xmlInvoice.Attributes["CreatedAt"].Value);

                Invoice invoice = new Invoice(username, createdAt);

                foreach (XmlNode detail in xmlInvoice.ChildNodes)
                {
                    string productId = detail.Attributes["ProductId"].Value;
                    int priceInput = int.Parse(detail.Attributes["PriceOutput"].Value);

                    Product product = unitOfWork.Products.Find(p => p.Id == productId);
                    product = CloneNewProduct(product, priceInput);

                    int quantity = int.Parse(detail.Attributes["Quantity"].Value);

                    invoice.InvoiceDetails.Add(new InvoiceDetail(invoice.Id, product, quantity));
                }

                unitOfWork.Invoices.Add(invoice);
            }

            DataProvider.Close();
        }

        private static void LoadOrder()
        {
            DataProvider.PathData = Variables.OrderURL;
            DataProvider.Open();

            XmlNodeList xmlOrderList = DataProvider.GetNodeList("Order");

            foreach (XmlNode xmlOrder in xmlOrderList)
            {
                string username = xmlOrder.Attributes["Username"].Value;
                string fullname = xmlOrder.Attributes["CustomerName"].Value;
                string ssn = xmlOrder.Attributes["SSN"].Value;
                string phone = xmlOrder.Attributes["Phone"].Value;
                DateTime createdAt = DateTime.Parse(xmlOrder.Attributes["CreatedAt"].Value);

                Order order = new Order(username, createdAt, new Customer(fullname, ssn, phone, 0));

                foreach (XmlNode detail in xmlOrder.ChildNodes)
                {
                    string productId = detail.Attributes["ProductId"].Value;

                    Product product = unitOfWork.Products.Find(p => p.Id == productId);
                    product = CloneNewProduct(product, product.PriceInput);

                    int quantity = int.Parse(detail.Attributes["Quantity"].Value);

                    order.OrderDetails.Add(new OrderDetail(productId, product, quantity));
                }

                unitOfWork.Orders.Add(order);
            }

            DataProvider.Close();
        }

        private static void LoadAvailable()
        {
            DataProvider.PathData = Variables.AvailableURL;
            DataProvider.Open();

            XmlNodeList xmlAvailableList = DataProvider.GetNodeList("Available");

            foreach (XmlNode xmlAvailable in xmlAvailableList)
            {
                string productId = xmlAvailable.Attributes["ProductId"].Value;
                int inStock = int.Parse(xmlAvailable.Attributes["InStock"].Value);

                Product product = unitOfWork.Products.Find(p => p.Id == productId);

                unitOfWork.Availables.Add(new Available(product, inStock));
            }

            DataProvider.Close();
        }

        private static void LoadCustomer()
        {
            DataProvider.PathData = Variables.CustomerURL;
            DataProvider.Open();

            XmlNodeList xmlCustomerList = DataProvider.GetNodeList("Customer");

            foreach (XmlNode xmlCustomer in xmlCustomerList)
            {
                string fullname = xmlCustomer.Attributes["FullName"].Value;
                string ssn = xmlCustomer.Attributes["SSN"].Value;
                string phone = xmlCustomer.Attributes["Phone"].Value;
                int point = int.Parse(xmlCustomer.Attributes["Point"].Value);

                unitOfWork.Customers.Add(new Customer(fullname, ssn, phone, point));
            }

            DataProvider.Close();
        }


        // ----------------- Background Process ----------------- //
        private static Product CloneNewProduct(Product product, int priceIn)
        {
            if (product is Electronic)
            {
                Electronic e = (Electronic)product;
                return new Electronic(e.Id, e.Name, e.Producer, priceIn, e.Warranty, e.ElectricPower);
            }

            if (product is Food)
            {
                Food f = (Food)product;
                return new Food(f.Id, f.Name, f.Producer, priceIn);
            }

            if (product is Porcelain)
            {
                Porcelain p = (Porcelain)product;
                return new Porcelain(p.Id, p.Name, p.Producer, priceIn, p.Material);
            }

            return null;
        }
    }
}
