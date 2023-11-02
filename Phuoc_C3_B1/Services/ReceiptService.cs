using Phuoc_C3_B1.Models;
using Phuoc_C5_B2;
using Phuoc_C5_B2.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;


namespace Phuoc_C3_B1.Services
{
    public class ReceiptService : IReceiptService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        public void CreateReceipt(ObservableCollection<ReceiptDetail> receiptDetails, ObservableCollection<FoodReceipt> foodReceipts)
        {
            Receipt receipt = new Receipt(Authentication.Username);
            receipt.ReceiptDetails = new List<ReceiptDetail>(receiptDetails);

            _unitOfWork.Receipts.Add(receipt);

            foreach (var item in foodReceipts)
            {
                _unitOfWork.FoodReceipt.Add(item);
                CreateFoodReceiptInXml(item);
            }

            SaveImportStock(receipt);
            CreateReceiptInXml(receipt);
        }


        // ----------------- Stock ----------------- //
        private void SaveImportStock(Receipt receipt)
        {
            DataProvider.PathData = Variables.StockURL;
            DataProvider.Open();

            foreach (ReceiptDetail receiptDetail in receipt.ReceiptDetails)
            {
                XmlNode xmlStock = DataProvider.GetNode($"//Stock[@ProductId='{receiptDetail.Product.Id}']");

                if (xmlStock != null)
                {
                    int prevIQ = int.Parse(xmlStock.Attributes["IQ"].Value) + int.Parse(xmlStock.Attributes["PrevIQ"].Value);
                    xmlStock.Attributes["PrevIQ"].Value = prevIQ.ToString();

                    int prevIT = int.Parse(xmlStock.Attributes["IT"].Value) + int.Parse(xmlStock.Attributes["PrevIT"].Value);
                    xmlStock.Attributes["PrevIT"].Value = prevIT.ToString();

                    xmlStock.Attributes["IQ"].Value = receiptDetail.Quantity.ToString();
                    xmlStock.Attributes["IT"].Value = receiptDetail.Total.ToString();
                    xmlStock.Attributes["ID"].Value = DateTime.Now.ToString();

                    int quantity = int.Parse(xmlStock.Attributes["Quantity"].Value) + receiptDetail.Quantity;
                    xmlStock.Attributes["Quantity"].Value = quantity.ToString();

                    UpdateImportStock(receiptDetail);
                }
                else
                {
                    CreateStockInXml(receipt.CreatedAt, receiptDetail);
                    _unitOfWork.Stocks.Add(new Stock(receiptDetail.Product, DateTime.Now, receiptDetail.Quantity));
                }
            }

            DataProvider.Close();
        }

        private void CreateStockInXml(DateTime createdAt, ReceiptDetail receiptDetail)
        {
            var newStockNode = DataProvider.CreateNode("Stock");

            var attr = DataProvider.CreateAttr("ProductId");
            attr.Value = receiptDetail.Product.Id;
            newStockNode.Attributes.Append(attr);

            // Import
            attr = DataProvider.CreateAttr("PrevIQ");
            attr.Value = "0";
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("PrevIT");
            attr.Value = "0";
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("IQ");
            attr.Value = receiptDetail.Quantity.ToString();
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("IT");
            attr.Value = receiptDetail.Total.ToString();
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("ID");
            attr.Value = createdAt.ToString();
            newStockNode.Attributes.Append(attr);

            // Export
            attr = DataProvider.CreateAttr("PrevEQ");
            attr.Value = "0";
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("PrevET");
            attr.Value = "0";
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("EQ");
            attr.Value = "0";
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("ET");
            attr.Value = "0";
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("ED");
            attr.Value = new DateTime(1900, 1, 1).ToString();
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Quantity");
            attr.Value = receiptDetail.Quantity.ToString();
            newStockNode.Attributes.Append(attr);

            DataProvider.AppendNode(DataProvider.NodeRoot, newStockNode);
        }

        private void UpdateImportStock(ReceiptDetail receiptDetail)
        {
            foreach (var item in _unitOfWork.Stocks)
            {
                if (item.Product.Id == receiptDetail.Product.Id)
                {
                    item.Import(receiptDetail.Product, receiptDetail.Quantity);
                    break;
                }
            }
        }


        // ----------------- Food Receipt ----------------- //
        private void CreateFoodReceiptInXml(FoodReceipt foodReceipt)
        {
            var newStockNode = DataProvider.CreateNode("Receipt");

            var attr = DataProvider.CreateAttr("FoodId");
            attr.Value = foodReceipt.FoodId;
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("ReceiptId");
            attr.Value = foodReceipt.ReceiptId;
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("MfgDate");
            attr.Value = foodReceipt.MfgDate.ToString();
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("ExpDate");
            attr.Value = foodReceipt.ExpDate.ToString();
            newStockNode.Attributes.Append(attr);

            DataProvider.PathData = Variables.FoodReceiptURL;
            DataProvider.Open();
            DataProvider.AppendNode(DataProvider.NodeRoot, newStockNode);
            DataProvider.Close();
        }


        // ----------------- Receipt ----------------- //
        private void CreateReceiptInXml(Receipt receipt)
        {
            var newStockNode = DataProvider.CreateNode("Receipt");

            var attr = DataProvider.CreateAttr("Id");
            attr.Value = receipt.Id;
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Username");
            attr.Value = Authentication.Username;
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Quantity");
            attr.Value = receipt.Quanity.ToString();
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("Total");
            attr.Value = receipt.Total.ToString();
            newStockNode.Attributes.Append(attr);

            attr = DataProvider.CreateAttr("CreatedAt");
            attr.Value = receipt.CreatedAt.ToString();
            newStockNode.Attributes.Append(attr);

            foreach (var item in receipt.ReceiptDetails)
            {
                var newDetailChildNode = DataProvider.CreateNode("Detail");

                attr = DataProvider.CreateAttr("ProductId");
                attr.Value = item.Product.Id.ToString();
                newDetailChildNode.Attributes.Append(attr);

                attr = DataProvider.CreateAttr("PriceInput");
                attr.Value = item.Product.PriceInput.ToString();
                newDetailChildNode.Attributes.Append(attr);

                attr = DataProvider.CreateAttr("Quantity");
                attr.Value = item.Quantity.ToString();
                newDetailChildNode.Attributes.Append(attr);

                newStockNode.AppendChild(newDetailChildNode);
            }

            DataProvider.PathData = Variables.ReceiptURL;
            DataProvider.Open();
            DataProvider.AppendNode(DataProvider.NodeRoot, newStockNode);
            DataProvider.Close();
        }
    }


    public interface IReceiptService
    {
        void CreateReceipt(ObservableCollection<ReceiptDetail> receiptDetails, ObservableCollection<FoodReceipt> foodReceipts);
    }
}
