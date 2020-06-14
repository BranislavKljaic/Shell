using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
 * Go komanda sluzi za promjenu tekuceg direktorijuma. Ukoliko zelimo da promijenimo direktorijum, pored kljucne rijeci go kucamo putanju koju zelimo 
 * da dostignemo (na primjer \root\admin\home\dir1\).
 */

namespace Shell
{
    public class Go
    {
        private readonly HelperMethods helperMethods = new HelperMethods();
        public void GoCommand(string command, User user)
        {
            // Ako je command samo jedna rijec, to je i commandToExecute, ako ima vise rijeci onda je commandToExecute sve osim prve rijeci.
            string commandToExecute = command.IndexOf(" ") > -1 ? command.Substring(command.IndexOf(" ") + 1) : command;

            if (String.Compare(command, "go") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nYou need to specify the path you want to go!\n");
                Console.ResetColor();
            }
            else if (helperMethods.CountWordsInString(commandToExecute) == 1)
            {
                string pathToGo = @"C:\" + commandToExecute;
                if (Directory.Exists(pathToGo) && pathToGo.EndsWith('\\'))
                {
                    Directory.SetCurrentDirectory(pathToGo);
                    user.SetUserPath(commandToExecute);
                    user.SetFullUserPath(pathToGo);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nCurrent path does not exists!\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nUnrecognizable use of command 'go'! If you need help with this command type 'help go'.\n");
                Console.ResetColor();
            }
        }
    }

}
