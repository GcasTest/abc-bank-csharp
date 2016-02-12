using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.BusinessComponents.InterestStrategies
{
    public interface IInterestStrategy
    {
        decimal CalculateInterest(IAccount account);
    }
}
