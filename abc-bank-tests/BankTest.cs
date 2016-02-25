using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        Bank bank;
        Account checkingAccount;
        Account savingsAccount;
        Account maxiSavingAccount;
        Customer john;
        Customer henry;

        [TestInitialize]
        public void Initialize()
        {
            bank = new Bank();
            checkingAccount = new CheckingAccount();
            savingsAccount = new SavingsAccount();
            maxiSavingAccount = new MaxiSavingsAccount();
            
            john = new Customer("John");
            henry = new Customer("Henry");

            checkingAccount.Deposit(100.0M, TranscationType.CREDIT);
            savingsAccount.Deposit(1500.0M, TranscationType.CREDIT);
            maxiSavingAccount.Deposit(3000.0M, TranscationType.CREDIT);
        }


        [TestMethod]
        public void Customer_Summary() 
        {
            bank.AddCustomer(john.OpenAccount(checkingAccount));

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void Total_Interest_Paid_CheckingAccount() {
            bank.AddCustomer(john.OpenAccount(checkingAccount));

            Assert.AreEqual(0.1M, bank.TotalInterestPaid());
        }

        [TestMethod]
        public void Total_Interest_Paid_SavingsAccount() {
            bank.AddCustomer(john.OpenAccount(savingsAccount));

            Assert.AreEqual(2.0M, bank.TotalInterestPaid());
        }

        [TestMethod]
        public void Total_Interest_Paid_MaxiSavingsAccount() {
            bank.AddCustomer(john.OpenAccount(maxiSavingAccount));

            Assert.AreEqual(150.0M, bank.TotalInterestPaid());
        }

        [TestMethod]
        public void Deposite_Daily_Interest_MaxiSaving_No_Withdrawal_Past_TenDays_Success()
        {
            john.OpenAccount(maxiSavingAccount);

            bank.AddCustomer(john);

            bank.DepositeDailyInterestForAllCustomers();

            Assert.AreEqual(3000.41M, maxiSavingAccount.CurrentBalance);
        }

        [TestMethod]
        public void Deposite_Daily_Interest_MaxiSaving_Withdrawal_Past_TenDays_Success()
        {
            maxiSavingAccount.Deposit(7000.0M, TranscationType.CREDIT);
            maxiSavingAccount.Withdraw(500.0M);
            john.OpenAccount(maxiSavingAccount);

            bank.AddCustomer(john);

            bank.DepositeDailyInterestForAllCustomers();

            Assert.AreEqual(9500.03M, maxiSavingAccount.CurrentBalance);
        }

        [TestMethod]
        public void Deposite_Daily_Interest_Savings_Success()
        {
            savingsAccount.Deposit(3000.0M, TranscationType.CREDIT);
            john.OpenAccount(savingsAccount);

            bank.AddCustomer(john);

            bank.DepositeDailyInterestForAllCustomers();

            Assert.AreEqual(4500.02M, savingsAccount.CurrentBalance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "amount must be greater than zero")]
        public void Deposite_Daily_Interest_Checking_Failure()
        {
            john.OpenAccount(checkingAccount);

            bank.AddCustomer(john);

            bank.DepositeDailyInterestForAllCustomers();

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Interest has been paid for today")]
        public void Deposite_Daily_Interest_Twice_Failure()
        {
            john.OpenAccount(maxiSavingAccount);

            bank.AddCustomer(john);

            bank.DepositeDailyInterestForAllCustomers();
            bank.DepositeDailyInterestForAllCustomers();

        }

        [TestCleanup]
        public void CleanUp()
        {
            bank = null;
            checkingAccount = null;
            savingsAccount = null;
            maxiSavingAccount = null;
            john = null;
        }
    }
}
