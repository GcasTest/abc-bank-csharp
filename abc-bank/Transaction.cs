using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        //public readonly double amount;

        //private DateTime TransactionDate;

        public DateTime TransactionDate { get; private set; }
        public double Amount { get; private set; }

        public Transaction(double amount) 
        {
            this.Amount = amount;
            this.TransactionDate = DateProvider.getInstance().Now();
        }
    }
}
