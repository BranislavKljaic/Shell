using System;
using System.IO;

/*
 * Komanda Print ispisuje sadrzaj tekstualne datoteke u konzolu. Zadavanjem samo komande print izbacuje se poruka da treba da se navede fajl, 
 * zadavanjem komande print fajl, gdje je fajl ime tekstualnog fajla, ispisuje se sadrzaj istog na konzolu. Svako drugacije zadavanje komande 
 * predlaze pomoc pri istoj.
 */

namespace Shell
{
    public class Print
    {
        private readonly HelperMethods helperMethods = new HelperMethods();
        public void PrintCommand(string command, User user)
        {
            // Ako je command samo jedna rijec, to je i commandToExecute, ako ima vise rijeci onda je commandToExecute sve osim prve rijeci.
            string commandToExecute = command.IndexOf(" ") > -1 ? command.Substring(command.IndexOf(" ") + 1) : command;

            if(String.Compare(command, "print") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nYou must specify target to print! Use 'help print' if you need more help with this command.\n");
                Console.ResetColor();
            }
            else if(String.Compare(commandToExecute, "print") != 0 && helperMethods.CountWordsInString(commandToExecute) == 1)
            {
                string fileToPrint = commandToExecute.IndexOf(" ") > -1 ? commandToExecute.Substring(commandToExecute.IndexOf(" ") + 1) : commandToExecute;
                Directory.SetCurrentDirectory(user.GetFullUserPath());
                if (File.Exists(fileToPrint))
                {
                    String[] allLines = File.ReadAllLines(fileToPrint);
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var line in allLines) Console.WriteLine(line);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Text file with name you provided does not exist!");
                    Console.ResetColor();
                }
            }
            else if (helperMethods.CountWordsInString(commandToExecute) >= 2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You may only print one file in the console at the time!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nUnrecognizable use of command 'print'! If you need help with this command type 'help print'.\n");
                Console.ResetColor();
            }
        }
    }
}
