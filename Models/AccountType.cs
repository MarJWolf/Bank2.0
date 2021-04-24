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
        
        public double LIHVA { get; set; }

        public double MESECHNA_TAKSA { get; set; }

    }
}
