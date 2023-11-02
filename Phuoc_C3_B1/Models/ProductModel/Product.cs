using Phuoc_C5_B2.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Phuoc_C3_B1.Models
{
    public abstract class Product : INotifyPropertyChanged
    {
        public string Id { get; }
        public string Name { get; }
        public string Producer { get; }

        private int _priceInput;
        public int PriceInput
        {
            get { return _priceInput; }
            set
            {
                _priceInput = value;
                OnPropertyChanged();
            }
        }

        public int PriceOutput { get { return GetPriceOutput(); } }
        public abstract string CategoryType { get; }


        public Product(string id, string name, string producer, int priceInput)
        {
            Id = id;
            Name = name;
            Producer = producer;
            PriceInput = priceInput;
        }


        public abstract Product MakeClone();

        public abstract string GetCategory();

        public void ChangePriceInput(int newPrice)
        {
            PriceInput = newPrice;
        }

        private int GetPriceOutput()
        {
            return PriceInput + (int)(PriceInput * 0.1) + (int)(PriceInput * Variables.Profit) + (PriceInput * (int)(Variables.StaffNumber * 0.012));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
