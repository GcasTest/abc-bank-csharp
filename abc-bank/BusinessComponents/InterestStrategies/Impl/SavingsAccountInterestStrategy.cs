using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.BusinessComponents.InterestStrategies.Impl
{
    public class SavingsAccountInterestStrategy : IInterestStrategy
    {
        public decimal CalculateInterest(IAccount account)
        {
            var amount = account.SumTransactions();

            if (amount <= 0)
                return 0;

            if (amount <= 1000)
                return amount * 0.001m;
            else
                return 1 + (amount - 1000) * 0.002m;
        }
    }
}
