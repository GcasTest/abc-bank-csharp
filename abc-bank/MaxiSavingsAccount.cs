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

            if (amount <= 1000)
                return amount * 0.02;
            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05;
            return 70 + (amount - 2000) * 0.1;
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
