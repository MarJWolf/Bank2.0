using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class BankAccount
    {
        public int ID { get; set;}

        [ForeignKey("ID_currency")]
        public virtual Currency currency { get; set; }

        public float INTEREST { get; set; }

        public float BALANCE { get; set; }

        [ForeignKey("EGN_client")]
        public virtual Client client { get; set; }

        public BankAccount()
        {

        }

    }
}
