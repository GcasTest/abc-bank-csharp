using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
	public struct InterestRate
	{
		public const double checking = 0.001/365;
		public const double savings1 = 0.001/365;
		public const double savings2 = 0.002/365;
		public const double maxi_savings1 = 0.05/365;
		public const double maxi_savings2 = 0.001/365;
	}

	public static class InterestCalculator
	{
		public static double calculateDailyInterest(Account account)
		{
			double amount = 0.0;
			AccountType accountType = account.GetAccountType();
			DateTime lastWithdrawDate = DateTime.MinValue;

			foreach(Transaction t in account)
			{
				amount += t.amount;
				if(accountType==AccountType.MAXI_SAVINGS && amount<0 && t.transactionDate>lastWithdrawDate)
				{
					lastWithdrawDate = t.transactionDate;
				}
			}

			switch(accountType){ 
                 	case AccountType.SAVINGS: 
                     	if (amount <= 1000) 
                        	return amount * InterestRate.savings1; 
                    	else 
                         	return amount * InterestRate.savings1 + (amount-1000) * InterestRate.savings2; 

                 	case AccountType.MAXI_SAVINGS: 
                     		if ( DateTime.Compare(lastWithdrawDate.AddDays(10), DateTime.Now)<0) 
                         		return amount * InterestRate.maxi_savings1; 
                     		else 
                     		 	return amount * InterestRate.maxi_savings2; 
                 	default: 
                     	return amount * InterestRate.checking; 
             	} 
	}
}
