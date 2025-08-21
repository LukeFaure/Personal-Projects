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
        public void CreateAccount(string accountName, int accountPin)
        {
            try
            {
                var newAccount = new AccountData.AccountRecords(accountName, accountPin, rnd.Next(100000, 1000000));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void RemoveAccount(string accountName, int accountPin)
        {
            int index = Program.accounts.FindIndex(x => x[0].Equals(accountName) && x[0].Equals(accountPin));
            if (index > 0)
            {
                Program.accounts.RemoveAt(index);
                Program.accountNames.RemoveAt(index);
                Program.accountPins.RemoveAt(index);
                Program.accountBsbs.RemoveAt(index);
            }
            else
                throw new ArgumentException("Error 002; the account name or the account pin is inccorect, please try again");
        }   
        public void EditAccount(string accountName, int accountPin, string newAccountName, int newAccountPin)
        { 
            
        }        
    }
}