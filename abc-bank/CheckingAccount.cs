using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class CheckingAccount : Account
    {

        #region public
        public override decimal InterestEarned()
        {
            return base.CurrentBalance * 0.001M;

        }

        public override decimal InterestEarnedDaily()
        {
            return Decimal.Round(InterestEarned() / 365,2);
        }

        public override String GetAccountStatementHeading()
        {
            return "Checking Account" + Environment.NewLine;
        }
        #endregion


    }
}
