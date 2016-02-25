using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public abstract class Account
    {

        public List<Transaction> transactions;

        public decimal CurrentBalance { get; set; }


        #region public

        public Account() 
        {
            this.transactions = new List<Transaction>();
        }

        public Guid Deposit(decimal amount, TranscationType transactionType) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transaction transaction = new Transaction(amount, transactionType);
                transactions.Add(transaction);
                this.CurrentBalance += amount;
                return transaction.TransactionId;
            }
        }

        public Guid Withdraw(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transaction transaction = new Transaction(-amount, TranscationType.DEBIT);
                transactions.Add(transaction);
                this.CurrentBalance -= amount;
                return transaction.TransactionId;
            }
        }

        public abstract decimal InterestEarnedDaily();
        
        public abstract decimal InterestEarned();

        public virtual String GetAccountStatementHeading()
        {
            return "Base Account" + Environment.NewLine;
        }

        public String StatementForAccount()
        {
            StringBuilder s = new StringBuilder();

            s.Append(this.GetAccountStatementHeading());

            decimal total = 0.0M;
            foreach (Transaction t in this.transactions)
            {
                s.Append("  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + Environment.NewLine);
                total += t.amount;
            }
            s.Append("Total " + ToDollars(total));
            return s.ToString();
        }

        public void DepositeDailyInterest()
        {
            if (HasInterestBeenPaid())
                throw new ArgumentException("Interest has been for today");

            this.Deposit(InterestEarnedDaily(), TranscationType.INTEREST);
        }

        #endregion

        #region private

        private bool HasInterestBeenPaid()
        {
            return this.transactions.Any(t => t.transactionDate.ToShortDateString() == DateTime.Today.ToShortDateString() 
                                            && t.TransactionType == TranscationType.INTEREST);
        }


        private String ToDollars(decimal d)
        {
            return String.Format("{0:c}", Math.Abs(d));
        }
        #endregion


    }
}
