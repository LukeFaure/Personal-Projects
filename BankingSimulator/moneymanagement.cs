using System;
using System.Collections.Generic;
using System.IO;
using BankingMain;

namespace MoneyLogic
{
    public class MoneyMain
    {
        public static void TransferMoney(string accountNameOne, int accountPinOne, int accountBsbOne, string accountNameTwo, int accountPinTwo, int accountBsbTwo, int moneyToTransfer)
        {
            var sender = Program.accounts.Find(x => x.Name == accountNameOne && x.Pin == accountPinOne && x.Bsb == accountBsbOne);
            var reciever = Program.accounts.Find(x => x.Name == accountNameTwo && x.Pin == accountPinTwo && x.Bsb == accountBsbTwo);

            if (sender == null || reciever == null)
                throw new ArgumentException("Error 013; account not found, name or pin or bsb could be incorrect");
            else
            {
                if (moneyToTransfer > sender.Balance)
                    throw new ArgumentException("Error 014; insufficient funds");
                else
                {
                    sender.Balance -= moneyToTransfer;
                    reciever.Balance += moneyToTransfer;

                    File.AppendAllText(Program.logPath, $"\n{DateTime.Now}: {accountNameTwo} recieved {moneyToTransfer} dollars from {accountNameOne}\n");
                }
            }
        }
        public static void WithdrawMoney(string accountName, int accountPin, int accountBsb, int moneyToWithdraw)
        {
            var account = Program.accounts.Find(x => x.Name == accountName && x.Pin == accountPin && x.Bsb == accountBsb);
            if (account == null)
                throw new ArgumentException("Error 013; account name, pin or bsb is incorrect, please try again");
            else
            {
                if (moneyToWithdraw > account.Balance)
                    throw new ArgumentException("Error 014; insufficient funds");
                else
                {
                    account.Balance -= moneyToWithdraw;
                    File.AppendAllText(Program.logPath, $"\n{DateTime.Now}: {accountName} withdrew {moneyToWithdraw} dollars\n");
                }
            }
        }
        public static void DepositMoney(string accountName, int accountPin, int accountBsb, int depositAmount)
        {
            var account = Program.accounts.Find(x => x.Name == accountName && x.Pin == accountPin && x.Bsb == accountBsb);
            if (account == null)
                throw new ArgumentException("Error 013; account name, pin or bsb is incorrect, please try again");
            else
            {
                account.Balance += depositAmount;
                File.AppendAllText(Program.logPath, $"\n{DateTime.Now}: {depositAmount} dollars was deposited into {accountName} account\n");
            }
        }
    }
}