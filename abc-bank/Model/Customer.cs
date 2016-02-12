using abc_bank.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Model
{
    public class Customer
    {
        private String name;
        private List<IAccount> accounts;

        public Customer(String name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name cannot be blank");

            this.name = name;
            this.accounts = new List<IAccount>();
        }

        public String GetName()
        {
            return name;
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

        public decimal TotalInterestEarned() 
        {
            decimal total = 0;
            foreach (IAccount a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public String GetStatement() 
        {
            StringBuilder statement = new StringBuilder();
            
            statement.Append("Statement for " + name.ToUpper());

            decimal total = 0.0m;
            foreach (IAccount a in accounts) 
            {
                statement.AppendLine(Environment.NewLine);
                statement.Append(a.StatementForAccount());

                total += a.SumTransactions();
            }

            statement.AppendLine(Environment.NewLine); 
            statement.Append("Total In All Accounts: ");
            statement.Append(Utilities.ToDollars(total));

            return statement.ToString();
        }
    }
}
