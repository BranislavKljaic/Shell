using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
 * FindDat komanda sluzi za pronalazenje datoteka ili direktorijuma u stablu direktorijuma. Komanda prolazi ako se string unesen u konzolu ne sastoji 
 * od vise od tri rijeci, prva rijec konzole, druga fajl/folder koji trazimo i treca je putanja od koje zelimo da pocnemo sa pretragom. U slucaju 
 * da se fajl/folder nadje pod tim stablom ispisace se puni path do trazenog fajla/foldera, dok u suprotnom se nece nista ispisati.
 */

namespace Shell
{
    public class FindDat
    {
        public void FindDatCommand(string command)
        {
            HelperMethods helperMethods = new HelperMethods();

            // Ako je command samo jedna rijec, to je i commandToExecute, ako ima vise rijeci onda je commandToExecute sve osim prve rijeci.
            string commandToExecute = command.IndexOf(" ") > -1 ? command.Substring(command.IndexOf(" ") + 1) : command;

            if (String.Compare(command, "findDat") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nYou must speficy name of the file and path where you want to search for the file!\n");
                Console.ResetColor();
            }
            else if (helperMethods.CountWordsInString(command) > 3)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nUnrecognizable use of 'findDat' command. If you need help with using it type 'help findDat'!\n");
                Console.ResetColor();
            }
            else if (helperMethods.CountWordsInString(command) == 3)
            {
                string fileToSearch = commandToExecute.Substring(0, commandToExecute.IndexOf(" "));
                string pathToSearch = commandToExecute.IndexOf(" ") > -1 ? commandToExecute.Substring(commandToExecute.IndexOf(" ") + 1) : commandToExecute;
                if (pathToSearch.StartsWith('\\'))
                {
                    string mainPath = @"C:" + pathToSearch;
                    string[] filePaths = Directory.GetFiles(mainPath, "*", SearchOption.AllDirectories);
                    foreach (var file in filePaths)
                    {
                        if (String.Compare(fileToSearch, Path.GetFileName(file)) == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Path.GetFullPath(Path.GetFileName(file)));
                            Console.ResetColor();
                        }
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nUnrecognizable use of 'findDat' command. If you need help with using it type 'help findDat'!\n");
                Console.ResetColor();
            }
        }
    }
}
