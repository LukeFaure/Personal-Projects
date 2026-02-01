using Xunit;
using Account.Logic;

namespace Account.UnitTests.Logic
{
    public class Test
    {
        [Fact]
        public void TestCreateBankAccount()
        {
            string name = "Luke";
            string pin = "3692";
            int bsb = 100000;
            decimal balance = 0m;
            BankAccount account = new BankAccount(name, pin, bsb, balance);

            Assert.Equal(name, account.Name);
            Assert.Equal(balance, account.Balance);
            account.Deposit(500m);
            Assert.Equal(500m, account.Balance);
            account.Withdraw(300m);
            Assert.Equal(200m, account.Balance);
            account.Withdraw(200m);
            Assert.Equal(0m, account.Balance);
            var ex2 = Assert.Throws<ArgumentException>(() => account.Deposit(-80.3m));
            Assert.Equal("Error 005: amount must be greater than 0", ex2.Message);
        }
    }
}