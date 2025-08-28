using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq.Expressions;
using System.Transactions;
using AccountData;
using AccountManagement;
using MoneyLogic;

namespace BankingMain
{
    public static class Program
    {
        public static List<AccountRecords> accounts = new List<AccountRecords>();
        public static string logPath = "banklog.txt";
        public static bool running = true;
        public static int frame = 0;
        public static int menu = 0;
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Faure Bank\n");
            Console.WriteLine("[manage accounts]\ntransfer funds\nwithdraw funds\ndeposit funds\n");
            while (running)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey(true);
                try
                {
                    new UI(keyPress);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    File.AppendAllText(logPath, $"\n{DateTime.Now}: {ex.Message}");
                }
            }
        }
        public static void ViewAccount(string accountName, int accountPin)
        {

        }
    }
    public class UI
    {
        public ConsoleKeyInfo PressedKey { get; set; }
        public UI(ConsoleKeyInfo pressedKey)
        {
            if (pressedKey.Key != ConsoleKey.UpArrow && pressedKey.Key != ConsoleKey.DownArrow && pressedKey.Key != ConsoleKey.Enter && pressedKey.Key != ConsoleKey.Escape)
                throw new ArgumentException("Error 007; invalid key press");
            else
            {
                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    Program.frame--;
                    Console.Clear();
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    Program.frame++;
                    Console.Clear();
                }
                else if (pressedKey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    if (Program.frame != 0 || (Program.frame == 0 && Program.menu == 1))
                    {
                        try
                        {
                            ExecuteMethod.ExecuteSelected();
                            Console.WriteLine("\nPress any key to continue... ");
                            Console.ReadKey(true);
                            Console.Clear();
                            Program.menu = 0;
                            Program.frame = 0;
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Program.menu++;
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    Program.menu--;
                    Console.Clear();
                    if (Program.menu < 0)
                        Environment.Exit(0);
                }
                switch (Program.frame)
                {
                    case 0:
                        if (Program.menu == 0)

                            Console.WriteLine("\n[manage accounts]\ntransfer funds\nwithdraw funds\ndeposit funds\n");

                        else if (Program.menu == 1)

                            Console.WriteLine("\n[create account]\nremove account\nedit account\nview account\n");

                        break;
                    case 1:
                        if (Program.menu == 0)

                            Console.WriteLine("\nmanage accounts\n[transfer funds]\nwithdraw funds\ndeposit funds\n");

                        else if (Program.menu == 1)

                            Console.WriteLine("\ncreate account\n[remove account]\nedit account\nview account\n");

                        break;
                    case 2:
                        if (Program.menu == 0)

                            Console.WriteLine("\nmanage accounts\ntransfer funds\n[withdraw funds]\ndeposit funds\n");

                        else if (Program.menu == 1)

                            Console.WriteLine("\ncreate account\nremove account\n[edit account]\nview account\n");

                        break;
                    case 3:
                        if (Program.menu == 0)
                            Console.WriteLine("\nmanage accounts\ntransfer funds\nwithdraw funds\n[deposit funds]\n");

                        else if (Program.menu == 1)
                            Console.WriteLine("\ncreate account\nremove account\nedit account\n[view account]\n");
                        break;
                    default:
                        Program.frame = 0;
                        if (Program.menu == 0)

                            Console.WriteLine("\n[manage accounts]\ntransfer funds\nwithdraw funds\ndeposit funds\n");

                        else if (Program.menu == 1)

                            Console.WriteLine("\n[create account]\nremove account\nedit account\nview account\n");

                        break;
                }
            }
        }
    }
    public static class ExecuteMethod
    {
        public static void ExecuteSelected()
        {
            if (Program.menu == 1)
            {
                if (Program.frame == 0)
                {
                    string accountName = FetchAccountVariables.GetName();
                    int accountPin = FetchAccountVariables.GetPin();
                    AccountManaging.CreateAccount(accountName, accountPin);
                }
                else if (Program.frame == 1)
                {
                    string accountName = FetchAccountVariables.GetName();
                    int accountPin = FetchAccountVariables.GetPin();
                    AccountManaging.RemoveAccount(accountName, accountPin);
                }
                else if (Program.frame == 2)
                {
                    string accountName = FetchAccountVariables.GetName();
                    int accountPin = FetchAccountVariables.GetPin();
                    string newAccountName = FetchAccountVariables.GetName();
                    int newAccountPin = FetchAccountVariables.GetPin();
                    AccountManaging.EditAccount(accountName, accountPin, newAccountName, newAccountPin);
                }
                else if (Program.frame == 3)
                {
                    string accountName = FetchAccountVariables.GetName();
                    int accountPin = FetchAccountVariables.GetPin();
                    AccountManaging.ViewAccount(accountName, accountPin);
                }
            }
            else if (Program.menu == 0)
            {
                if (Program.frame == 1)
                {
                    string sendingAccountName = FetchAccountVariables.GetName();
                    int sendingAccountPin = FetchAccountVariables.GetPin();
                    int sendingAccountBsb = FetchAccountVariables.GetBsb();
                    string receivingAccountName = FetchAccountVariables.GetName();
                    int receivingAccountPin = FetchAccountVariables.GetPin();
                    int receivingAccountBsb = FetchAccountVariables.GetBsb();
                    Console.WriteLine("Enter amount to transfer: ");
                    int amount = FetchAccountVariables.CheckMoney();
                    MoneyMain.TransferMoney(sendingAccountName, sendingAccountPin, sendingAccountBsb, receivingAccountName, receivingAccountPin, receivingAccountBsb, amount);
                }
                else if (Program.frame == 2)
                {
                    string accountName = FetchAccountVariables.GetName();
                    int accountPin = FetchAccountVariables.GetPin();
                    int accountBsb = FetchAccountVariables.GetBsb();
                    Console.WriteLine("Enter amount to withdraw: ");
                    int amount = FetchAccountVariables.CheckMoney();
                    MoneyMain.WithdrawMoney(accountName, accountPin, accountBsb, amount);
                }
                else if (Program.frame == 3)
                {
                    string accountName = FetchAccountVariables.GetName();
                    int accountPin = FetchAccountVariables.GetPin();
                    int accountBsb = FetchAccountVariables.GetBsb();
                    Console.WriteLine("Enter amount to deposit: ");
                    int amount = FetchAccountVariables.CheckMoney();
                    MoneyMain.DepositMoney(accountName, accountPin, accountBsb, amount);
                }
            }
        }
    }
    public class FetchAccountVariables
    {
        public static int GetPin()
        {
            while (true)
            {
                Console.WriteLine("Enter a 4 digit pin: ");
                string? userPin = Console.ReadLine();
                if (userPin != null)
                {
                    if (userPin.Length == 4)
                    {
                        int accountPin;
                        if (int.TryParse(userPin, out accountPin))
                        {
                            return accountPin;
                        }
                        else
                        {
                            Console.WriteLine("Error 001; pin must be numerical digits");
                            Console.WriteLine("press any key to continue...");
                            Console.ReadKey(true);
                            Console.Clear();
                            continue;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Error 002; pin must be 4 digits");
                        Console.WriteLine("\npress any key to continue...");
                        Console.ReadKey(true);
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Error 003; pin cannot be null");
                    Console.WriteLine("\npress any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
            }
        }
        public static int GetBsb()
        {
            while (true)
            {
                Console.WriteLine("Enter account BSB: ");
                string? userBsb = Console.ReadLine();
                if (userBsb != null)
                    if (userBsb.Length == 6)
                    {
                        int accountBsb;
                        if (int.TryParse(userBsb, out accountBsb))
                            return accountBsb;
                        else
                        {
                            Console.WriteLine("Error 004; bsb must be numerical digits");
                            Console.WriteLine("\npress any key to continue...");
                            Console.ReadKey(true);
                            Console.Clear();
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error 005; bsb must be 6 digits");
                        Console.WriteLine("\npress any key to continue...");
                        Console.ReadKey(true);
                        Console.Clear();
                        continue;
                    }
                else
                {
                    Console.WriteLine("Error 006; bsb cannot be null");
                    Console.WriteLine("\npress any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
            }
        }
        public static string GetName()
        {
            while (true)
            {
                Console.WriteLine("Enter accout name: ");
                string? accountName = Console.ReadLine();
                if (accountName != null)
                    return accountName;
                else
                {
                    Console.WriteLine("Error 007; name cannot be null");
                    Console.WriteLine("\npress any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
            }
        }
        public static int CheckMoney()
        {
            while (true)
            {
                string? money = Console.ReadLine();
                if (money != null)
                {
                    int amount;
                    bool isInt = int.TryParse(money, out amount);
                    if (isInt)
                        return amount;
                    else
                    {
                        Console.WriteLine("Error 008; amount must be numerical digits");
                        Console.WriteLine("\npress any key to continue...");
                        Console.ReadKey(true);
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Error 009; amount cannot be null");
                    Console.WriteLine("\npress any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
            }
        }
    }
}