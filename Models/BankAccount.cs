using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class BankAccount
    {
        public int ID { get; set;}
        
        public virtual Currency currency { get; set; }
        
        public int? CurrencyId { get; set; }

        public float BALANCE { get; set; }

        public virtual Client client { get; set; }
        public int? ClientId { get; set; }

        public virtual AccountType acctype { get; set; }
        public int? AccTypeId { get; set; }
        public BankAccount()
        {

        }

    }
}
