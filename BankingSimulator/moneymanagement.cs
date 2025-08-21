using System;
using System.Collections.Generic;
using System.IO;
using BankingMain;

namespace MoneyLogic
{
    public class MoneyMain
    {
        public void TransferMoney(string accountNameOne, int accountPinOne, int accountBsbOne, string accountNameTwo, int accountPinTwo, int accountBsbTwo, int moneyToTransfer)
        {
            int indexOfAccountOne = Program.accounts.FindIndex(x => x[0].Equals(accountNameOne) &&
                                                         x[1].Equals(accountPinOne) &&
                                                         x[2].Equals(accountBsbOne));
            int indexOfAccountTwo = Program.accounts.FindIndex(x => x[0].Equals(accountNameTwo) &&
                                                         x[1].Equals(accountPinTwo) &&
                                                         x[2].Equals(accountBsbTwo));
            if (indexOfAccountOne < 0 || indexOfAccountTwo < 0)
                throw new ArgumentException("Error 003; account names, pins or bsbs do not match, please try again");
            else
            {
                if (moneyToTransfer > (int)Program.accounts[indexOfAccountTwo][3])
                    throw new ArgumentException("Error 004; insufficient funds in transferor account");
                else
                {
                    Program.accounts[indexOfAccountOne][3] = (int)Program.accounts[indexOfAccountOne][3] + moneyToTransfer;
                    Program.accounts[indexOfAccountTwo][3] = (int)Program.accounts[indexOfAccountTwo][3] - moneyToTransfer;

                    File.AppendAllText(Program.logPath, $"{DateTime.Now}: {accountNameOne} recieved {moneyToTransfer} dollars from {accountNameTwo} \n");
                }
            }
        }
        public void WithdrawMoney(string accountName, int accountPin, int accountBsb, int moneyToWithdraw)
        {
            int index = Program.accounts.FindIndex(x => x[0].Equals(accountName) &&
                                                x[1].Equals(accountPin) &&
                                                x[2].Equals(accountBsb));
            if (index < 0)
                throw new ArgumentException("Error 005; account name, pin or bsb is incorrect, please try again");
            else
            {
                if (moneyToWithdraw > (int)Program.accounts[index][3])
                    throw new ArgumentException("Error 006; insufficient funds");
                else
                {
                    Program.accounts[index][3] = (int)Program.accounts[index][3] - moneyToWithdraw;
                    File.AppendAllText(Program.logPath, $"{DateTime.Now}: {accountName} withdrew {moneyToWithdraw} dollars \n");
                }
            }
        }
    }
}