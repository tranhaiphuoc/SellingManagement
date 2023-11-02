﻿using System;
using System.Collections.Generic;


namespace Phuoc_C3_B1.Models
{
    public class Order
    {
        private static int _id = 1;
        private int _quantity;
        private double _total;
        private bool _flag;


        public string Id { get; }
        public string Username { get; }
        public List<OrderDetail> OrderDetails { get; set; }
        public int Quanity
        {
            get
            {
                if (!_flag)
                {
                    foreach (var item in OrderDetails)
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
                    foreach (var item in OrderDetails)
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
        public Customer Customer { get; }


        public Order(string username, Customer customer)
        {
            Id = "HD" + _id++.ToString();
            Username = username;
            OrderDetails = new List<OrderDetail>();
            Customer = customer;
        }

        public Order(string username, DateTime createDate, Customer customer)
        {
            Id = "HD" + _id++.ToString();
            Username = username;
            OrderDetails = new List<OrderDetail>();
            CreatedAt = createDate;
            Customer = customer;
        }

        public static string GetOrderNextId()
        {
            return "HD" + _id.ToString();
        }
    }
}
