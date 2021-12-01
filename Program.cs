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
            string selected = "notChosen"; 
            while (!selected.Equals("0"))
            {
                Console.WriteLine("Pasirinkite scenarijų:\n1 - Vektoriaus siuntimas\n2 - teksto siuntimas\n0 - Baigti darba"); // \n3 - paveiksliuko siuntimas
                selected = Console.ReadLine();
                switch (selected)
                {
                    case "0":
                        Environment.Exit(0);
                        break;
                    case "1":
                        ScenarioHandler.HandleScenarioOne(probabilityNumber);
                        break;
                    case "2":
                        ScenarioHandler.HandleScenarioTwo(probabilityNumber);
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Pasirinkimas nerastas. Bandykite dar kartą.");
                        break;
                }
            }

        }

        // Vektoriaus spausdinimas 
        // Įeities parametrai: vektorius, kuris bus spausdinamas
        // Grąžinama: nieko
        static private void PrintCodeVector(int[] code)
        {
            foreach (int e in code)
            {
                Console.Write(e + " ");
            }
            Console.Write("\n");
        }
    }
}
