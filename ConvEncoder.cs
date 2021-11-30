using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Theory
{
    class ConvEncoder
    {
        int[] memoryBlocks = new int[6] { 0, 0, 0, 0, 0, 0 }; // kodo būsena (stumiamojo registro atiminties turinys 26 psl.)

        // Vektoriaus kodavimas sasukos kodu [Ber84, §15.62, p.390, fig. 15.10]
        // Įeities parametrai: vartotojo įvestas vektorius
        // Grąžinama: užkoduotas vektorius
        public int[] Encode(int[] vector)
        {
            int[] vectorToCode = new int[vector.Length + 6];
            // Vektorius papildomas šešiais 0 tam, kad kodavimo pabaigoje išsivalytų atminties registrai
            foreach(int element in vectorToCode)
            {
                vectorToCode[element] = 0;
            }
            for(int i = 0; i < vector.Length; i++)
            {
                vectorToCode[i] = vector[i];
            }

            // Užkoduoto vektoriaus kintamasis
            int[] encodedVector = new int[vectorToCode.Length * 2];
            // Užkoduoto vektoriaus elementų numeratorius
            int j = 0; 

            // Vektoriaus bitų kodavimas dviem simboliais
            for(int i = 0; i < vectorToCode.Length; i++)
            {
                // pirmas simbolis - į regstrą įėjęs bitas
                encodedVector[j] = vectorToCode[i];
                j++;
                // Console.WriteLine("Koduojamas bitas: " + vectorToCode[i]);
                // antras simbolis - į registrą įėjęs bitas susumuotas su atitinkamais atminties registro blokais (2, 5, 6)
                encodedVector[j] = CountSecondBit(vectorToCode[i]);
                j++;
                // Console.WriteLine(String.Format("{0} bitas užkodutas {1}{2}", vectorToCode[i], encodedVector[j-2], encodedVector[j-1]));
            }

            return encodedVector;
        }

        // Koduojamas antras išeinantis iš registro bitas
        // Įeities parametrai: koduojamas bitas
        // Grąžinama: apskaičiotas užkoduotas bitas
        private int CountSecondBit(int bit)
        {
            /*Console.Write("Atminties blokai bituko skaiciavimo laiko momentu: ");
            foreach (int block in memoryBlocks)
            {
                Console.Write(block + " ");
            }
            Console.Write("\n");*/
            // suskaičiuojama antro simbolio reikšmė [Ber84, §15.62, p. 389–390]
            int countedBit = (bit + memoryBlocks[1] + memoryBlocks[4] + memoryBlocks[5])%2; // 2, 5, 6 atminties blokai + iejes bitukas
            // perstumiamos atminties blokų reikšmės
            for(int i = 5; i > 0; i--)
            {
                memoryBlocks[i] = memoryBlocks[i - 1];
            }
            memoryBlocks[0] = bit;

            return countedBit;
        }

    }
}
