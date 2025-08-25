using System;
using System.Linq;
using BankingMain;
using System.IO;
namespace AccountData
{
    public class AccountRecords
    {
        public override string ToString()
        {
            return $"Name: {Name}, Pin: {Pin}, BSB: {Bsb} Balance: ${Balance}";
        }
        public string Name { get; set; }
        public int Pin { get; set; }
        public int Bsb { get; set; }
        public int Balance { get; set; }
        public AccountRecords(string name, int pin, int bsb)
        {
            if (Program.accounts.Any(a => a[0].Equals(name) || a[1].Equals(pin) || a[2].Equals(bsb)) || Program.accounts.Any(a => a[0].Equals(name) && a[1].Equals(pin)))
                throw new ArgumentException("Error 001; one or more of the values already exist, please try again");

            Name = name;
            Pin = pin;
            Bsb = bsb;
            int balance = 0;
            Balance = balance;

            Program.accountNames.Add(name);
            Program.accountPins.Add(pin);
            Program.accountBsbs.Add(bsb);
            Program.accounts.Add([name, pin, bsb, balance]);
            File.AppendAllText(Program.logPath, $"{DateTime.Now}: New Account Created | name: {name} pin: {pin} bsb: {bsb}");
        }
    }
}