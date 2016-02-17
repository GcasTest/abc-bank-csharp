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
        private List<IAccount> accounts;

        public List<IAccount> Accounts { get { return accounts; } }

        public Customer(String name)
        {
            if (name.StartsWith(" "))
            {
                if (string.IsNullOrWhiteSpace(name))

                    throw new ArgumentException("Name cannot begin with a space");
            }
            this.name = name;
            this.accounts = new List<IAccount>();
        }

        public String GetName()
        {
            return name;
        }

        public void TransferFunds(IAccount fromAccnt, IAccount toAccnt, double amount)
        {
            //customer can use the transfer service which implement the ITransferFunds Interface
            //Future:this is good place to check for Authorization of customer for $ transfer before method call
            CustomerTransferFunds initiateTransfer = new CustomerTransferFunds();
            initiateTransfer.Account_To_Account_Transfer(this, fromAccnt, toAccnt, amount);
        }

        public Customer OpenAccount(IAccount account)
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
            foreach (IAccount a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public String GetStatement()
        {
            String statement = null;
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (IAccount a in accounts)
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String statementForAccount(IAccount a)
        {
            //String s = "";
            String s = a.accountType;

            //Translate to pretty account type
            /*    switch (a.GetAccountType()){
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
                */
            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions)
            {
                //s += "  " + /*t.transactionType*/ (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                s += "\n  " + t.transactionType() + " " + ToDollars(t.amount) /*+ "\n"*/;
                total += t.amount;
            }
            s += "\n" + "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double d)
        {
            //return String.Format("{$%,.2f}", Math.Abs(d));
            return string.Format("{0:C2}", Math.Abs(d));
        }
    }
}
