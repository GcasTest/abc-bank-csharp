using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Model
{
    public class Transaction
    {
        public readonly decimal amount;
        public readonly TransactionTypes transactionType;
        public readonly DateTime transactionDate;

        public Transaction(decimal amount)
        {
            if(amount == 0)
                throw new ArgumentException("amount cannot be zero");
            
            this.amount = amount;
            this.transactionDate = DateTime.Now;
            this.transactionType = amount < 0 ? TransactionTypes.Withdrawal : TransactionTypes.Deposit;
        }

    }
}
