﻿using System;
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

            double probabilityNumber = 0.3;
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
            Console.WriteLine("Pasirinkite scenarijų:\n1 - Vektoriaus siuntimas\n0 - Baigti darba"); // \n2 - teksto siuntmas\n3 - paveiksliuko siuntimas
            int selected = Int32.Parse(Console.ReadLine());
            int[] vector = new int[] { };

            switch (selected)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    vector = SelectionHandler.HandleVector();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("pasirinkimas nerastas");
                    break;
            }
            

            //int[] vector = SelectionHandler.HandleVector();
           //int[] vector = new int[] { 1, 1, 0, 1, 0 };
            Console.WriteLine("Ivestas vektorius:");
            PrintCodeVector(vector);

            // KODAVIMAS
            ConvEncoder encoder = new ConvEncoder();
            int[] encodedVector = encoder.Encode(vector);

            Console.WriteLine("Uzkoduotas vektorius:");
            PrintCodeVector(encodedVector);

            // SIUNTIMAS KANALU
            Channel channel = new Channel(probabilityNumber);
            int[] distortedCode = channel.SendCode(encodedVector);

            Console.WriteLine("Kanalu persiustas vektorius:");
            PrintCodeVector(distortedCode);

            // Pranešama, kiek ir kuriose pozicijose įvyko klaidų
            List<int> errors = channel.GetErrorsAndPositions(distortedCode, encodedVector);
            Console.WriteLine("Padarytos klaidos:");
            Console.WriteLine("  Klaidu kiekis: " + errors.Count);
            Console.Write("  Klaidu pozicijos: ");
            foreach (int i in errors)
            {
                Console.Write(i + " ");
            }
            Console.Write("\n");

            // Klaidų redagavimas pagal vartotojo pateiktas pozicijas
            Console.WriteLine("Ar taisyti klaidas? y/n");
            string resolveAnswer = Console.ReadLine();

            while (resolveAnswer.StartsWith('y'))
            {
                Console.WriteLine("Iveskite klaidos pozicija:");
                // Įvedama klaidos pozicija
                int position = Convert.ToInt32(Console.ReadLine());
                // Patikrinama, ar vartotojo įrasyta pozicija priklauso vektoriui
                if (position < distortedCode.Length && position >= 0)
                {
                    // Taisomas elementas nurodytoje pozicijoje
                    distortedCode = channel.ResolveErrorByHand(distortedCode, position);
                    // Darbo tesimui kiek norima kartų
                    Console.WriteLine("Testi taisyma? y/n");
                    resolveAnswer = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Klaidinga pozicija.");
                }
            }

            // DEKODAVIMAS
            ConvDecoder convDecoder = new ConvDecoder();
            int[] decodedVector = convDecoder.Decode(distortedCode);

            // Atspausdinamas rezultatas
            Console.WriteLine("Dekoduotas rezultatas:");
            PrintCodeVector(decodedVector);

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
