using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.BusinessComponents.InterestStrategies.Impl
{
    public class MaxiSavingsAccountFivePercentInterestStrategy : IInterestStrategy
    {
        public decimal CalculateInterest(IAccount account)
        {
            var amount = account.SumTransactions();

            if (amount <= 0)
                return 0;

            DateTime tenDaysPrior = DateTime.Now.AddDays(-10);

            bool hasWithdrawalsInTenDaysPrior = account.Transactions.Any(t => 
                t.transactionType == Model.TransactionTypes.Withdrawal 
                    && t.transactionDate >= tenDaysPrior);

            if (hasWithdrawalsInTenDaysPrior)
                return amount * 0.001m;

            return amount * 0.05m;            
        }
    }
}
