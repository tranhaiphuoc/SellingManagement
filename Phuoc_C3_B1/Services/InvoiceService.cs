using Phuoc_C3_B1.Models;
using Phuoc_C5_B2;
using Phuoc_C5_B2.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Phuoc_C3_B1.Services
{
    public class InvoiceService : IInvoiceService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        public void CreateInvoice(ObservableCollection<InvoiceDetail> invoiceDetails)
        {
            Invoice invoice = new Invoice(Authentication.Username);
            invoice.InvoiceDetails = new List<InvoiceDetail>(invoiceDetails);

            _unitOfWork.Invoices.Add(invoice);

            SaveExportStock(invoice);
            CreateInvoiceInXml(invoice);
            SaveAvailable(invoice);
        }


        public ObservableCollection<Invoice> GetInvoicesByDate(DateTime date)
        {
            ObservableCollection<Invoice> temp = new ObservableCollection<Invoice>();

            _unitOfWork.Invoices.ForEach(r =>
            {
                if (r.CreatedAt.Date == date.Date)
                {
                    temp.Add(r);
                }
            });

            return temp;
        }

        public Invoice GetById(string id)
        {
            Invoice temp = null;

            foreach (var item in _unitOfWork.Invoices)
            {
                if (item.Id == id)
                {
                    temp = item;
                }
            }

            return temp;
        }


        private void SaveExportStock(Invoice invoice)
        {
            DataProvider.PathData = Variables.StockURL;
            DataProvider.Open();

            foreach (InvoiceDetail invoiceDetail in invoice.InvoiceDetails)
            {
                XmlNode xmlStock = DataProvider.GetNode($"//Stock[@ProductId='{invoiceDetail.Product.Id}']");

                if (xmlStock != null)
                {
                    int prevEQ = int.Parse(xmlStock.Attributes["PrevEQ"].Value) + int.Parse(xmlStock.Attributes["EQ"].Value);
                    xmlStock.Attributes["PrevEQ"].Value = prevEQ.ToString();

                    int prevT = int.Parse(xmlStock.Attributes["PrevET"].Value) + int.Parse(xmlStock.Attributes["ET"].Value);
                    xmlStock.Attributes["PrevET"].Value = prevT.ToString();

                    xmlStock.Attributes["EQ"].Value = invoiceDetail.Quantity.ToString();
                    xmlStock.Attributes["ET"].Value = invoiceDetail.Total.ToString();
                    xmlStock.Attributes["ED"].Value = DateTime.Now.ToString();

                    int quantity = int.Parse(xmlStock.Attributes["Quantity"].Value) - invoiceDetail.Quantity;
                    xmlStock.Attributes["Quantity"].Value = quantity.ToString();

                    UpdateExportStock(invoiceDetail);
                }
            }

            DataProvider.Close();
        }

        private void UpdateExportStock(InvoiceDetail invoiceDetail)
        {
            foreach (var item in _unitOfWork.Stocks)
            {
                if (item.Product.Id == invoiceDetail.Product.Id)
                {
                    item.Export(invoiceDetail.Product, invoiceDetail.Quantity);
                    break;
                }
            }
        }

        private static void CreateInvoiceInXml(Invoice invoice)
        {
            var newStockNode = DataProvider.CreateNode("Invoice");

            var attr = DataProvider.CreateAttr("Id");
            attr.Value = invoice.Id;
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Username");
            attr.Value = Authentication.Username;
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Quantity");
            attr.Value = invoice.Quanity.ToString();
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Total");
            attr.Value = invoice.Total.ToString();
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("CreatedAt");
            attr.Value = invoice.CreatedAt.ToString();
            newStockNode.Attributes.Append(attr);

            foreach (var item in invoice.InvoiceDetails)
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

                newStockNode.AppendChild(newDetailChildNode);
            }

            DataProvider.PathData = Variables.InvoiceURL;
            DataProvider.Open();
            DataProvider.AppendNode(DataProvider.NodeRoot, newStockNode);
            DataProvider.Close();
        }

        private void SaveAvailable(Invoice invoice)
        {
            DataProvider.PathData = Variables.AvailableURL;
            DataProvider.Open();

            foreach (InvoiceDetail invoiceDetail in invoice.InvoiceDetails)
            {
                XmlNode xmlStock = DataProvider.GetNode($"//Available[@ProductId='{invoiceDetail.Product.Id}']");

                if (xmlStock != null)
                {
                    int newAvailable = int.Parse(xmlStock.Attributes["InStock"].Value) + invoiceDetail.Quantity;
                    xmlStock.Attributes["InStock"].Value = newAvailable.ToString();

                    UpdateAvailable(invoiceDetail);
                }
                else
                {
                    CreateAvailableInXml(invoiceDetail);
                    _unitOfWork.Availables.Add(new Available(invoiceDetail.Product, invoiceDetail.Quantity));
                }
            }

            DataProvider.Close();
        }

        private void CreateAvailableInXml(InvoiceDetail invoiceDetail)
        {
            var newStockNode = DataProvider.CreateNode("Available");

            var attr = DataProvider.CreateAttr("ProductId");
            attr.Value = invoiceDetail.Product.Id;
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("InStock");
            attr.Value = invoiceDetail.Quantity.ToString();
            newStockNode.Attributes.Append(attr);

            DataProvider.AppendNode(DataProvider.NodeRoot, newStockNode);
        }

        private void UpdateAvailable(InvoiceDetail invoiceDetail)
        {
            foreach (var item in _unitOfWork.Availables)
            {
                if (item.Product.Id == invoiceDetail.Product.Id)
                {
                    item.Update(invoiceDetail.Quantity);
                    break;
                }
            }
        }
    }


    public interface IInvoiceService
    {
        void CreateInvoice(ObservableCollection<InvoiceDetail> invoiceDetails);
        ObservableCollection<Invoice> GetInvoicesByDate(DateTime date);
        Invoice GetById(string id);
    }
}
