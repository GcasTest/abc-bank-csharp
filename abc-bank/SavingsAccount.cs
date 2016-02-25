using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class SavingsAccount : Account
    {
        #region public
        public SavingsAccount()
        {

        }
        public override decimal InterestEarned()
        {
            if (base.CurrentBalance <= 1000)
                return base.CurrentBalance * 0.001M;
            else
                return 1 + (base.CurrentBalance - 1000) * 0.002M;
            
        }
        public override decimal InterestEarnedDaily()
        {
            return Decimal.Round(InterestEarned() / 365, 2);
        }

        public override String GetAccountStatementHeading()
        {
            return "Savings Account" + Environment.NewLine;
        }
        #endregion
    }
}
