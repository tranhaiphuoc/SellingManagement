using Phuoc_C3_B1.Interfaces;
using System;


namespace Phuoc_C3_B1.Models
{
    public class Porcelain : Product, IDiscount
    {
        public string Material { get; }
        public override string CategoryType { get { return "Porcelain"; } }

        public Porcelain(string id, string name, string producer, int priceInput, string material)
            : base(id, name, producer, priceInput)
        {
            Material = material;
        }


        public int GetDiscount()
        {
            return (DateTime.Now.DayOfWeek == DayOfWeek.Saturday) ? (int)(PriceOutput * 0.3) : 0;
        }

        public override Product MakeClone()
        {
            return new Porcelain(Id, Name, Producer, PriceInput, Material);
        }

        public override string GetCategory()
        {
            return "Porcelain";
        }
    }
}
