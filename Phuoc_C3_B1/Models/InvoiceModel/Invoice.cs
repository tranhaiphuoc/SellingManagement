using System;
using System.Collections.Generic;


namespace Phuoc_C3_B1.Models
{
    public class Invoice
    {
        private static int _id = 1;
        private int _quantity;
        private double _total;
        private bool _flag;


        public string Id { get; }
        public string Username { get; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
        public int Quanity
        {
            get
            {
                if (!_flag)
                {
                    foreach (var item in InvoiceDetails)
                    {
                        _total += item.Total;
                        _quantity += item.Quantity;
                    }
                    _flag = true;
                }
                return _quantity;
            }
        }
        public double Total
        {
            get
            {
                if (!_flag)
                {
                    foreach (var item in InvoiceDetails)
                    {
                        _total += item.Total;
                        _quantity += item.Quantity;
                    }
                    _flag = true;
                }
                return _total;
            }
        }
        public DateTime CreatedAt { get; } = DateTime.Now;


        public Invoice(string username)
        {
            Id = "PXK" + _id++.ToString();
            Username = username;
            InvoiceDetails = new List<InvoiceDetail>();
        }

        public Invoice(string username, DateTime createDate)
        {
            Id = "PXK" + _id++.ToString();
            Username = username;
            InvoiceDetails = new List<InvoiceDetail>();
            CreatedAt = createDate;
        }


        public static string GetInvoiceNextId()
        {
            return $"PXK{_id}";
        }
    }
}
