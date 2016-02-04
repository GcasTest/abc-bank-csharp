using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    class BankApp
    {
        
        public static void main(String[] args)
        {
            Bank bofa = Bank.getInstance();
            while (true)
            {
                DateTime nextDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 1, 0, 0);
                Task t = Task.Run(async delegate
                        {
                            await Task.Delay((nextDay - DateTime.Now).Hours);
                            bofa.DailyInterestAccure();
                        }
                );
                t.Wait();
            }
        }
    }
}
