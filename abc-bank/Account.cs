using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {

        private static int count = 0;
        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;
        public const string INTEREST_MEMO = "Interest Paid";

        private readonly int accountType;
        private readonly int accountNumber;
        public List<Transaction> transactions;

        //Included current balance as reading transactions every time would create performance issue in readl world(multiple transactions)
        private double currentBalance;

        public System.Object lockObject = new System.Object();

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.accountNumber = count++;
        }

        public int AccountNumber()
        {
            return this.accountNumber;
        }

        //Get the account balance
        public double getBalance()
        {
            currentBalance = sumTransactions();
            return currentBalance;
        }

        public void Deposit(double amount, string memo = "")
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else {
                // Lock the operation so that no one can perform withdraw while deposit in progress.               
                lock (lockObject)
                {
                    currentBalance = currentBalance + amount;
                    transactions.Add(new Transaction(amount, memo));
                }
            }
        }

        public void Withdraw(double amount, string memo = "")
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else if (amount > currentBalance)
            {
                throw new ArgumentException("amount must be less than balance");
            }
            else {
                // Lock the operation so that no one can perform deposit while withraw in progress.
                lock (lockObject)
                {
                    currentBalance = currentBalance - amount;
                    transactions.Add(new Transaction(-amount, memo));
                }
            }
        }

        public void dailyInterestAccure()
        {
            double interest = calculateInterest() / 365;
            if (interest > 0)
                this.Deposit(calculateInterest() / 365, "Interest Paid");
        }

        public double InterestEarned()
        {
            //Uncomment this to get the total of daily interest accured.
            // return CheckIfTransactionsExist(false, true);
            return calculateInterest();
        }

        private double calculateInterest()
        {
            double amount = sumTransactions();
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;
                case MAXI_SAVINGS:
                    double rate = 0.05;
                    for (int i = 0, j = transactions.Count - 1; j >= 0 && i < 10; j--, i++)
                    {
                        if (transactions[j].Trans_Type.Equals(Transaction.Trans_Withdraw) &&
                            (DateTime.Now - transactions[j].transactionDate).Days <= 10)
                        {
                            rate = 0.001;
                            break;
                        }

                    }
                    return amount * rate;
                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions()
        {
            return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll, bool interestTransaction = false)
        {
            double amount = 0.0;
            double interest = 0.0;
            foreach (Transaction t in transactions)
            {
                amount += t.amount;
                if (interestTransaction && t.memo.Equals(INTEREST_MEMO))
                    interest += t.amount;
            }
            if (interestTransaction)
                return interest;
            return amount;
        }

        public int GetAccountType()
        {
            return accountType;
        }

    }
}
