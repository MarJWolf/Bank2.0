using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Currency
    {
        public int ID { get; set; }

        public string NAME { get; set; }

        public string FULL_NAME { get; set; }

        public double FIXED_RATE { get; set; }
    }
}
