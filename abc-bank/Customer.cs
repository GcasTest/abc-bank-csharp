using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        public string Name { get; set; }

        public List<Account> Accounts { get; set; }

        #region public
        public Customer(String name)
        {
            this.Name = name;
            this.Accounts = new List<Account>();
        }

        public Customer OpenAccount(Account account)
        {
            if (CheckIfAccountExists(account))
                throw new ArgumentException("Account of this type already exist!");
            else
            {
                Accounts.Add(account);
                return this;
            }
        }


        public int GetNumberOfAccounts()
        {
            return Accounts.Count;
        }

        public decimal TotalInterestEarned() 
        {
            decimal total = 0;
            foreach (Account a in Accounts)
                total += a.InterestEarned();
            return total;
        }

        public void TransferFundsBetweenAccounts(Account from, Account to, decimal amount)
        {
            decimal FromAccountBalanceBeforeTransfer = from.CurrentBalance;
            decimal ToAccountBalanceBeforeTransfer = to.CurrentBalance;
            Guid? FundWithdrawID = null;
            Guid? FundDepositeID = null;

            if (!IsTransferValid(from, to, amount))
                return;

            try
            {
                FundWithdrawID = from.Withdraw(amount);
                FundDepositeID = to.Deposit(amount,TranscationType.CREDIT);
            }
            catch (Exception e)
            {
                if (FundWithdrawID != null)
                    from.transactions.RemoveAll(t => t.TransactionId == FundWithdrawID);
                if (FundDepositeID != null)
                    to.transactions.RemoveAll(t => t.TransactionId == FundDepositeID);
                from.CurrentBalance = FromAccountBalanceBeforeTransfer;
                to.CurrentBalance = ToAccountBalanceBeforeTransfer;

                throw new Exception("Error occured! Please try again later");
            }

        }   

        public void DepositeDailyInterest(List<Account> accounts)
        {
            foreach (Account a in accounts)
            {
                a.DepositeDailyInterest();
            }
        }

        public String GetStatement() 
        {
            String statement = null;
            statement = "Statement for " + Name + Environment.NewLine;
            decimal total = 0.0M;
            foreach (Account a in Accounts) 
            {
                statement += Environment.NewLine + a.StatementForAccount() + Environment.NewLine;
                total += a.CurrentBalance; //a.sumTransactions();
            }
            statement += Environment.NewLine + "Total In All Accounts " + ToDollars(total);
            return statement;
        }
        #endregion

        #region private

        private bool IsTransferValid(Account from, Account to, decimal amount)
        {
            if (CheckIfAccountExists(from) && CheckIfAccountExists(to))
            {
                if (from.CurrentBalance >= amount)
                    return true;
                else
                    throw new ArgumentException("Not enough fund available");
            }
            else
                throw new ArgumentException("Invalid account");
        }

        private bool CheckIfAccountExists(Account account)
        {
            bool accountExists = false;
            foreach (Account a in this.Accounts)
            {

                if (a.GetType() == account.GetType())
                    accountExists = true;
            }
            return accountExists;
        }


        private String ToDollars(decimal d)
        {
            return String.Format("{0:c}", Math.Abs(d));
        }

        #endregion
    }
}
