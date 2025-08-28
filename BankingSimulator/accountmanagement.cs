using System;
using System.Collections.Generic;
using System.IO;
using AccountData;
using BankingMain;

namespace AccountManagement
{
    public class AccountManaging
    {
        private static Random rnd = new Random();
        public static void CreateAccount(string accountName, int accountPin)
        {
            try
            {
                var newAccount = new AccountData.AccountRecords(accountName, accountPin, rnd.Next(100000, 1000000));
                Console.WriteLine(newAccount);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void RemoveAccount(string accountName, int accountPin)
        {
            var account = Program.accounts.Find(x => x.Name == accountName && x.Pin == accountPin);
            if (account != null)
            {
                Program.accounts.Remove(account);
                File.AppendAllText(Program.logPath, $"\n{DateTime.Now}: account deleted | name: {accountName} pin: {accountPin}\n");
            }
            else
                throw new ArgumentException("Error 011; the account name or the account pin is incorrect, please try again");
        }
        public static void EditAccount(string accountName, int accountPin, string newAccountName, int newAccountPin)
        {
            var account = Program.accounts.Find(a => a.Name == accountName && a.Pin == accountPin);
            if (account == null)
                throw new ArgumentException("Error 011; the account name or the account pin is incorrect, please try again");
            int index = Program.accounts.FindIndex(x => x.Name == accountName && x.Pin == accountPin);
            if (index < 0)
                throw new ArgumentException("Error 011; the account name or the account pin is incorrect, please try again");
            if (Program.accounts.Where((a, i) => i != index).Any(a => a.Name == newAccountName || a.Pin == newAccountPin))
                throw new ArgumentException("Error 010; the account name or pin already exists, please try again");
            account.Name = newAccountName;
            account.Pin = newAccountPin;
            File.AppendAllText(Program.logPath, $"\n{DateTime.Now} account edited | name: {accountName} pin: {accountPin} new name: {newAccountName} new pin: {newAccountPin}\n");
        }
        public static void ViewAccount(string accountName, int accountPin)
        {
            var account = Program.accounts.Find(a => a.Name == accountName && a.Pin == accountPin);
            if (account == null)
                throw new ArgumentException("Error 011; the account name or the account pin is incorrect");
            Console.WriteLine(account);
        }    
    }
}