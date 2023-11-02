using Phuoc_C3_B1.Models;
using Phuoc_C3_B1.Repositories;
using System.Collections.Generic;


namespace Phuoc_C3_B1
{
    class UnitOfWork
    {
        private static AccountRepository _accounts = new AccountRepository();
        public AccountRepository Accounts { get { return _accounts; } }


        private static List<Product> _products = new List<Product>();
        private static List<FoodReceipt> _foodReceipt = new List<FoodReceipt>();
        private static List<Receipt> _receipts = new List<Receipt>();
        private static List<Invoice> _invoices = new List<Invoice>();

        private static List<Stock> _stocks = new List<Stock>();
        private static List<Order> _orders = new List<Order>();

        private static List<Available> _availables = new List<Available>();
        private static List<Customer> _customers = new List<Customer>();


        public List<Product> Products { get { return _products; } }
        public List<FoodReceipt> FoodReceipt { get { return _foodReceipt; } }
        public List<Receipt> Receipts { get { return _receipts; } }
        public List<Invoice> Invoices { get { return _invoices; } }

        public List<Stock> Stocks { get { return _stocks; } }
        public List<Order> Orders { get { return _orders; } }

        public List<Available> Availables { get { return _availables; } }
        public List<Customer> Customers { get { return _customers; } }
    }
}
