using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class MaxiSavingsAccount : Account
    {
        #region public
        public override decimal InterestEarned()
        {
            List<Transaction> pastTenDaysTransactions = this.transactions.Where(t => t.transactionDate >= DateProvider.GetInstance().Now().AddDays(-10)).ToList();

            if (pastTenDaysTransactions.Any(t => t.TransactionType.Equals(TranscationType.DEBIT)))
                 return base.CurrentBalance * 0.001M;
            else
                return base.CurrentBalance * 0.05M;
        }

        public override decimal InterestEarnedDaily()
        {
            return Decimal.Round(InterestEarned() / 365, 2);
        }

        public override String GetAccountStatementHeading()
        {
            return "MaxiSaving Account" + Environment.NewLine;
        }
        #endregion

    }
}
