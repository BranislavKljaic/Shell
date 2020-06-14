using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

/*
 * Create komanda sluzi za kreiranje fajla i kreiranje foldera. U slucaju unosenja jedne rijeci, pored rijeci komande, (ili putanje) sa ekstenzijom 
 * kreira se fajl, a u slucaju unosenja argumenta '-d' kreira se folder i on mora da bude bez ekstenzije. Ako je druga rijec u stringu koji se 
 * unosi u konzolu '-d', validan unesen string moze da ima maksimalno tri rijeci i u tom slucajno se pravi folder, ukoliko druga rijec nije '-d' u 
 * unesenom stringu u konzoli, ona samo i sigurno moze samo da se fajl napravi, ali ako nema ekstenzije ispisace se odgovarajuca poruka.
 */

namespace Shell
{
    public class Create
    {
        private readonly HelperMethods helperMethods = new HelperMethods();

        public void CreateCommand(string command, User user)
        {
            // Ako je command samo jedna rijec, to je i commandToExecute, ako ima vise rijeci onda je commandToExecute sve osim prve rijeci.
            string commandToExecute = command.IndexOf(" ") > -1 ? command.Substring(command.IndexOf(" ") + 1) : command;

            if (String.Compare(command, "create") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nUsing just word 'create' does not execute any type of command. Use 'help create' if you need help with the command!\n");
                Console.ResetColor();
            }
            // Ako se osim create unese samo jos jedna rijec, onda se samo datoteka moze napraviti. Zahtjevam da se unese ekstenzija!
            else if (helperMethods.CountWordsInString(commandToExecute) == 1)
            {
                string pathToCreate = @"C:\" + commandToExecute;

                if (String.Compare(commandToExecute, "-d") == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nYou must give the name to directory!\n");
                    Console.ResetColor();
                }
                else if (!commandToExecute.Contains('\\'))
                {
                    if (!File.Exists(commandToExecute) && Path.HasExtension(commandToExecute))
                    {
                        File.Create(user.GetFullUserPath() + commandToExecute);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nFile with name " + commandToExecute + " successfually created!\n");
                        Console.ResetColor();
                    }
                    else if (File.Exists(commandToExecute))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nCurrent file already exists!\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nUnrecognizable file extension!\n");
                        Console.ResetColor();
                    }
                }
                else if (commandToExecute.Contains('\\'))
                {
                    try
                    {
                        string fileToCreate = Path.GetFileName(pathToCreate);
                        if (Path.HasExtension(fileToCreate))
                        {
                            if (!File.Exists(pathToCreate))
                            {
                                File.Create(pathToCreate);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nFile with name " + fileToCreate + " successfually created!\n");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nFile already exists!\n");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nYou must specify the extension!\n");
                            Console.ResetColor();
                        }
                    }
                    catch(IOException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nUnrecognizable use of command 'create'! If you need help with this command type 'help create'.\n");
                    Console.ResetColor();
                }
            }
            // Ako se unesu dvije rijeci, osim create, onda se samo direktorijum moze praviti.
            else if (helperMethods.CountWordsInString(commandToExecute) == 2)
            {
                string dArgument = commandToExecute.Substring(0, commandToExecute.IndexOf(" "));
                string commandForDirectory = commandToExecute.IndexOf(" ") > -1 ? commandToExecute.Substring(commandToExecute.IndexOf(" ") + 1) : commandToExecute;
                
                if (String.Compare(dArgument, "-d") == 0)
                {
                    if (!commandForDirectory.Contains('\\'))
                    {
                        if (!Path.HasExtension(commandForDirectory))
                        {
                            if (!Directory.Exists(commandForDirectory))
                            {
                                Directory.CreateDirectory(commandForDirectory);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nDirectory " + commandForDirectory + " successfully created!\n");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nDirectory already exists!\n");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nDirectory must not have extension!\n");
                            Console.ResetColor();
                        }
                    }
                    else if (commandForDirectory.Contains('\\'))
                    {
                        string pathForDirectory = @"C:\" + commandForDirectory;
                        try
                        {
                            string directoryToCreate = Path.GetFileName(commandForDirectory);
                            if (!Path.HasExtension(directoryToCreate) && !pathForDirectory.EndsWith('\\'))
                            {
                                if (!Directory.Exists(pathForDirectory)) {

                                    Directory.CreateDirectory(pathForDirectory);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nDirectory with name " + directoryToCreate + " successfually created!\n");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nDirectory already exists!\n");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nDirectory must not have extension, and/or must have name!\n");
                                Console.ResetColor();
                            }
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine("\nThere was a problem handling this: !" + ex.Message + "\n");
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nYou provided invalid argument!\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nUnrecognizable use of command 'create'! If you need help with this command type 'help create'.\n");
                Console.ResetColor();
            }
        }
    }
}