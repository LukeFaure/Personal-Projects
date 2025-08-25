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
        public static void RemoveAccount(string? accountName, int? accountPin)
        {
            int index = Program.accounts.FindIndex(x => x[0].Equals(accountName) && x[1].Equals(accountPin));
            if (index >= 0)
            {
                Program.accounts.RemoveAt(index);
                Program.accountNames.RemoveAt(index);
                Program.accountPins.RemoveAt(index);
                Program.accountBsbs.RemoveAt(index);
                File.AppendAllText(Program.logPath, $"\n{DateTime.Now}: account deleted | name: {accountName} pin: {accountPin}\n");
            }
            else
                throw new ArgumentException("Error 002; the account name or the account pin is incorrect, please try again");
        }
        public static void EditAccount(string? accountName, int? accountPin, string newAccountName, int newAccountPin)
        {
            int index = Program.accounts.FindIndex(x => x[0].Equals(accountName) && x[1].Equals(accountPin));
            if (index < 0)
                throw new ArgumentException("Error 002; the account name or the account pin is incorrect, please try again");
            if (Program.accounts.Any(a =>
                                        a[0].Equals(newAccountName) ||
                                        a[1].Equals(newAccountPin)))
                throw new ArgumentException("Error 008; the account name or pin already exists, please try again");
            Program.accounts[index][0] = newAccountName;
            Program.accounts[index][1] = newAccountPin;
            File.AppendAllText(Program.logPath, $"\n{DateTime.Now} account edited | name: {accountName} pin: {accountPin} new name: {newAccountName} new pin: {newAccountPin}\n");
        }        
    }
}