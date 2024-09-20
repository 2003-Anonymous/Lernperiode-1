using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text;
using Microsoft.Win32;

namespace Anmeldefeld_LP1
{
    internal class Program
    {

        static void Main(string[] args)
        {

            Console.ResetColor();

            string filePath = @"C:\Users\joshu\source\repos\Lernatelier\Alternative\Password.txt";
            string filePath2 = @"C:\Users\joshu\source\repos\Lernatelier\Alternative\Inhalt.txt";
            string filePath3 = @"C:\Users\joshu\source\repos\Lernatelier\Alternative\Benutzername.txt";

            string defaultUser = "Joshua";

            string savedUser = defaultUser;

            int defaultCode = 77;

            int savedCode = defaultCode;

            string user = "";

            if (File.Exists(filePath))
            {
                try
                {
                    string codeFromFile = File.ReadAllText(filePath).Trim();

                    if (!string.IsNullOrEmpty(codeFromFile))
                    {
                        savedCode = Convert.ToInt32(codeFromFile);
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error");
                    return;
                }
            }

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            if (File.Exists(filePath3))
            {
                try
                {
                    string userFromFile = File.ReadAllText(filePath3).Trim();

                    if (!string.IsNullOrEmpty(userFromFile))
                    {
                        savedUser = userFromFile;
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error");
                    return;
                }
            }
            if (username == "changecode")
            {
                Console.Write("Enter old code: ");
                string oldPassword = ReadPassword();

                if (int.TryParse(oldPassword, out int code) && code == savedCode)
                {
                    Console.Write("\nEnter new code: ");
                    string newPassword = ReadPassword();

                    if (newPassword == oldPassword)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nAre you stupid?");

                        return;
                    }

                    if (int.TryParse(newPassword, out int newCode))
                    {
                        try
                        {

                            File.WriteAllText(filePath, newCode.ToString());
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n\nCode changed successfully");
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error");
                        }
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
            else if (username == "changeuser")
            {
                Console.Write("Enter old Username: ");
                string oldUser = Console.ReadLine();
                
                if (oldUser == savedUser)
                {
                    Console.Write("\nEnter new username: ");
                    string newUser = Console.ReadLine();

                   
                    if (newUser == oldUser)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nAre you stupid? You entered the same username.");
                        return;
                    }

                    
                    try
                    {
                        File.WriteAllText(filePath3, newUser); 
                        savedUser = newUser; 
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\nUsername changed successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error saving the new username: {ex.Message}");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid old username");
                }
            }
            else if (username == savedUser)
            {
                Console.Write("Enter Code: ");
                string password = ReadPassword();

                if (int.TryParse(password, out int code) && code == savedCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\nCode correct\n\n{File.ReadAllText(filePath2)}");

                    StringBuilder inputText = new StringBuilder();

                    string line;
                    while ((line = Console.ReadLine()) != "END")  // 'END' beendet die Eingabe
                    {
                        inputText.AppendLine(line);
                    }

                    File.AppendAllText(filePath2, inputText.ToString() + Environment.NewLine);
                    Console.WriteLine("Eintrag wurde hinzugefügt.");
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Code incorrect");
                }

            }
            else if (username == "showCode")
            {
                Console.Write($"Your code is {savedCode}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAccess denied");
            }
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            return password;
        }
    }
}