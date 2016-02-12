using abc_bank.Accounts.Impl;
using abc_bank.BusinessComponents.InterestStrategies;
using abc_bank.BusinessComponents.InterestStrategies.Impl;
using abc_bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts.Impl
{
    public class SavingsAccount : AccountBase
    {
        private const string accountType = "Savings Account";

        public SavingsAccount(IInterestStrategy interestStrategy)
        {
            base.Transactions = new List<Transaction>();
            base.InterestStrategy = interestStrategy;
        }

        public SavingsAccount() : this(new SavingsAccountInterestStrategy())
        {
        }

        protected override string GetAccountType() 
        {
            return accountType;
        }

    }
}
