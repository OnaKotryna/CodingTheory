using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Theory
{
    static class ScenarioHandler
    {
        private static ConvEncoder encoder = new ConvEncoder();
        private static ConvDecoder decoder = new ConvDecoder();


        // Apdorojamas pirmas scenarijus - naudotojas įveda vektorių
        // Įeities parametrai: klaidos tikimybė
        public static void HandleScenarioOne(double probabilityNumber)
        {
            int[] vector = new int[] { };
            vector = InputHandler.HandleVector();

            // KODAVIMAS
            int[] encodedVector = encoder.Encode(vector);

            Console.WriteLine("Uzkoduotas vektorius:");
            PrintCodeVector(encodedVector);

            // SIUNTIMAS KANALU
            Channel channel = new Channel(probabilityNumber);
            int[] distortedCode = channel.SendCode(encodedVector);

            Console.WriteLine("Kanalu persiustas vektorius:");
            PrintCodeVector(distortedCode);

            // Pranešama, kiek ir kuriose pozicijose įvyko klaidų
            List<int> errors = ErrorHandler.GetErrorsAndPositions(distortedCode, encodedVector);
            Console.WriteLine("Padarytos klaidos:");
            Console.WriteLine("  Klaidu kiekis: " + errors.Count);
            Console.Write("  Klaidu pozicijos: ");
            foreach (int i in errors)
            {
                Console.Write(i + " ");
            }
            Console.Write("\n");

            // Klaidų redagavimas pagal vartotojo pateiktas pozicijas
            Console.WriteLine("Ar taisyti klaidas? t/n");
            string resolveAnswer = Console.ReadLine();
            while (resolveAnswer.StartsWith('t'))
            {
                Console.WriteLine("Iveskite klaidos pozicija:");
                // Įvedama klaidos pozicija
                int position = Convert.ToInt32(Console.ReadLine());
                // Patikrinama, ar vartotojo įrasyta pozicija priklauso vektoriui
                if (position < distortedCode.Length && position >= 0)
                {
                    // Taisomas elementas nurodytoje pozicijoje
                    distortedCode = ErrorHandler.ResolveErrorByHand(distortedCode, position);
                    // Darbo tesimui kiek norima kartų
                    Console.WriteLine("Rankiniu budu pataisytas vektorius:");
                    PrintCodeVector(distortedCode);
                    Console.WriteLine("Testi taisyma? t/n");
                    resolveAnswer = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Klaidinga pozicija.");
                }
            }

            // DEKODAVIMAS
            int[] decodedVector = decoder.Decode(distortedCode);

            Console.WriteLine("Ivestas vektorius:");
            PrintCodeVector(vector);

            // Atspausdinamas rezultatas
            Console.WriteLine("Dekoduotas rezultatas:");
            PrintCodeVector(decodedVector);

        }

        // Apdorojamas antras scenarijus - naudotojas įveda tekstą
        // Įeities parametrai: klaidos tikimybė
        public static void HandleScenarioTwo(double probabilityNumber)
        {
            int[] textVector;
            textVector = InputHandler.HandleText();

            Channel channel = new Channel(probabilityNumber);
            int[] textVectorFromChannel = channel.SendCode(textVector);

            Console.WriteLine("Kanalu persiustas teksto vektorius be kodavimo:");
            string result = string.Join("", textVectorFromChannel);
            Console.WriteLine(InputHandler.BinaryToString(result));

            // Teksto bitų kodavimas, siuntimas kanalu ir dekodavimas
            int[] encodedTextVector = encoder.Encode(textVector);
            int[] encodedTextVectorFromChannel = channel.SendCode(encodedTextVector);
            int[] decodedTextVector = decoder.Decode(encodedTextVectorFromChannel);

            // Dekoduoto persiųsto teksto atspausdinimas
            Console.WriteLine("Dekoduotas kanalu persiustas tekstas:");
            string decodedText = string.Join("", decodedTextVector);
            Console.WriteLine(InputHandler.BinaryToString(decodedText));

        }

        // Vektoriaus bitų spausdinimas 
        // Įeities parametrai: vektorius, kuris bus spausdinamas
        // Grąžinama: atvaizduojamas vektorius konsolėje
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
