using System;
using System.Collections.Generic;

namespace Phuoc_C3_B1.Models
{
    public class Receipt
    {
        private static int _id = 1;
        private int _quantity;
        private double _total;
        private bool _flag;


        public string Id { get; }
        public string Username { get; }
        public List<ReceiptDetail> ReceiptDetails { get; set; }
        public int Quanity
        {
            get
            {
                if (!_flag)
                {
                    foreach (var item in ReceiptDetails)
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
                    foreach (var item in ReceiptDetails)
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


        public Receipt(string username)
        {
            Id = "PNK" + _id++.ToString();
            Username = username;
            ReceiptDetails = new List<ReceiptDetail>();
        }

        public Receipt(string username, DateTime createDate)
        {
            Id = "PNK" + _id++.ToString();
            Username = username;
            ReceiptDetails = new List<ReceiptDetail>();
            CreatedAt = createDate;
        }

        public static string GetReceiptNextId()
        {
            return $"PNK{_id}";
        }
    }
}
