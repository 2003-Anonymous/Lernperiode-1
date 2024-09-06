using System;
using System.Reflection.PortableExecutable;
using Microsoft.Win32;

namespace Anmeldefeld_LP1
{
    internal class Program
    {

        static void Main(string[] args)
        {

            Console.ResetColor();

            string registryPath = @"Software\Anmeldefeld_LP1";
            string keyName = "Code";
            int defaultCode = 77;


            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true) ??
                              Registry.CurrentUser.CreateSubKey(registryPath);

            if (key == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error");
                return;
            }

            object code2 = key.GetValue(keyName);
            int savedCode = code2 != null ? Convert.ToInt32(code2) : defaultCode;

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            if (username == "changecode")
            {
                Console.Write("Enter old code: ");
                if (int.TryParse(Console.ReadLine(), out int code) && code == savedCode)
                {
                    Console.Write("Enter new code: ");
                    if (int.TryParse(Console.ReadLine(), out int newCode))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        key.SetValue(keyName, newCode);
                        Console.WriteLine("Code changed successfully");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid new code");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid old code");
                }
            }
            else if (username == "Joshua")
            {
                try
                {
                    Console.Write("Enter Code: ");
                    if (int.TryParse(Console.ReadLine(), out int code) && code == savedCode)
                    {
                        if (File.Exists("Verknüpfung.txt"))
                        {
                            string content = File.ReadAllText("Verknüpfung.txt");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Code correct\n{content}");
                        }

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Code incorrect");
                    }
                }
                catch
                {
                    Console.WriteLine("Error");
                }

            }
            else if (username == "showCode")
            {
                Console.Write($"Your code is {savedCode}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Acess denied");
            }

            key.Close();
        }
    }
}



