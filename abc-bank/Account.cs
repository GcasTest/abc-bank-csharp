using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace abc_bank
{

    delegate double InterestCalculator(Account account, DateTime asofDate);
    public class Account
    {

        InterestCalculator interestCalculator = new InterestCalculator(InterestCalculatorSavings);

        public Account(AccountType accountType, Customer customer) 
        {
            AccountType = accountType;
            Transactions = new List<Transaction>();
            Customer = customer;
            if (accountType == AccountType.SAVINGS)
                interestCalculator = new InterestCalculator(InterestCalculatorSavings);
            if (accountType == AccountType.MAXI_SAVINGS)
                interestCalculator = new InterestCalculator(InterestCalculatorMaxi);
            if (accountType == AccountType.CHECKING)
                interestCalculator = new InterestCalculator(InterestCalculatorChecking);


        }


        #region Properties
        public List<Transaction> Transactions { get; private set; }
        public AccountType AccountType { get; private set; }
        public Customer Customer { get; private set; }
        #endregion

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                Transactions.Add(new Transaction(-amount));
            }
        }
        
        public void Transfer(Account toAccount, double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("amount must be greater than zero");

            if (this.CurrentBalance < amount)
                throw new ArgumentException("your current balance is less than the amount you want to transfer");

            if (this.Customer != toAccount.Customer)
                throw new ArgumentException("you can only transfer between your accounts");


            this.Withdraw(amount);
            toAccount.Deposit(amount);

        }

        public double CurrentBalance
        {
            get { return Transactions.Sum(x => x.Amount); }
        }


        public double InterestEarned(DateTime asOfDate) 
        {
            return interestCalculator(this, asOfDate);

            //double amount = SumTransactions();
            //switch(AccountType){
            //    case AccountType.SAVINGS:
            //        return savingsInterest(this, asOfDate);
            //        //if (amount <= 1000)
            //        //    return amount * 0.001;
            //        //else
            //        //    return 1 + (amount-1000) * 0.002;
            //    case AccountType.MAXI_SAVINGS:
            //        if (amount <= 1000)
            //            return amount * 0.02;
            //        if (amount <= 2000)
            //            return 20 + (amount-1000) * 0.05;
            //        return 70 + (amount-2000) * 0.1;
            //    case AccountType.CHECKING:
            //        return amount * 0.001;
            //    default:
            //        return amount * 0.001;
            //}
        }

        public double SumTransactions()
        {

            return CurrentBalance;
        }

        static double InterestCalculatorSavings(Account account, DateTime asofDate)
        {
            double amount = 0.0;
            amount = account.SumTransactions();
            if (amount <= 1000)
                return InterestRateHelper.CalculateTotalWithCompoundInterest(amount, 0.001, 365, 1);
            else
                return InterestRateHelper.CalculateTotalWithCompoundInterest(1000, 0.001, 365, 1) + (amount - 1000) * 0.002;

        }


        static double InterestCalculatorChecking(Account account, DateTime asofDate)
        {
            double amount = 0.0;
            amount = account.SumTransactions();
            
            return InterestRateHelper.CalculateTotalWithCompoundInterest(amount, 0.001, 365, 1); // 
         //   return amount * 0.001;
        }


        static double InterestCalculatorMaxi(Account account, DateTime asofDate)
        {

            //Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%
            
            double amount = 0.0;
            amount = account.SumTransactions();
            if (account.Transactions.Where(x => x.Amount < 0 && x.TransactionDate.AddDays(10) > asofDate.Date).Count()>0)
                return InterestRateHelper.CalculateTotalWithCompoundInterest(amount, 0.001, 365, 1); 
            else
                return InterestRateHelper.CalculateTotalWithCompoundInterest(amount, 0.05, 365, 1);

        }
        //private double CheckIfTransactionsExist(bool checkAll) 
        //{
        //    double amount = 0.0;
        //    foreach (Transaction t in Transactions)
        //        amount += t.Amount;
        //    return amount;
        //}

    }

    public enum AccountType
    {
        CHECKING = 0,
        SAVINGS = 1,
        MAXI_SAVINGS = 2
    }

    public class InterestRateHelper
    {

        public static double CalculateTotalWithCompoundInterest(double principal, double interestRate, int compoundingPeriodsPerYear, double yearCount)
        {
            return principal * (double)Math.Pow((double)(1 + interestRate / compoundingPeriodsPerYear), compoundingPeriodsPerYear * yearCount) - principal;
        }
    }
    
}
