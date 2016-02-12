using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Accounts;
using abc_bank.Model;
using abc_bank.BusinessComponents.InterestStrategies;
using abc_bank.BusinessComponents.InterestStrategies.Impl;

namespace abc_bank.Accounts.Impl
{
    public class CheckingAccount : AccountBase
    {
        private const string accountType = "Checking Account";

        public CheckingAccount(IInterestStrategy interestStrategy)
        {
            base.Transactions = new List<Transaction>();
            base.InterestStrategy = interestStrategy;
        }

        public CheckingAccount() : this(new CheckingAccountInterestStrategy())
        {
        }

        protected override string GetAccountType() 
        {
            return accountType;
        }

    }
}
