using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class SavingsAccount : IAccount
    {
        string accountType = "Savings Account";
        public List<Transaction> transactions { get; set; }

        public SavingsAccount()
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
                return amount * 0.001;
            else
                return 1 + (amount - 1000) * 0.002;
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
