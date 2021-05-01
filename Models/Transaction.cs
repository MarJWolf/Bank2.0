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

        public virtual TransactionCategory transactionCategory { get; set; }

        public int TransCatId { get; set; }

        public float SUM { get; set; }

        public DateTime DATE { get; set; }

        public virtual Employee employee { get; set; }
        public int EmployeeId { get; set; }

        public virtual BankAccount bankAccount { get; set; }

        public int BankAccId { get; set; }
        public Transaction()
        {

        }
    }
}
