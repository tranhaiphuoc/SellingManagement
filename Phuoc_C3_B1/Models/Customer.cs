using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phuoc_C3_B1.Models
{
    public class Customer
    {
        private static int _id = 1;


        public int Id { get; }
        public string FullName { get; }
        public string SSN { get; }
        public string Phone { get; }
        public int Point { get; private set; }


        public Customer(string fullName, string ssn, string phone, int point)
        {
            Id = _id++;
            FullName = fullName;
            SSN = ssn;
            Phone = phone;
            Point = point;
        }


        public void AccumulatePoint(int point)
        {
            Point += point;
        }
    }
}
