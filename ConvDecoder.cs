using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Theory
{
    class ConvDecoder
    {
        int[] memoryBlocks = new int[6] { 0, 0, 0, 0, 0, 0 }; // kodo būsena (stumiamojo registro atiminties turinys 26 psl.)

        int[] errorFixMemoryBlocks = new int[6] { 0, 0, 0, 0, 0, 0 };

        // Vektoriaus dekodavimas sasukos kodu [Ber84, §15.63, p.391, fig. 15.11]
        // Įeities parametrai: užkoduotas vektorius
        // Grąžinama: dekoduotas vektorius
        public int[] Decode(int[] encodedCode)
        {
            int[] decodedCode = new int[encodedCode.Length];
            int j = 0;

            for(int i = 0; i < encodedCode.Length; i += 2)
            {
                int firstBit = encodedCode[i];
                int secondBit = encodedCode[i + 1];
                Console.Write("Dekoduojami bitai: " + firstBit + secondBit+" ");
                int decodedBit = DecodeBits(firstBit, secondBit);
                decodedCode[j] = decodedBit;
                j++;
            }

            int[] finalDecodedCode = new int[decodedCode.Length / 2 - 6];
            j = 0;
            for(int i = 6; i < decodedCode.Length / 2; i++)
            {
                finalDecodedCode[j] = decodedCode[i];
                j++;
            }

            return finalDecodedCode;
        }

        // Bitų porų dekodavimas į vieną bitą
        private int DecodeBits(int firstBit, int secondBit)
        {
            // Surandama esamo laiko momento atitinkamų atminties blokų bei įėjusių simbolių suma
            // Jei klaidų nėra 0, jei klaidų yra 1
            int bitForErrorFix = (firstBit + memoryBlocks[1] + memoryBlocks[4] + memoryBlocks[5] + secondBit) % 2;

            // Surandama esamo laiko momento mde simbolių seka
            int[] mdeSyndrome = new int[4];
            mdeSyndrome[0] = bitForErrorFix;
            mdeSyndrome[1] = errorFixMemoryBlocks[0];
            mdeSyndrome[2] = errorFixMemoryBlocks[3];
            mdeSyndrome[3] = errorFixMemoryBlocks[5];

            Console.Write("  MDE: ");
            foreach (int digit in mdeSyndrome)
            {
                Console.Write(digit + " ");
            }

            Console.Write("  Apatine: ");
            foreach (int block in errorFixMemoryBlocks)
            {
                Console.Write(block + " ");
            }
            // perstatomi apatines schemos registrai 
            for (int i = 5; i > 0; i--)
            {
                errorFixMemoryBlocks[i] = errorFixMemoryBlocks[i - 1];
            }
            // surasta esamo laiko momento atitinkamų atminties blokų bei įėjusių simbolių suma įrašoma į apatinę schemą
            errorFixMemoryBlocks[0] = bitForErrorFix;

            

            int bitValueForError = GetErrorDecision(mdeSyndrome);
            int decodedBit = (bitValueForError + memoryBlocks[5]) % 2;

            Console.Write(String.Format("   MDE: {0}   Dekoduotas bitas {1}\n", bitValueForError, decodedBit));

            Console.Write("Virsutine: ");
            foreach (int block in memoryBlocks)
            {
                Console.Write(block + " ");
            }
            //Console.Write("\n");
            for (int i = 5; i > 0; i--)
            {
                memoryBlocks[i] = memoryBlocks[i - 1];
            }
            // pirmas poros simbolis keliauja i virsutine schema
            memoryBlocks[0] = firstBit;

            return decodedBit;
        }

        private int GetErrorDecision(int[] mdeSyndrome)
        {
            int sumOfSyndrome = mdeSyndrome[0] + mdeSyndrome[1] + mdeSyndrome[2] + mdeSyndrome[3];
            Console.Write("suma " + sumOfSyndrome);
            if (sumOfSyndrome >= 3)
            {
                return 1;
            }
            return 0;
        }
    }
}
