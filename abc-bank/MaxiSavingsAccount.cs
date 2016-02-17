using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class MaxiSavingsAccount : IAccount
    {
        string accountType = "Maxi Savings Account";
        public List<Transaction> transactions { get; set; }

        public MaxiSavingsAccount()
        {
            this.transactions = new List<Transaction>();
        }

        string IAccount.accountType { get { return accountType; } }



        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else {
                double accountBalance = sumTransactions();
                if (accountBalance > amount)
                    transactions.Add(new Transaction(-amount));
                else
                    throw new Exception("Insufficient Funds in account");
            }
        }

        public double InterestEarned()
        {
            double amount = sumTransactions();



            if (checkWithDrawls_PriorDays(10))
                return amount * 0.001;
            else
                return amount * 0.05;


          /*  if (amount <= 1000)
                return amount * 0.02;
            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05;
            return 70 + (amount - 2000) * 0.1;
            */

        }

        public double sumTransactions()
        {
            return CheckIfTransactionsExist(true);
        }

        public double CheckIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }


        public bool checkWithDrawls_PriorDays(int numberOfDaysPrior)
        {
            
            foreach (Transaction t in transactions)
            {
                
                if (t.transactionType().Equals("withdrawls") && t.TransactionDate <= DateTime.Now.Date.AddDays(-numberOfDaysPrior))
                {
                    break;
                 
                }
                return true;
            }
          
            return false;

        }

    }
}
