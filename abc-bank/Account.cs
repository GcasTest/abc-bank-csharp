using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        private readonly AccountType accountType;
        public List<Transaction> transactions;
        private double interest;

        public Account(AccountType accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.interest = 0.0;
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }
        
        //calculate daily interst and add to account
        public viod updateInterest()
	    {
		    interest += InterestCalculator.calculateDailyInterest(this);
	    }
	    

        public double InterestEarned() 
        {
            return interest;
        }

        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll) 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public AccountType GetAccountType() 
        {
            return accountType;
        }

    }
}
