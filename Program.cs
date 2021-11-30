using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Coding_Theory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Užduotis A15");

            double probabilityNumber = 0;
            bool getProbability = true;

            // Klaidos tikimybes ivedimas
            while (getProbability)
            {
                Console.WriteLine("Nurodykite klaidos tikimybę:");
                string givenProbability = Console.ReadLine();
                givenProbability = givenProbability.Replace(',', '.');
                if (Double.TryParse(givenProbability, out probabilityNumber) && probabilityNumber < 1)
                {
                    getProbability = false;
                }
                else
                {
                    Console.WriteLine("Klaida tikimybės nurodyme.", givenProbability);
                }
            }

            // Scenarijaus pasirinkimas
            /*Console.WriteLine("Pasirinkite scenarijų:\n1 - Vektoriaus siuntimas"); // \n2 - teksto siuntmas\n3 - paveiksliuko siuntimas
            int selected = Int32.Parse(Console.ReadLine());
            switch (selected)
            {
                case 1:
                    SelectionHandler.HandleVector();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("pasirinkimas nerastas");
                    break;
            }*/

            SelectionHandler.HandleVector();
            
        }
        
    }
}
