using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        //Every Customer needs to have unique identification, so created Id using static counter
        private static int count = 0;
        private int id;

        private String name;
        private List<Account> accounts;
        public System.Object lockobject = new System.Object();

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
            this.id = count++;
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

        public void dailyInterestAccure()
        {
            foreach (Account a in accounts)
                a.dailyInterestAccure();
        }

        public double TotalInterestEarned()
        {
            double total = 0.0;
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
            switch (a.GetAccountType())
            {
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
            foreach (Transaction t in a.transactions)
            {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double d)
        {
            //return String.Format("$%,.2f", Math.Abs(d));
            //return String.Format("{0: 0,0.00}", Math.Abs(d));
            return Math.Abs(d).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
        }

        public void transfer(Account accountTo, Account accountFrom, int amount)
        {
            if (amount <= 0) return;
            if (accountFrom.getBalance() < amount) return;

            try
            {
                lock (lockobject)
                {
                    accountFrom.Withdraw(amount);
                    accountTo.Deposit(amount);
                }
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
    }
}
