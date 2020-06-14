using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

/*
 * Where je komanda koja prikazuje putanju do tekuceg direktorijuma u kojem se ulogovani korisnik nalazi. Ako se samo zada komanda where onda 
 * se ispisuje trenutna putanja, u svakom drugom slucaju se ispisaju da je komanda neregularna i nudi se pomoc za nju.
 */

namespace Shell
{
    public class Where
    {
        public void WhereCommand(string command, User user)
        {
            // Ako je command samo jedna rijec, to je i commandToExecute, ako ima vise rijeci onda je commandToExecute sve osim prve rijeci.
            string commandToExecute = command.IndexOf(" ") > -1 ? command.Substring(command.IndexOf(" ") + 1) : command;

            if (String.Compare(commandToExecute, "where") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(user.GetUserPath());
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nUnrecognizable use of command 'where'! If you need help with this command type 'help where'.\n");
                Console.ResetColor();
            }
        }
    }
}
