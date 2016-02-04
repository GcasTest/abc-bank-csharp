using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void Savings_account()
        {
            Account savingsAccount = new Account(Account.SAVINGS);
            savingsAccount.Deposit(1500);

            Assert.AreEqual(2.0, savingsAccount.InterestEarned(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Checkings_account()
        {
            Account checkingsAccount = new Account(Account.CHECKING);
            checkingsAccount.Deposit(100);

            Assert.AreEqual(0.1, checkingsAccount.InterestEarned(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account_NoWithDraw()
        {
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            maxiSavingsAccount.Deposit(3000.0);

            Assert.AreEqual(150.0, maxiSavingsAccount.InterestEarned(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account_WithDraw()
        {
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
           
            maxiSavingsAccount.Deposit(3000.0);
            maxiSavingsAccount.Withdraw(1000.0);

            Assert.AreEqual(2.0, maxiSavingsAccount.InterestEarned(), DOUBLE_DELTA);
        }
    }
}
