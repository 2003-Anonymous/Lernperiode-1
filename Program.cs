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

            string filePath = @"C:\Users\joshu\source\repos\Lernatelier\Alternative\Daten_Verknüpfung-Alternative.txt";
            string filePath2 = @"C:\Users\joshu\source\repos\Lernatelier\Alternative\Verknüpfung2.txt";

            int defaultCode = 77;

            int savedCode = defaultCode;
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

            if (username == "changecode")
            {
                Console.Write("Enter old code: ");
                if (int.TryParse(Console.ReadLine(), out int code) && code == savedCode)
                {
                    Console.Write("Enter new code: ");
                    if (int.TryParse(Console.ReadLine(), out int newCode))
                    {
                        try
                        {

                            File.WriteAllText(filePath, newCode.ToString());
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Code changed successfully");
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
            else if (username == "Joshua")
            {
                Console.Write("Enter Code: ");
                if (int.TryParse(Console.ReadLine(), out int code) && code == savedCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Code correct\n{File.ReadAllText(filePath2)}");

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
                Console.WriteLine("Access denied");
            }
        }
    }
}

/*using System;
using System.IO;

namespace Anmeldefeld_LP1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ResetColor();

            // Datei-Pfad definieren
            string filePath = @"C:\Users\joshu\source\repos\Lernatelier\Alternative\Daten_Verknüpfung-Alternative.txt";
            int defaultCode = 77; // Standard-Code

            // Gespeicherten Code initialisieren
            int savedCode = defaultCode;

            // Überprüfen, ob die Datei existiert und die erste Zeile einlesen
            if (File.Exists(filePath))
            {
                try
                {
                    // using stellt sicher, dass der Stream nach dem Lesen freigegeben wird
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string firstLine = reader.ReadLine()?.Trim(); // Nur die erste Zeile einlesen
                        if (!string.IsNullOrEmpty(firstLine))
                        {
                            savedCode = Convert.ToInt32(firstLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Fehler beim Lesen des Codes: {ex.Message}");
                    return;
                }
            }

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
                        try
                        {
                            // using stellt sicher, dass der Stream nach dem Schreiben geschlossen wird
                            using (StreamWriter writer = new StreamWriter(filePath, false)) // false: Datei wird überschrieben
                            {
                                writer.WriteLine(newCode.ToString());
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Code changed successfully");

                            // Debug-Ausgabe zur Bestätigung des neuen Codes
                            string testRead = File.ReadLines(filePath).FirstOrDefault()?.Trim();
                            if (testRead == newCode.ToString())
                            {
                                Console.WriteLine("Der neue Code wurde korrekt gespeichert.");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Fehler beim Überprüfen des neuen Codes.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
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
            else if (username == "Joshua")
            {
                Console.Write("Enter Code: ");
                if (int.TryParse(Console.ReadLine(), out int code) && code == savedCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Code correct\n{savedCode}");
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
                Console.WriteLine("Access denied");
            }
        }
    }
}*/
