using System;
using System.Collections.Generic;
using System.Text;

/*
 * Pomocna klasa u kojoj se nalazi metoda koja broji rijeci u stringu koji je proslijedjen kroz komandnu liniju, konzolu. Koristio sam je u vecini 
 * komandi (njihovim metodama za izvrsavanje komande), pa da ne moram da je pisem nekoliko puta.
 */

namespace Shell
{
    public class HelperMethods
    {
        public int CountWordsInString(string stringToCount)
        {
            var words = stringToCount.Split(' ');

            var array = new List<string>();
            foreach (var word in words)
                array.Add(word);

            int count = 0;
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] != null)
                    count += 1;
            }

            return count;
        }
    }
}
