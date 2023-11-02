using Phuoc_C3_B1.Models;


namespace Phuoc_C3_B1.ViewModels
{
    public class ExpiredFood
    {
        public Food Food { get; set; }
        public FoodReceipt FoodReceipt { get; set; }


        public ExpiredFood(Food f, FoodReceipt fr)
        {
            Food = f;
            FoodReceipt = fr;
        }
    }
}
