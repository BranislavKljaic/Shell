using System;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.ComponentModel.Design.Serialization;
using System.Reflection.Emit;

/*
 * Klasa iz koje se pozivaju sve komande. Kada se napravi novi objekat, pozivanjem konstruktora (bez parametara) pravi se root folder u koji se odmah 
 * smijesta jedan uder (admin user, admin sifra) i tekstualni fajl koji sadrzi sve korisnike (tekstualni fajl je hidden). Pri pokretanju programa 
 * mogu se koristiti login, create user i exit komande. Pri create user pravi se korisnikov folder i u njemu home folder, i stavlja se njegova sifra 
 * i korisnicko ime u tekstualni fajl, koji se pri logovanju pretrazuje radi verifikacije validnosti korisnickog imena i lozinke.
 * Exit komanda terminira program nakon 1,5 sekundu. ExecuteCommand izvrsava sve druge komande nakon logovanja korisnika. ReadPassword sluzi za pisanje 
 * znaka * umjesto karaktera, pri unosenju sifre pri prijavljivanju i kreiranju novog usera.
 */

namespace Shell
{
    class Commands
    {
        private static readonly string computerUser = Environment.UserName;
        private readonly string shellMainFolder = @"C:\root\";
        private readonly string usersFile = @"C:\root\users.txt";
        private User activeUser;

        public void SetUser(User user)
        {
            activeUser = user;
        }

        public User GetUser()
        {
            return activeUser;
        }

        public Commands()
        {
            if (!Directory.Exists(shellMainFolder))
            {
                Console.WriteLine("Creating initial directory and admin user ...");
                Directory.CreateDirectory(shellMainFolder);
                if (!File.Exists(usersFile))
                {
                    // Kreiranje fajla i pisanje u njega.
                    using StreamWriter sw = File.CreateText(usersFile);
                    sw.WriteLine("admin:admin");
                    sw.Close();
                }
                File.SetAttributes(usersFile, FileAttributes.Hidden);
                Directory.CreateDirectory(shellMainFolder + @"\admin\home\");
            }
            else
            {
                Directory.SetCurrentDirectory(shellMainFolder);
            }
        }

        public void FirstCommand()
        {
            Console.WriteLine("Available commands at this moment are \"login\", \"exit\" and \"create user\".");
            String choice;

            do
            {
                Console.Write("shell> ");
                choice = Console.ReadLine();
            }
            while (String.Compare(choice, "login") != 0 && String.Compare(choice, "exit") != 0 && String.Compare(choice, "create user") != 0);

            if (String.Compare(choice, "login") == 0)
            {
                String username;
                String password;
                do
                {
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    Console.Write("Password: ");
                    password = ReadPassword();
                    if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password) || username.Contains(" ") || password.Contains(" "))
                        Console.WriteLine("Username and password must contain regular alphanumeric signs and without spaces!");
                }
                while (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password) || username.Contains(" ") || password.Contains(" "));

                string newUser = username + ":" + password;

                // Citanje svih linija iz fajla sa user-ima i ubacivanje u niz stringova.
                if (File.Exists(usersFile))
                {
                    String[] usersFileLines = File.ReadAllLines(usersFile);
                    String[] usersList = new string[usersFileLines.Length];
                    activeUser = new User();
                    for (int i = 0; i < usersFileLines.Length; i++)
                    {
                        usersList[i] = usersFileLines[i].IndexOf(":") > -1 ? usersFileLines[i].Substring(0, usersFileLines[i].IndexOf(":")) : usersFileLines[i];
                        if (String.Compare(usersList[i], username) == 0)
                        {
                            if (String.Compare(usersFileLines[i], newUser) == 0)
                            {
                                Console.WriteLine("You loged in successfully! Congratulation!\n");
                                activeUser.SetUsername(username);
                                Directory.SetCurrentDirectory(@"C:\root\" + username + @"\home\");
                                activeUser.SetFullUserPath(@"C:\root\" + username + @"\home\");
                                string currentUserPath = Directory.GetCurrentDirectory();
                                if (currentUserPath.Contains("\\root\\" + username + "\\home")) currentUserPath = "\\root\\" + activeUser.GetUsername() + "\\home\\";
                                activeUser.SetUserPath(currentUserPath);
                            }
                            else
                            {
                                Console.WriteLine("Sorry, username and password do not match!\n");
                                FirstCommand();
                            }
                            return;
                        }
                    }
                    Console.WriteLine("User with '" + username + "' username does not exist. Please login with valid username or create new user!\n");
                    FirstCommand();
                }
            }
            else if (String.Compare(choice, "create user") == 0)
            {
                Console.WriteLine("Creating new user ...");
                String username;
                String password;
                do
                {
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    Console.Write("Password: ");
                    password = ReadPassword();
                    if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password) || username.Contains(" ") || password.Contains(" "))
                        Console.WriteLine("Username and password must contain regular alphanumeric signs and without spaces!");
                }
                while (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password) || username.Contains(" ") || password.Contains(" "));

                string newUser = username + ":" + password;

                // Citanje svih linija iz fajla sa user-ima i ubacivanje u niz stringova.
                if (File.Exists(usersFile))
                {
                    String[] usersFileLines = File.ReadAllLines(usersFile);
                    String[] usersList = new string[usersFileLines.Length];
                    for (int i = 0; i < usersFileLines.Length; i++)
                    {
                        usersList[i] = usersFileLines[i].IndexOf(":") > -1 ? usersFileLines[i].Substring(0, usersFileLines[i].IndexOf(":")) : usersFileLines[i];
                        if (String.Compare(usersList[i], username) == 0)
                        {
                            Console.WriteLine("This user already exists!");
                            FirstCommand();
                        }
                    }

                    using StreamWriter sw = File.AppendText(usersFile);
                    sw.WriteLine(newUser);
                    sw.Close();
                }
                Console.WriteLine("User '" + username + "' successfully created! Now you can log in with this user.\n");
                Directory.CreateDirectory(@"C:\root\" + username + @"\home\");
                FirstCommand();
            }
            else if (String.Compare(choice, "exit") == 0)
            {
                Console.Write("Exiting ...");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }

        }

        public void ExecuteCommand(string choice)
        {
            string firstWord = choice.IndexOf(" ") > -1 ? choice.Substring(0, choice.IndexOf(" ")) : choice;

            if (String.Compare(firstWord, "help") == 0)
            {
                Help help = new Help();
                help.HelpCommand(choice);
            }
            else if (String.Compare(firstWord, "where") == 0)
            {
                Where where = new Where();
                where.WhereCommand(choice, GetUser());
            }
            else if (String.Compare(firstWord, "go") == 0)
            {
                Go go = new Go();
                go.GoCommand(choice, activeUser);
            }
            else if (String.Compare(firstWord, "create") == 0)
            {
                Create create = new Create();
                create.CreateCommand(choice, activeUser);
            }
            else if (String.Compare(firstWord, "list") == 0)
            {
                List list = new List();
                list.ListCommand(choice, activeUser);
            }
            else if (String.Compare(firstWord, "print") == 0)
            {
                Print print = new Print();
                print.PrintCommand(choice, activeUser);
            }
            else if (String.Compare(firstWord, "find") == 0)
            {
                Find find = new Find();
                find.FindCommand(choice);
            }
            else if (String.Compare(firstWord, "findDat") == 0)
            {
                FindDat findDat = new FindDat();
                findDat.FindDatCommand(choice);
            }
            else
            {
                Console.WriteLine("\nThere is no such command! Please use 'help' to see available commands.\n");
            }
        }

        // Metoda za kucanje znaka '*' umjesto normalnih slova za sifru.
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password[0..^1];
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }
    }
}
