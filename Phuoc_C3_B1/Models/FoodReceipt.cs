using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phuoc_C3_B1.Models
{
    public class FoodReceipt
    {
        public string FoodId { get; set; }
        public string ReceiptId { get; set; }
        public DateTime MfgDate { get; set; }
        public DateTime ExpDate { get; set; }


        public FoodReceipt(string foodId, string receiptId, DateTime mfgDate, DateTime expDate)
        {
            FoodId = foodId;
            ReceiptId = receiptId;
            MfgDate = mfgDate;
            ExpDate = expDate;
        }
    }
}
