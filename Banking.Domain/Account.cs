namespace Account.Logic
{
    public class BankAccount
    {
        public override string ToString()
        {
            return $"Name: {Name}, Pin: {Pin}, BSB: {Bsb}, Balance: {Balance}";
        }
        public string Name { get; private set; }
        string Pin { get; set; }
        int Bsb { get; set; }
        public decimal Balance { get; private set; }
        public BankAccount(string name, string pin, int bsb, decimal balance)
        {
            if (name == null)
                throw new ArgumentException("Error 001: account name cannot be null");
            if (pin.Length != 4)
                throw new ArgumentException("Error 002: pin length must be 4 digits");
            if (!pin.All(char.IsAsciiDigit))
                throw new ArgumentException("Error 003: pin must be ascii digits");
            if (balance < 0m)
                throw new ArgumentException("Error 004: balance must be greater than 0");
            Name = name;
            Pin = pin;
            Bsb = bsb;
            Balance = balance;
        }
        private void ApplyDeposit(decimal value)
        {
            Balance += value;
        }
        private void ApplyWithdrawal(decimal value)
        {
            if (Balance - value < 0m)
                throw new InvalidOperationException("Withdrawal Declined: Insufficient Funds");
            else
            Balance -= value;
        }
        public void Deposit(decimal amount)
        {
            if (amount <= 0m)
                throw new ArgumentException("Error 005: amount must be greater than 0");
            ApplyDeposit(amount);
        }
        public void Withdraw(decimal amount)
        {
            if (amount <= 0m)
                throw new ArgumentException("Error 006: amount must be greater than 0");
            ApplyWithdrawal(amount);
        }
    }
}