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
        private int id;
        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }
        public int ID
        {
            get { return id; }
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

        public void TransferBetweenAccs(double amount, int FromAccount, int ToAccount, Customer a = null, Customer b = null)
        {
            object o1, o2;

            if (a == null && b == null)
            {
                a = b = this;
                if (FromAccount > ToAccount)
                {
                    o1 = this.accounts[FromAccount];
                    o2 = this.accounts[ToAccount];
                }
                else
                {
                    o2 = this.accounts[FromAccount];
                    o1 = this.accounts[ToAccount];
                }
            }
            else
            {if (a.ID > b.ID)
                {
                    o1 = a.accounts[FromAccount];
                    o2 = b.accounts[ToAccount];
                }
            else
                {
                    o2 = a.accounts[FromAccount];
                    o1 = b.accounts[ToAccount];
                }
            }
            if (a.accounts[FromAccount] != null && b.accounts[ToAccount] != null && (FromAccount != ToAccount || a != b))//validate accounts
            {
                lock (o1)
                {    lock (o2)
                    {
                        double totalAmount = 0;
                        foreach (var t in a.accounts[FromAccount].transactions)
                        {
                            totalAmount += t.amount;
                        }
                        if (totalAmount < amount)//validate amount
                            throw new ArgumentException("Insufficient amount");
                        a.accounts[FromAccount].Withdraw(amount);
                        b.accounts[ToAccount].Deposit(amount);
                    }
            }
            }
            else throw new ArgumentException("Invalid Account");
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

        private String ToDollars(double d)
        {
            return String.Format("${0:#,###.##}", Math.Abs(d));
        }
    }
}
