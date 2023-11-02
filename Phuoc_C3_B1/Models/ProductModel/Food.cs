using Phuoc_C3_B1.Interfaces;
using System;


namespace Phuoc_C3_B1.Models
{
    public class Food : Product, IDiscount
    {
        public override string CategoryType { get { return "Food"; } }


        public Food(string id, string name, string producer, int priceInput)
            : base(id, name, producer, priceInput)
        { }


        public int GetDiscount()
        {
            return (DateTime.Now.DayOfWeek == DayOfWeek.Thursday) ? (int)(PriceOutput * 0.25) : 0;
        }

        public override Product MakeClone()
        {
            return new Food(Id, Name, Producer, PriceInput);
        }

        public override string GetCategory()
        {
            return "Food";
        }
    }
}
