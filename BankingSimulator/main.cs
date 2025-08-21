using System;
using System.Collections.Generic;
using System.IO;
using AccountData;

namespace BankingMain
{
    public static class Program
    {
        public static List<string> accountNames = new List<string>();
        public static List<int> accountPins = new List<int>();
        public static List<int> accountBsbs = new List<int>();
        public static List<object[]> accounts = new List<object[]>();
        public static string logPath = "banklog.txt";
        public static bool running = true;
        public static int frame = 0;
        public static int menu = 0;
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Faure Bank\n");
            Console.WriteLine("[manage accounts]\ntransfer funds\nwithdraw funds\n");
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
                    Program.menu++;
                    Console.Clear();
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    Program.menu--;
                    Console.Clear();
                }
                switch (Program.frame)
                    {
                        case 0:
                            if (Program.menu == 0)
                            {
                                Console.WriteLine("\n[manage accounts]\ntransfer funds\nwithdraw funds\n");
                            }
                            else if (Program.menu == 1)
                            {
                                Console.WriteLine("\n[create account]\nremove account\nedit account\n");
                            }
                            break;
                        case 1:
                            if (Program.menu == 0)
                            {
                                Console.WriteLine("\nmanage accounts\n[transfer funds]\nwithdraw funds\n");
                            }
                            else if (Program.menu == 1)
                            {
                                Console.WriteLine("\ncreate account\n[remove account]\nedit account\n");
                            }
                            break;
                        case 2:
                            if (Program.menu == 0)
                            {
                                Console.WriteLine("\nmanage accounts\ntransfer funds\n[withdraw funds]\n");
                            }
                            else if (Program.menu == 1)
                            {
                                Console.WriteLine("\ncreate account\nremove account\n[edit account]\n");
                            }
                            break;
                        default:
                            Program.frame = 0;
                            if (Program.menu == 0)
                            {
                                Console.WriteLine("\n[manage accounts]\ntransfer funds\nwithdraw funds\n");
                            }
                            else if (Program.menu == 1)
                            {
                                Console.WriteLine("\n[create account]\nremove account\nedit account\n");
                            }
                            break;
                    }
            }
        }
    }
}