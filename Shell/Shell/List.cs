using System;
using System.IO;

/*
 * Komanda List sluzi za izlistavanje sadrzaja direktorijuma koji je proslijedjen kao path ili trenutnog direktorijuma, ukljucujuci sve 
 * njegove pod-direktorijume i njihove fajlove. Ako se navede samo komanda list, bez ikakvih argumenata, izlistace se sadrzaj trenutnog direktorijuma.
 */

namespace Shell
{
    public class List
    {
        private readonly HelperMethods helperMethods = new HelperMethods();
        private static User activeUser;
        private static int counter = 0;

        public void ListCommand(string command, User user)
        {
            // Ako je command samo jedna rijec, to je i commandToExecute, ako ima vise rijeci onda je commandToExecute sve osim prve rijeci.
            string commandToExecute = command.IndexOf(" ") > -1 ? command.Substring(command.IndexOf(" ") + 1) : command;
            activeUser = user;

            if (String.Compare(command, "list") == 0)
            {
                ListContent(user.GetFullUserPath());
            }
            else if (helperMethods.CountWordsInString(commandToExecute) == 1 && String.Compare(commandToExecute, "list") != 0)
            {
                string pathToList = @"C:" + commandToExecute;
                if (Directory.Exists(pathToList))
                {
                    // Cuvanje trenutnog direktorijuma prije nego sto izlistam trazeni direktorijum.
                    string beforeListing = Directory.GetCurrentDirectory();
                    Directory.SetCurrentDirectory(pathToList);
                    ListContent(pathToList);
                    // Vracanje na direktorijum iz kojeg smo pozvali da se izlista sadrzaj trazenog direktorijuma.
                    Directory.SetCurrentDirectory(beforeListing);
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
                Console.WriteLine("\nUnrecognizable use of command 'list'! If you need help with this command type 'help list'.\n");
                Console.ResetColor();
            }
        }

        public void ListContent(string path)
        {
            // Ako postoji putanja, tj ako postoji taj direktorijum.
            if (Directory.Exists(path))
            {
                // Stavljam sve fajlove u jedan niz, i sve foldere u drugi niz, pa ih spajam.
                string[] filePaths = Directory.GetFiles(path);
                string[] allFiles = Directory.GetDirectories(Directory.GetCurrentDirectory());
                String[] filesAndFolders = new String[filePaths.Length + allFiles.Length];
                Array.Copy(filePaths, filesAndFolders, filePaths.Length);
                Array.Copy(allFiles, 0, filesAndFolders, filePaths.Length, allFiles.Length);

                for (int i = 0; i < filesAndFolders.Length; i++)
                {
                    string extension = Path.GetExtension(filesAndFolders[i]);
                    if (String.Compare(extension, "") == 0)
                    {
                        counter += 1;
                        for (int j = 0; j < counter - 1; j++) Console.Write("\t");
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine(Path.GetFileName(filesAndFolders[i]));
                        Console.ResetColor();
                        Directory.SetCurrentDirectory(Path.GetFullPath(filesAndFolders[i]));
                        ListContent(Path.GetFullPath(filesAndFolders[i]));
                        counter--;
                    }
                    else
                    {
                        for (int j = 0; j < counter; j++) Console.Write("\t");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(Path.GetFileName(filesAndFolders[i]));
                        Console.ResetColor();
                    }
                }
            }
            Directory.SetCurrentDirectory(activeUser.GetFullUserPath());
        }
    }
}
