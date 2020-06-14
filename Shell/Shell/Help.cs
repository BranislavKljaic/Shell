using System;
using System.Collections.Generic;
using System.Text;

/*
 * Help komanda sluzi za opis dostupnih komandi. Ukoliko unesemo ime komande kao drugi argument u konzoli, ispisuje se pomoc za koristenje date 
 * komande.
 */

namespace Shell
{
    public class Help
    {
        public void HelpCommand(string command)
        {
            // Ako je command samo jedna rijec, to je i commandToExecute, ako ima vise rijeci onda je commandToExecute sve osim prve rijeci.
            string commandToExecute = command.IndexOf(" ") > -1 ? command.Substring(command.IndexOf(" ") + 1) : command;

            if (String.Compare(command, "help") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Available commands are:");
                Console.WriteLine("\t'where'\t\t-> shows up current path.");
                Console.WriteLine("\t'go'\t\t-> changing current directory.");
                Console.WriteLine("\t'create'\t\t-> creating a directory or a file.");
                Console.WriteLine("\t'list'\t\t-> listing the content of a directory.");
                Console.WriteLine("\t'print'\t\t-> printing the content of a file in the console.");
                Console.WriteLine("\t'find'\t\t-> searching text in a file.");
                Console.WriteLine("\t'findDat'\t-> searching specific file on the system.");
                Console.ResetColor();
            }
            else if(String.Compare(commandToExecute, "where") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nCommand 'where' help: ");
                Console.WriteLine("\nCommand 'where' shows directory where you are currently located." +
                    "\nValid use of this command is just typing key word.\nFor example if you are located in your home directory, running 'where' will output:" +
                    " \\root\\username\\home\\\n");
                Console.ResetColor();
            }
            else if (String.Compare(commandToExecute, "go") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nCommand 'go' help: ");
                Console.WriteLine("\nCommand 'go' is used to change path and switch between different directories. For example, is you have directory in your home" +
                    " folder named 'datoteka', to change your path there you write: 'go \\root\\username\\home\\datoteka\n");
                Console.ResetColor();
            }
            else if (String.Compare(commandToExecute, "create") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nCommand 'create' help: ");
                Console.WriteLine("\nCommand 'create' is used to create a directory or a file on the specific path.\n" +
                    "If '-d' is specified, shell will create directory and if you do not specify '-d' parameter, shell is creating a file.\n" +
                    "If you do not write full path, directory/file will be created on the path where you are at the moment.\n" +
                    "Current path can be checked using command 'where'." +
                    "Some examples of create usage: \n" +
                    "\tcreate -d /home/dir1\t-> creates directory dir1 in directory home\n" +
                    "\tcreate /home/dat1\t-> creates file dat1 in directory home\n" +
                    "\tcreate -d dir1\t\t-> creates directory dir1 in current directory\n" +
                    "\tcreate dat1\t\t-> creates file dat1 in current directory\n");
                Console.ResetColor();
            }
            else if (String.Compare(commandToExecute, "list") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nCommand 'list' help: ");
                Console.WriteLine("\nCommand 'list' is used to list the content of a specific directory.\n" +
                    "If you want to list content of directory dir1 which is in home directory, you will do it like this: 'list \\root\\username\\home\\dir1'\n" +
                    "If you find yourself in home directory at the moment, you can just type 'list dir1' and you will get content like for example:\n" +
                    "dir1\n\tdat11\n\tdat12\n\tdir11\n\t\tdat111\ndir2\n\tdat21\n");
                Console.ResetColor();
            }
            else if (String.Compare(commandToExecute, "print") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nCommand 'print' help: ");
                Console.WriteLine("\nCommand 'print' is used to print out the content of a text file on regular output (console).\n" +
                    "If name of the file is 'Branislav.txt', and its content is 'Branislav ce dobiti sve bodove iz projektnog zadatka'\n" +
                    "Using 'print Branislav.txt', console output will be:\n" +
                    "'Branislav ce dobiti sve bodove iz projektnog zadatka'\n");
                Console.ResetColor();
            }
            else if (String.Compare(commandToExecute, "find") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nCommand 'find' help: ");
                Console.WriteLine("\nCommand 'find' is searching content of a file and if it finds certain text it prints out number of row of the text.\n" +
                    "For example if content of the text is:\n'Vecina ljudi ne zeli slobodu, \njer sloboda znaci odgovornost. \nVecina ljudi \nse uzasava od odgovornosti.'\n" +
                    "Using 'find \"jer sloboda znaci odgovornost\" file1.txt'\n" +
                    "Will print out on console: 2\n");
                Console.ResetColor();
            }
            else if (String.Compare(commandToExecute, "findDat") == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nCommand 'findDat' help: ");
                Console.WriteLine("\nCommand 'findDat' is searching for a file through directory which we give as a parameter.\n" +
                    "Usage of this command is for example: 'findDat slika.png \\home\\username\\slike'\n" +
                    "If file does not exist on a current path, output will be blank, otherwise it will print out name of the file.\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Unrecognizable use of command 'help'!");
                Console.ResetColor();
            }
        }
    }
}
