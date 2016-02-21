using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class NewFeatureTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;
        Account checkingAccount;
        Account savingsAccount;
        Account maxiSavingsAccount;
        Customer customer;
 
        [TestInitialize]
        public void Setup()
        {
            // Set up Customer1
            customer = new Customer("Henry");

            // Set up accounts
            checkingAccount = new Account(AccountType.CHECKING, customer);
            savingsAccount = new Account(AccountType.SAVINGS, customer);
            maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, customer);
           
            // open accounts and make deposits/withdrawals
            customer.OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void TransferANegativeNumber()
        {
            try
            { 
                checkingAccount.Transfer(checkingAccount, -1);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("amount must be greater than zero", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "your current balance is less than the amount you want to transfer")]
        public void TransferMoreThenInAccount()
        {
            try
            {
                checkingAccount.Transfer(checkingAccount, 120);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("your current balance is less than the amount you want to transfer", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "you can only transfer between your accounts.")]
        public void TransferOutsideAccount()
        {

            Customer customer2 = new Customer("Henry2");
            Account customer2Account = new Account(AccountType.MAXI_SAVINGS, customer2);
            customer2.OpenAccount(customer2Account);
            customer2Account.Deposit(200);
            try
            {
                checkingAccount.Transfer(customer2Account, 40);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("you can only transfer between your accounts", ex.Message);
                throw;
            }
        }

        #region "Feature 1"
        [TestMethod]
        public void TransferMoney()
        {
            checkingAccount.Transfer(savingsAccount, 80);

            Assert.AreEqual(checkingAccount.CurrentBalance, 20);
            Assert.AreEqual(savingsAccount.CurrentBalance, 3880);
        }

        #endregion

        [TestMethod]
        public void TestCompaoundInterest()
        {
            Bank bank = new Bank();
            customer = new Customer("John");
            customer.OpenAccount(checkingAccount);
            bank.AddCustomer(customer);
            checkingAccount.Deposit(100.0);

            double test1 = bank.TotalInterestPaid(DateTime.Now.AddYears(1));
            // Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
            double test2 = CalculateTotalWithCompoundInterest(200, 0.001, 365, 1);
        }


        #region "Featrure 2"

        [TestMethod]
        public void MaxiSavingsAccount()
        {
            Bank bank = new Bank();
            customer = new Customer("Bill");
            bank.AddCustomer(customer.OpenAccount(maxiSavingsAccount));
            maxiSavingsAccount.Deposit(3000.0);
            maxiSavingsAccount.Deposit(100.0);
            maxiSavingsAccount.Withdraw(100.0);

            Assert.AreEqual(154.23451714061685, bank.TotalInterestPaid(DateTime.Now.AddYears(1)), DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiSavingsAccount9days()
        {
            Bank bank = new Bank();
            customer = new Customer("Bill");
            bank.AddCustomer(customer.OpenAccount(maxiSavingsAccount));
            maxiSavingsAccount.Deposit(3000.0);
            maxiSavingsAccount.Deposit(100.0);
            maxiSavingsAccount.Withdraw(100.0);

            Assert.AreEqual(0.073973413402654842, bank.TotalInterestPaid(DateTime.Now.AddDays(9)), DOUBLE_DELTA);
        }
        #endregion


        static double CalculateTotalWithCompoundInterest(double principal, double interestRate, int compoundingPeriodsPerYear, double yearCount)
        {
            return principal * (double)Math.Pow((double)(1 + interestRate / compoundingPeriodsPerYear), compoundingPeriodsPerYear * yearCount)-principal;
        }

    }
}
