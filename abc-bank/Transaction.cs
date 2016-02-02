using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        public readonly double amount;

        //Payal 02/01/2016 : : Modified the access modifier from private to public.
        public DateTime transactionDate;

        public Transaction(double amount) 
        {
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().Now();
        }
    }
}
