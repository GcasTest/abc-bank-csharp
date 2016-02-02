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

        private readonly int accountType;
        public List<Transaction> transactions;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
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
            double totalamount = sumTransactions();
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            //Payal : Check for Sufficient funds before withrawing
            else 
                if (amount <= sumTransactions())            
                    transactions.Add(new Transaction(-amount));            
            else throw new Exception("Insufficient funds");
        }

        
        public double InterestEarned() 
        {
            double amount = sumTransactions();
            
            switch(accountType){
                case SAVINGS:                    
                    if (amount <= 1000)
                        return amount * (0.001);
                    else
                        return 1 + (amount-1000) * (0.002);    

                case MAXI_SAVINGS:                    

                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05;
                    return 70 + (amount-2000) * 0.1;

                default:
                    return amount * 0.001;                                 
            }
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

        //Payal 02/01/2016 : Created new method to check whether there were any withdrawals in past 10 days
        public bool CheckWithdrawals()
        {
            var lstWithdrwals = (from x in transactions
                               where x.transactionDate >= DateTime.Now.Date.AddDays(-10) && x.amount < 0
                                     select x).ToList<Transaction>();
            return lstWithdrwals.Count > 0 ? true : false;  
        }

        //Payal 02/01/2016 : Method created to calculate interest accrued on daily basis.
        //Assumption: 1. There are 365 days in the year.
             //       2.Interest accreued daily is credited to customer account monthly.
        public double InterestAccruedDaily()
        {
            double amount = sumTransactions();

            switch (accountType)
            {
                case SAVINGS:                    
                    if (amount <= 1000)
                        return (amount * Math.Pow((1 + (0.001 / 365)), 365/12)) - amount;   
                    else
                        return (1000 * Math.Pow(1+ (0.001/365), 365/12)) + ((amount-1000) * Math.Pow((1 + (0.002 / 365)), 365/12)) - amount;   

                case MAXI_SAVINGS:
                //Changed the logic to calculate interest as per requirement for MAX SAVINGS account.
                if (CheckWithdrawals())
                    return (amount * Math.Pow((1 + (0.001 / 365)), 365/12)) - amount;                      
                else
                    return (amount * Math.Pow((1 + (0.05 / 365)), 365/12)) - amount;   

                default:
                return (amount * Math.Pow((1+(0.001/365)), 365/12)) - amount;                
            }
        }

    }
}
