using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;
        private readonly object o = new object();
       // private ReaderWriterLock rwl = new ReaderWriterLock();
        private readonly int accountType;
        public List<Transaction> transactions;
        private DateTime accountOpenTime;

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.accountOpenTime = DateTime.Now;
        }


        public void Deposit(double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                lock (o)
                { transactions.Add(new Transaction(amount)); }
            }
        }

        public void Withdraw(double amount)
        {  
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                lock(o)
                { transactions.Add(new Transaction(-amount));}
            }
        }
       

    public double InterestEarned() 
        {
            //double amount = sumTransactions();
            switch(accountType){
                case SAVINGS:  
                        return weightAveAmount(0.001, 0.002, 1000);
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    TimeSpan spanTransaction;
                    bool IsWithdrawInTenDays = false;
                    foreach (Transaction t in transactions)
                    {
                        if (t.amount < 0)
                        {
                            spanTransaction = DateTime.Now.Subtract(t.TransactionDate);
                            if (spanTransaction.TotalDays <= 10)
                            {
                                IsWithdrawInTenDays = true;
                                break;
                            }
                        }
                    }
                    if (!IsWithdrawInTenDays)
                        return weightAveAmount(0.05);
                    else
                        return weightAveAmount(0.001);

                //if (amount <= 1000)
                //    return amount * 0.02;
                //if (amount <= 2000)
                //    return 20 + (amount-1000) * 0.05;
                //return 70 + (amount-2000) * 0.1;
                default:
                    return weightAveAmount( 0.001) ;
            }
        }
         public double weightAveAmount(double rate1,double rate2=0,double threshhold=0)//do not need to seperate deposite and withdraw; simple rate,not compound rate
        {
            double interest = 0.0;
            double principle = 0.0;
            if (threshhold == 0)
                foreach (Transaction t in transactions)
                {
                    TimeSpan span = DateTime.Now.Subtract(t.TransactionDate);
                    interest += t.amount * rate1 * span.TotalDays / 365;
                    principle += t.amount;
                }
            else
                foreach (Transaction t in transactions)
                {
                    TimeSpan span = DateTime.Now.Subtract(t.TransactionDate);
                    principle += t.amount;
                    if (principle <= threshhold)
                        interest += t.amount * rate1 * span.TotalDays / 365;
                    else if((principle- t.amount) > threshhold )
                        interest += t.amount * rate2 * span.TotalDays / 365;
                    else
                        interest += ((principle - 1000) * rate2 +(1000- (principle-t.amount))* rate1)* span.TotalDays / 365;
                }
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

        public int GetAccountType() 
        {
            return accountType;
        }
      
    }
}

