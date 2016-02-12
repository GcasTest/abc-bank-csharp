using abc_bank.BusinessComponents.InterestStrategies;
using abc_bank.Common;
using abc_bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace abc_bank.Accounts.Impl
{
    public abstract class AccountBase : IAccount
    {
        public List<Transaction> Transactions { get; set; }

        public IInterestStrategy InterestStrategy { get; set; }

        protected abstract string GetAccountType();

        public virtual decimal InterestEarned()
        {
            return InterestStrategy.CalculateInterest(this);
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                Transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            var accountTotal = SumTransactions();
            if (accountTotal - amount < 0)
            {
                throw new ArgumentException("cannot withdraw more than current account balance");
            }
            else
            {
                Transactions.Add(new Transaction(-amount));
            }
        }

        public decimal SumTransactions()
        {
            decimal amount = 0.0m;
            foreach (Transaction t in Transactions)
                amount += t.amount;
            return amount;
        }

        public String StatementForAccount()
        {
            StringBuilder statement = new StringBuilder();

            statement.AppendLine(GetAccountType());

            //Now total up all the transactions
            decimal total = 0.0m;
            foreach (Transaction t in Transactions)
            {
                statement.Append("  ");
                statement.Append(t.transactionType.ToString().ToLower());
                statement.Append(" ");
                statement.AppendLine(Utilities.ToDollars(t.amount));

                total += t.amount;
            }

            statement.Append("Total ");
            statement.Append(Utilities.ToDollars(total));
            return statement.ToString();
        }

        public void SetInterestStrategy(IInterestStrategy interestStrategy)
        {
            this.InterestStrategy = interestStrategy;
        }
    }
}
