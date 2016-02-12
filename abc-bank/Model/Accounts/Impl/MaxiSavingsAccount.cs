using abc_bank.Accounts;
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
    public class MaxiSavingsAccount : AccountBase
    {
        private const string accountType = "Maxi Savings Account";

        public MaxiSavingsAccount(IInterestStrategy interestStrategy)
        {
            base.Transactions = new List<Transaction>();
            base.InterestStrategy = interestStrategy;
        }

        public MaxiSavingsAccount() : this(new MaxiSavingsAccountInterestStrategy())
        {
        }

        protected override string GetAccountType() 
        {
            return accountType;
        }

    }
}
