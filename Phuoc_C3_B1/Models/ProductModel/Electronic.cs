using Phuoc_C3_B1.Interfaces;
using System;


namespace Phuoc_C3_B1.Models
{
    public class Electronic : Product, IDiscount
    {
        public int Warranty { get; }
        public int ElectricPower { get; }
        public override string CategoryType { get { return "Electronic"; } }


        public Electronic(string id, string name, string producer, int priceInput, int warranty, int electricPower)
            : base(id, name, producer, priceInput)
        {
            Warranty = warranty;
            ElectricPower = electricPower;
        }


        public int GetDiscount()
        {
            return (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday) ? (int)(PriceOutput * 0.15) : 0;
        }

        public override Product MakeClone()
        {
            return new Electronic(Id, Name, Producer, PriceInput, Warranty, ElectricPower);
        }

        public override string GetCategory()
        {
            return "Electronic";
        }
    }
}
