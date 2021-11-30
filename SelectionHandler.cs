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
        // ir sis siunciamas kodavimui
        public static int[] HandleVector()
        {
            bool getVector = true;
            int[] vector = new int[] { };
            while (getVector)
            {
                Console.WriteLine("Iveskite dvejetaini vektoriu: \nPvz. 010010");
                // Ivedamas vektorius
                string givenVector = Console.ReadLine();
                // patikrinamas vektoriaus tinkamumas
                if (Regex.IsMatch(givenVector, "^[01]+$"))
                {
                    vector = new int[givenVector.Length];
                    //paruosiamas vektorius kodavimui (is string verciama i int)
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

            Console.WriteLine("Siunciama kodavimui:'))");
            return vector;

            // Console.WriteLine("Uzkoduotas ivestas vektorius:");
        }

    }
}
