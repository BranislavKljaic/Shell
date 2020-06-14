using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
 * Find komanda sluzi za pretrazivanje tekstualnih datoteka, da bi se pronasla rijec/string koja se potencijalno nalazi u toj tekstualnoj datoteci. 
 * U slucaju da se proslijedjena rijec/string pronadje u zadatoj tekstualnoj datoteci, vratice se broj reda u kojem se ona nalazi, u suprotnom se 
 * nece nista ispisati i vracamo se na mogucnost izvrsavanja bilo koje komande.
 */

namespace Shell
{
    public class Find
    {
        private readonly HelperMethods helperMethods = new HelperMethods();
        public void FindCommand(string command)
        {
            // Ako je command samo jedna rijec, to je i commandToExecute, ako ima vise rijeci onda je commandToExecute sve osim prve rijeci.
            string commandToExecute = command.IndexOf(" ") > -1 ? command.Substring(command.IndexOf(" ") + 1) : command;

            if (String.Compare(command, "find") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nUnrecognizable use of 'find' command. Type 'help find' if you need instructions how to use this command!\n");
                Console.ResetColor();
                return;
            }
            string firstWordInCommand = command.Substring(0, command.IndexOf(" "));
            if (String.Compare(firstWordInCommand, "find") == 0 && helperMethods.CountWordsInString(command) > 2)
            {                
                string fileName = command;
                string searchingString = commandToExecute;
                for (int i = 0; i < helperMethods.CountWordsInString(command) - 1; i++) 
                {
                    fileName = fileName.IndexOf(" ") > -1 ? fileName.Substring(fileName.IndexOf(" ") + 1) : fileName;
                }
                string stringToFind = searchingString.Substring(0, searchingString.IndexOf(fileName));
                stringToFind = stringToFind.Remove(stringToFind.Length - 1);

                if (File.Exists(fileName))
                {
                    String[] allLines = File.ReadAllLines(fileName);

                    for (int i = 0; i < allLines.Length; i++)
                    {
                        if (String.Compare(allLines[i], stringToFind) == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\t" + (i + 1));
                            Console.ResetColor();
                        }
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nUnrecognizable use of 'find' command. Type 'help find' if you need instructions how to use this command!\n");
                Console.ResetColor();
            }
        }
    }
}
