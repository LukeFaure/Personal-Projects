using System;
using System.Linq;
using BankingMain;
namespace AccountData
{
    public class AccountRecords
    {
        public override string ToString()
        {
            return $"Name: {Name}, Pin: {Pin}, BSB: {Bsb}";
        }

        public string Name { get; set; }
        public int Pin { get; set;  }
        public int Bsb { get; set; }
        public AccountRecords(string name, int pin, int bsb)
        {
            if (Program.accounts.Any(a => a[0].Equals(name) || a[1].Equals(pin) || a[2].Equals(bsb)))
                throw new ArgumentException("Error 001; one or more of the values already exist, please try again");

            Name = name;
            Pin = pin;
            Bsb = bsb;

            Program.accountNames.Add(name);
            Program.accountPins.Add(pin);
            Program.accountBsbs.Add(bsb);
            Program.accounts.Add(new object[] { name, pin, bsb });
        }
    } 
}