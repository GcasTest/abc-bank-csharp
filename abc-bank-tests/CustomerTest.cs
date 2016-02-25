using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        Customer oscar;
        Account checkingAccount;
        Account savingsAccount;
        Account maxiSavingAccount;

        [TestInitialize]
        public void Initialize()
        {
            oscar = new Customer("Oscar");
            checkingAccount = new CheckingAccount();
            savingsAccount = new SavingsAccount();
            maxiSavingAccount = new MaxiSavingsAccount();
        }

        [TestMethod]
        public void Get_Statement_Success()
        {
            oscar.OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M,TranscationType.CREDIT);
            savingsAccount.Deposit(4000.0M, TranscationType.CREDIT);
            savingsAccount.Withdraw(200.0M);

            Assert.AreEqual("Statement for Oscar" + Environment.NewLine +
                    Environment.NewLine +
                    "Checking Account" + Environment.NewLine +
                    "  deposit $100.00" + Environment.NewLine +
                    "Total $100.00" + Environment.NewLine +
                    Environment.NewLine +
                    "Savings Account" + Environment.NewLine +
                    "  deposit $4,000.00" + Environment.NewLine +
                    "  withdrawal $200.00" + Environment.NewLine +
                    "Total $3,800.00" + Environment.NewLine +
                    Environment.NewLine +
                    "Total In All Accounts $3,900.00", oscar.GetStatement());
        }

        [TestMethod]
        public void Get_Number_of_Account_One()
        {
            oscar.OpenAccount(savingsAccount);
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }


        [TestMethod]
        public void Get_Number_of_Account_Three()
        {
            oscar.OpenAccount(checkingAccount);
            oscar.OpenAccount(savingsAccount);
            oscar.OpenAccount(maxiSavingAccount);
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Account of this type already exist!")]
        public void Test_Open_Account_Invalid()
        {
            oscar.OpenAccount(savingsAccount);
            oscar.OpenAccount(savingsAccount);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Not enough fund available")]
        public void Transfer_Funds_Failure_Fund_Unavailable()
        {
            oscar.OpenAccount(checkingAccount);
            oscar.OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M,TranscationType.CREDIT);
            savingsAccount.Deposit(500.0M,TranscationType.CREDIT);

            oscar.TransferFundsBetweenAccounts(checkingAccount, savingsAccount, 200.0M);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid account")]
        public void Transfer_Funds_Failure_Invalid_Account()
        {
            oscar.OpenAccount(checkingAccount);
            oscar.OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M,TranscationType.CREDIT);
            savingsAccount.Deposit(500.0M, TranscationType.CREDIT);

            oscar.TransferFundsBetweenAccounts(checkingAccount, maxiSavingAccount, 200.0M);
        }

        [TestMethod]
        public void Transfer_Funds_Success()
        {
            oscar.OpenAccount(checkingAccount);
            oscar.OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M, TranscationType.CREDIT);
            savingsAccount.Deposit(500.0M, TranscationType.CREDIT);

            oscar.TransferFundsBetweenAccounts(checkingAccount, savingsAccount, 50.0M);
            Assert.AreEqual(50.0M, checkingAccount.CurrentBalance);
            Assert.AreEqual(550.0M, savingsAccount.CurrentBalance);
        }

        [TestCleanup]
        public void CleanUp()
        {
            checkingAccount = null;
            savingsAccount = null;
            maxiSavingAccount = null;
            oscar = null;
        }

    }
}
