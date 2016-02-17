using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class CheckingAccount : IAccount
    {

        // int accountType;
        //also implement IDs
        string accountType = "Checking Account";
        public List<Transaction> transactions { get; set; }

        public CheckingAccount()
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
                if (accountBalance >= amount)
                    transactions.Add(new Transaction(-amount));
                else
                    throw new Exception("Insufficient Funds in account");
            }
        }
        /* not needed ; access the string property 
        public int GetAccountType()
          {
              throw new NotImplementedException();
          }
          */

        public double InterestEarned()
        {
            double amount = sumTransactions();

            return amount * 0.001;//use strategy pattern
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

    }
}
