using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Coding_Theory
{
    static class SelectionHandler
    {

        // Ivedamas vartotojo vektorius
        // ir siunciamas kodavimui
        public static void HandleVector()
        {
            bool getVector = true;
            int[] vector;
            while (getVector)
            {
                Console.WriteLine("Iveskite dvejetaini vektoriu: \nPvz. 010010");
                string givenVector = Console.ReadLine();
                if (Regex.IsMatch(givenVector, "^[01]+$"))
                {
                    vector = new int[givenVector.Length];
                    for (int i = 0; i < givenVector.Length; i++)
                    {
                        vector[i] = Convert.ToInt32(givenVector[i].ToString());
                    }
                    getVector = false;
                }
                else
                {
                    Console.WriteLine("\nVektorius nera dvejetainis\n");
                }
            }
        }

    }
}
