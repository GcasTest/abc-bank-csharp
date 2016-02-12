using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts.Impl;
using abc_bank.BusinessComponents.InterestStrategies.Impl;


namespace abc_bank_tests
{
    [TestClass]
    public class MaxiSavingAccountTest
    {
        [TestMethod]
        public void MaxiSavingsAccountTest_SumTransactions()
        {
            IAccount maxiSavingsAccount = new MaxiSavingsAccount();

            maxiSavingsAccount.Deposit(450.75m);
            maxiSavingsAccount.Deposit(120.32m);
            maxiSavingsAccount.Withdraw(4.67m);

            Assert.AreEqual(566.40m, maxiSavingsAccount.SumTransactions());
        }

        [TestMethod]
        public void MaxiSavingsAccountTest_InterestEarned_Less_Than_A_Thousand()
        {
            IAccount maxiSavingsAccount = new MaxiSavingsAccount();

            maxiSavingsAccount.Deposit(500m);
            maxiSavingsAccount.Deposit(200m);
            maxiSavingsAccount.Withdraw(100m);

            Assert.AreEqual(12, maxiSavingsAccount.InterestEarned());
        }

        [TestMethod]
        public void MaxiSavingsAccountTest_InterestEarned_More_Than_A_Thousand()
        {
            IAccount maxiSavingsAccount = new MaxiSavingsAccount();

            maxiSavingsAccount.Deposit(150.0m);
            maxiSavingsAccount.Deposit(1520.0m);
            maxiSavingsAccount.Withdraw(95.0m);

            Assert.AreEqual(48.75m, maxiSavingsAccount.InterestEarned());
        }

        [TestMethod]
        public void MaxiSavingsAccountTest_InterestEarned_More_Than_Two_Thousand()
        {
            IAccount maxiSavingsAccount = new MaxiSavingsAccount();

            maxiSavingsAccount.Deposit(950.75m);
            maxiSavingsAccount.Deposit(1520.32m);
            maxiSavingsAccount.Withdraw(95.00m);

            Assert.AreEqual(107.607m, maxiSavingsAccount.InterestEarned());
        }

        [TestMethod]
        public void MaxiSavingsAccountTest_InterestEarned_FivePercent_Rates_Less_Than_A_Thousand()
        {
            IAccount maxiSavingsAccount = new MaxiSavingsAccount(new MaxiSavingsAccountFivePercentInterestStrategy());

            maxiSavingsAccount.Deposit(250.75m);
            maxiSavingsAccount.Deposit(120.25m);

            Assert.AreEqual(18.55m, maxiSavingsAccount.InterestEarned());
        }

        [TestMethod]
        public void MaxiSavingsAccountTest_InterestEarned_FivePercent_Rates_More_Than_A_Thousand()
        {
            IAccount maxiSavingsAccount = new MaxiSavingsAccount(new MaxiSavingsAccountFivePercentInterestStrategy());

            maxiSavingsAccount.Deposit(250.75m);
            maxiSavingsAccount.Deposit(120.25m);

            Assert.AreEqual(18.55m, maxiSavingsAccount.InterestEarned());
        }

        [TestMethod]
        public void MaxiSavingsAccountTest_InterestEarned_FivePercent_Rates_More_Than_A_Thousand_With_Withdrawals()
        {
            IAccount maxiSavingsAccount = new MaxiSavingsAccount(new MaxiSavingsAccountFivePercentInterestStrategy());

            maxiSavingsAccount.Deposit(250.75m);
            maxiSavingsAccount.Deposit(120.25m);
            maxiSavingsAccount.Withdraw(1);

            Assert.AreEqual(.37m, maxiSavingsAccount.InterestEarned());
        }
    }
}
