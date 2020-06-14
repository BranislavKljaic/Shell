using System;
using System.Collections.Generic;
using System.Text;

/*
 * Klasa koja reprezentuje korisnika. homePath je ono sta se ispisuje na konzoli, path trenutni (\root\admin\home\ na primjer), dok je fullUserPath 
 * stvarni path gdje se trenutno nalazimo (C:\Users\Branislav\...\root\admin\home\ na primjer).
 */

namespace Shell
{
    public class User
    {
        private string username;
        private string password;
        private string homePath;
        private string fullUserPath;

        public void SetUsername(string username)
        {
            this.username = username;
        }

        public string GetUsername()
        {
            return username;
        }
        public void SetPassword(string password)
        {
            this.password = password;
        }

        public string GetPassword()
        {
            return password;
        }

        public void SetUserPath(string path)
        {
            this.homePath = path;
        }

        public string GetUserPath()
        {
            return homePath;
        }
        public void SetFullUserPath(string path)
        {
            this.fullUserPath = path;
        }
        
        public string GetFullUserPath()
        {
            return fullUserPath;
        }
    }
}
