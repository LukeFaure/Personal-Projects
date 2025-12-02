using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq.Expressions;
using System.Transactions;
using AccountData;
using AccountManagement;
using MoneyLogic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankingMain
{
    public static class Program
    {
        public static List<AccountRecords>? accounts;
        public static string logPath = "banklog.txt";
        public static string jsonPath = "accounts.json";
        public static bool running = true;
        public static int frame = 0;
        public static int menu = 0;
        public static string? jsonString;
        public static void Main(string[] args)
        {
            string json = File.ReadAllText(jsonPath);
            if (!string.IsNullOrWhiteSpace(json))
                accounts = JsonSerializer.Deserialize<List<AccountRecords>>(json);
            else
                accounts = new List<AccountRecords>();
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
                    else
                        Program.menu++;
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    Program.menu--;
                    Console.Clear();
                    if (Program.menu < 0)
                    {
                        File.WriteAllText(Program.jsonPath, JsonSerializer.Serialize(Program.accounts));
                        Environment.Exit(0);
                    }
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
        public static bool transferAccount = false;
        public static bool withdrawAccount = false;
        public static bool fetchNewAccountName = false;
        public static bool fetchNewAccountPin = false;
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
                    fetchNewAccountName = true;
                    string newAccountName = FetchAccountVariables.GetName();
                    fetchNewAccountName = false;
                    fetchNewAccountPin = true;
                    int newAccountPin = FetchAccountVariables.GetPin();
                    fetchNewAccountPin = false;
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

                    transferAccount = true;
                    string sendingAccountName = FetchAccountVariables.GetName();
                    transferAccount = false;
                    int sendingAccountPin = FetchAccountVariables.GetPin();
                    int sendingAccountBsb = FetchAccountVariables.GetBsb();
                    withdrawAccount = true;
                    string receivingAccountName = FetchAccountVariables.GetName();
                    withdrawAccount = false;
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
                if (ExecuteMethod.fetchNewAccountPin)
                    Console.WriteLine("Enter new 4 digit pin: ");
                else
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
                if (ExecuteMethod.transferAccount)
                    Console.WriteLine("Enter sending account name: ");
                else if (ExecuteMethod.withdrawAccount)
                    Console.WriteLine("Enter receiving account name: ");
                else if (ExecuteMethod.fetchNewAccountName)
                    Console.WriteLine("Enter new account name: ");
                else
                    Console.WriteLine("Enter account name: ");
        
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