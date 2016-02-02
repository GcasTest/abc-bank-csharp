using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        private String name;
        private List<Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String GetName()
        {
            return name;
        }

        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public String GetStatement() 
        {
            String statement = null;
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts) 
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String statementForAccount(Account a) 
        {
            String s = "";

           //Translate to pretty account type
            switch(a.GetAccountType()){
                case Account.CHECKING:
                    s += "Checking Account\n";
                    break;
                case Account.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case Account.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions) {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        //Payal 02/01/2016 : Modified this method to accept proper Currency formatter.
        private String ToDollars(double d)
        {
            return String.Format("{0:C2}", Math.Abs(d));
        }

        //Payal 02/01/2016 : Created new method to transfer money from one account to another
        public Customer IntraAccountTransfer(Account depositalAccount, Account withrawingAccount, double amount)
        {
            try
            {
                if (!accounts.Contains(withrawingAccount))
                    throw new ArgumentException("The Withdrawing Account doesn't belong to customer:  " + this.name);
                if (!accounts.Contains(depositalAccount))
                    throw new ArgumentException("The Deposited Account doesn't belong to customer:  " + this.name);                
                    withrawingAccount.Withdraw(amount);
                    depositalAccount.Deposit(amount);                
            }
            catch (Exception ex)
            {
                //Catch the exception and display it
                Console.Write(ex.StackTrace);
                throw ex;
            }
            return this;
        }

        //Payal 02/01/2016 : Created new method to get total interest on daily basis
        public double TotalInterestAccruedDaily()
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.InterestAccruedDaily();
            return total;
        }
    }
}
