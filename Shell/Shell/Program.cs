using System;
using System.IO;

namespace Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Shell - Branislav Kljaic";
            Console.ForegroundColor = ConsoleColor.Yellow;
            // Pamcenje pocetnog direktorijuma, kada se izlogujemo iz user-a, da mozemo da se vratimo na njega.
            var mainDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("Welcome to Shell!");
            Console.ResetColor();
            // Konstruktor pravi pocetni direktorijum ako vec nije napravljen i fajl (tekstualnu datoteku users.txt) u koji dodaje admin usera.
            Commands command = new Commands();
            // Trazim unos prve komande koja je logovanje, izlazak iz aplikacije ili kreiranje novog user-a, sve ostale komande nisu prihvatljive ovdje.
            command.FirstCommand();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nType \"help\" if you need to check available commands, or \"help 'command'\" if you need help with a specific command." +
                "\nFor leaving your account type 'logout'.");
            Console.ResetColor();
            User user = command.GetUser();
            String choice;

            do
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("shell $" + user.GetUsername() + ": '" + user.GetUserPath() + "' > ");
                choice = Console.ReadLine();
                if (String.Compare(choice, "logout") != 0)
                    command.ExecuteCommand(choice);
                Console.ResetColor();
            }
            while (String.Compare(choice, "logout") != 0);

            Directory.SetCurrentDirectory(mainDirectory);
            
            Main(null);
        }
    }
}