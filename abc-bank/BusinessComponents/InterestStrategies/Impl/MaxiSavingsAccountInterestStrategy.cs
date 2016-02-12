using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.BusinessComponents.InterestStrategies.Impl
{
    public class MaxiSavingsAccountInterestStrategy : IInterestStrategy
    {
        public decimal CalculateInterest(IAccount account)
        {
            var amount = account.SumTransactions();

            if (amount <= 0)
                return 0;

            if (amount <= 1000)
                return amount * 0.02m;

            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05m;

            return 70 + (amount - 2000) * 0.1m;
        }
    }
}
