using Phuoc_C3_B1.Models;
using Phuoc_C5_B2;
using Phuoc_C5_B2.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;


namespace Phuoc_C3_B1.Services
{
    public class OrderService : IOrderService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        public void CreateOrder(Customer customer, List<OrderDetail> orderDetails)
        {
            if (IsOldCustomer(customer) == false)
            {
                _unitOfWork.Customers.Add(customer);
                CreateCustomer(customer);
            }

            Order order = new Order(Authentication.Username, customer);
            order.OrderDetails = orderDetails;

            _unitOfWork.Orders.Add(order);

            SaveAvailable(order);
            SaveCustomerPoint(customer, (int)(order.Total * Variables.MoneyToPointRatio));
            CreateOrderInXml(order);
        }


        public ObservableCollection<Order> GetOrdersByDate(DateTime date)
        {
            ObservableCollection<Order> temp = new ObservableCollection<Order>();

            _unitOfWork.Orders.ForEach(order =>
            {
                if (order.CreatedAt.Date == date.Date)
                {
                    temp.Add(order);
                }
            });

            return temp;
        }


        public Order GetById(string id)
        {
            Order temp = null;

            foreach (var item in _unitOfWork.Orders)
            {
                if (item.Id == id)
                {
                    temp = item;
                }
            }

            return temp;
        }


        private bool IsOldCustomer(Customer newCustomer)
        {
            foreach (Customer customer in _unitOfWork.Customers)
            {
                if (customer.SSN == newCustomer.SSN)
                {
                    return true;
                }
            }
            return false;
        }

        private static void CreateCustomer(Customer newCustomer)
        {
            var newCustomerNode = DataProvider.CreateNode("Customer");

            var attr = DataProvider.CreateAttr("FullName");
            attr.Value = newCustomer.FullName;
            newCustomerNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("SSN");
            attr.Value = newCustomer.SSN;
            newCustomerNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Phone");
            attr.Value = newCustomer.Phone;
            newCustomerNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Point");
            attr.Value = newCustomer.Point.ToString();
            newCustomerNode.Attributes.Append(attr);

            DataProvider.PathData = Variables.CustomerURL;
            DataProvider.Open();
            DataProvider.AppendNode(DataProvider.NodeRoot, newCustomerNode);
            DataProvider.Close();
        }

        private static void SaveCustomerPoint(Customer customer, int point)
        {
            DataProvider.PathData = Variables.CustomerURL;
            DataProvider.Open();

            XmlNode xmlCustomer = DataProvider.GetNode($"//Customer[@SSN='{customer.SSN}']");

            int newPoint = customer.Point + point;
            xmlCustomer.Attributes["Point"].Value = newPoint.ToString();
            customer.AccumulatePoint(point);

            DataProvider.Close();
        }

        private void SaveAvailable(Order order)
        {
            DataProvider.PathData = Variables.AvailableURL;
            DataProvider.Open();

            foreach (var item in order.OrderDetails)
            {
                XmlNode xmlAvailable = DataProvider.GetNode($"//Available[@ProductId='{item.Product.Id}']");

                if (xmlAvailable != null)
                {
                    int newStock = int.Parse(xmlAvailable.Attributes["InStock"].Value) - item.Quantity;
                    xmlAvailable.Attributes["InStock"].Value = newStock.ToString();
                }
            }

            DataProvider.Close();
        }

        private void CreateOrderInXml(Order order)
        {
            var newOrderNode = DataProvider.CreateNode("Order");

            var attr = DataProvider.CreateAttr("Id");
            attr.Value = order.Id;
            newOrderNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Username");
            attr.Value = Authentication.Username;
            newOrderNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Quantity");
            attr.Value = order.Quanity.ToString();
            newOrderNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Total");
            attr.Value = order.Total.ToString();
            newOrderNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("CreatedAt");
            attr.Value = order.CreatedAt.ToString();
            newOrderNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("CustomerName");
            attr.Value = order.Customer.FullName;
            newOrderNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("SSN");
            attr.Value = order.Customer.SSN;
            newOrderNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Phone");
            attr.Value = order.Customer.Phone;
            newOrderNode.Attributes.Append(attr);

            foreach (var item in order.OrderDetails)
            {
                var newDetailChildNode = DataProvider.CreateNode("Detail");

                attr = DataProvider.CreateAttr("ProductId");
                attr.Value = item.Product.Id.ToString();
                newDetailChildNode.Attributes.Append(attr);

                attr = DataProvider.CreateAttr("PriceOutput");
                attr.Value = item.Product.PriceOutput.ToString();
                newDetailChildNode.Attributes.Append(attr);

                attr = DataProvider.CreateAttr("Quantity");
                attr.Value = item.Quantity.ToString();
                newDetailChildNode.Attributes.Append(attr);

                newOrderNode.AppendChild(newDetailChildNode);
            }

            DataProvider.PathData = Variables.OrderURL;
            DataProvider.Open();
            DataProvider.AppendNode(DataProvider.NodeRoot, newOrderNode);
            DataProvider.Close();
        }
    }


    public interface IOrderService
    {
        void CreateOrder(Customer customer, List<OrderDetail> orderDetails);
        ObservableCollection<Order> GetOrdersByDate(DateTime date);
        Order GetById(string id);
    }
}
