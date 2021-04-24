using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Transaction
    {
        public int ID { get; set; }

        [ForeignKey("NAME_Category")]
        public virtual TransactionCategory transactionCategory { get; set; }

        public float SUM { get; set; }

        public DateTime DATE { get; set; }

        [ForeignKey("ID_employee")]
        public virtual Employee employee { get; set; }

        [ForeignKey("ID_bankAccount")]
        public virtual BankAccount bankAccount { get; set; }

        public Transaction()
        {

        }
    }
}
