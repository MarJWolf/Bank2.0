using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class TransactionCategory
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        public int COEF { get; set; }

        public TransactionCategory() { }

    }
}
