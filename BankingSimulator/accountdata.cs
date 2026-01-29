using BankingMain;
namespace AccountData
{
    public class AccountRecords
    {
        public override string ToString()
        {
            return $"Name: {Name}, Pin: {Pin}, BSB: {Bsb} Balance: ${Balance}";
        }
        public string? Name { get; set; }
        public int? Pin { get; set; }
        public int? Bsb { get; set; }
        public long? Balance { get; set; }

        public AccountRecords() {}
        public AccountRecords(string? name, int? pin, int? bsb, long? balance)
        {
            if (Program.accounts.Exists(a => a.Name == name || a.Pin == pin || a.Bsb == bsb))
                throw new ArgumentException("Error 010; the account name or the account pin already exists, please try again");

            Name = name;
            Pin = pin;
            Bsb = bsb;
            Balance = 0;
            Program.accounts.Add(this);
            File.AppendAllText(Program.logPath, $"\n{DateTime.Now}: New Account Created | name: {name} pin: {pin} bsb: {bsb}\n");
        }
    }
}