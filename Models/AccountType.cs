using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{   
   
    public class AccountType
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        [Range(1, 10)]
        public float INTEREST { get; set; }

        public double TAX { get; set; }

    }
}
