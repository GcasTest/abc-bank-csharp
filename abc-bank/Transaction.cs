using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        //Added transaction type attribute 
        public const int Trans_Deposit = 1;
        public const int Trans_Withdraw = 2;

        public readonly int Trans_Type;

        public readonly double amount;
        public readonly DateTime transactionDate;
        public readonly string memo;

        public Transaction(double amount, string memo = "")
        {
            this.amount = amount;
            this.Trans_Type = amount > 0 ? Trans_Deposit : Trans_Withdraw;
            this.transactionDate = DateProvider.getInstance().Now();
            this.memo = memo;
        }
    }
}
