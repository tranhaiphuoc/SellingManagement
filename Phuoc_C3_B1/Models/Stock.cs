using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Phuoc_C3_B1.Models
{
    public class Stock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Product Product { get; }

        public int PrevImportQuantity { get; private set; }
        public int PrevImportTotal { get; private set; }

        public int ImportQuantity { get; private set; }
        public int ImportTotal { get; private set; }
        public DateTime ImportDate { get; private set; }

        public int PrevExportQuantity { get; private set; }
        public int PrevExportTotal { get; private set; }

        public int ExportQuantity { get; private set; }
        public int ExportTotal { get; private set; }
        public DateTime ExportDate { get; private set; }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }


        public Stock(Product product, DateTime importDate, int quantity)
        {
            Product = product;

            ImportQuantity = quantity;
            ImportTotal = quantity * product.PriceInput;
            ImportDate = importDate;

            Quantity = quantity;
        }

        public Stock(Product product,
            int prevIQ, int prevIT, int importQ, int importT, DateTime importD,
            int prevEQ, int prevET, int exportQ, int exportT, DateTime exportD)
        {
            Product = product;

            PrevImportQuantity = prevIQ;
            PrevImportTotal = prevIT;

            ImportQuantity = importQ;
            ImportTotal = importT;
            ImportDate = importD;

            PrevExportQuantity = prevEQ;
            PrevExportTotal = prevET;

            ExportQuantity = exportQ;
            ExportTotal = exportT;
            ExportDate = exportD;

            Quantity = PrevImportQuantity + ImportQuantity - PrevExportQuantity - ExportQuantity;
        }


        public void Import(Product product, int quantity)
        {
            PrevImportQuantity += ImportQuantity;
            PrevImportTotal += ImportTotal;

            ImportQuantity = quantity;
            ImportTotal = quantity * product.PriceInput;
            ImportDate = DateTime.Now;

            Quantity += ImportQuantity;
        }

        public void Export(Product product, int quantity)
        {
            PrevExportQuantity += ExportQuantity;
            PrevExportTotal += ExportTotal;

            ExportQuantity = quantity;
            ExportTotal = quantity * product.PriceOutput;
            ExportDate = DateTime.Now;

            Quantity -= ImportQuantity;
        }
    }
}
