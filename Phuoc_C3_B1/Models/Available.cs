using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Phuoc_C3_B1.Models
{
    public class Available : INotifyPropertyChanged
    {
        public Product Product { get; }

        private int _inStock;
        public int InStock
        {
            get { return _inStock; }
            set
            {
                _inStock = value;
                OnPropertyChanged();
            }
        }


        public Available(Product product, int inStock)
        {
            Product = product;
            InStock = inStock;
        }


        public void Update(int quantity)
        {
            InStock += quantity;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
