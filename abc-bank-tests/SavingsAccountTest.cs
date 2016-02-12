using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts.Impl;


namespace abc_bank_tests
{
    [TestClass]
    public class SavingAccountTest
    {
        [TestMethod]
        public void SavingAccountTest_SumTransactions()
        {
            IAccount savingsAccount = new SavingsAccount();            

            savingsAccount.Deposit(450.75m);
            savingsAccount.Deposit(120.32m);
            savingsAccount.Withdraw(4.67m);

            Assert.AreEqual(566.40m, savingsAccount.SumTransactions());
        }

        [TestMethod]
        public void SavingsAccountTest_InterestEarned_Less_Than_A_Thousand()
        {
            IAccount savingsAccount = new SavingsAccount();

            savingsAccount.Deposit(450.75m);
            savingsAccount.Deposit(120.50m);
            savingsAccount.Withdraw(4.60m);

            Assert.AreEqual(.56665m, savingsAccount.InterestEarned());
        }

        [TestMethod]
        public void SavingsAccountTest_InterestEarned_More_Than_A_Thousand()
        {
            IAccount savingsAccount = new SavingsAccount();

            savingsAccount.Deposit(1200);
            var actual = savingsAccount.InterestEarned();

            Assert.AreEqual(1.4m, actual);
        }


    }
}
