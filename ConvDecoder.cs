using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Theory
{
    class ConvDecoder
    {
        // Atminties blokų schemos
        private int[] memoryBlocks = new int[6] { 0, 0, 0, 0, 0, 0 }; 
        private int[] errorFixMemoryBlocks = new int[6] { 0, 0, 0, 0, 0, 0 };

        // Dekodavimas sasukos kodu [Ber84, §15.63, p.391, fig. 15.11]
        // Įeities parametrai: užkoduotas vektorius (kodas)
        // Grąžinama: dekoduotas vektorius (kodas)
        public int[] Decode(int[] encodedCode)
        {
            int[] fullDecodedCode = new int[encodedCode.Length];
            int j = 0;

            // Kiekvienos bitų poros dekodavimas į vieną bitą
            for(int i = 0; i < encodedCode.Length; i += 2)
            {
                int firstBit = encodedCode[i];
                int secondBit = encodedCode[i + 1];
                int decodedBit = DecodeBits(firstBit, secondBit);
                fullDecodedCode[j] = decodedBit;
                j++;
            }
            
            // Gaunamas sutvarkytas dekoduotas vektorius (kodas) (dekoderio būsenos bitų pašalinimas)
            int[] decodedCode = FixDecodedCode(fullDecodedCode);
            
            return decodedCode;
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

            // Perstatomos apatinės schemos atminties blokų reikšmės
            for (int i = 5; i > 0; i--)
            {
                errorFixMemoryBlocks[i] = errorFixMemoryBlocks[i - 1];
            }
            // Surasta esamo laiko momento atitinkamų atminties blokų bei įėjusių simbolių suma įrašoma į apatinę schemą
            errorFixMemoryBlocks[0] = bitForErrorFix;

            // Surandamas MDE sprendimo bitas (MDE rezultatas)
            int bitValueForError = GetErrorDecision(mdeSyndrome);
            int decodedBit = (bitValueForError + memoryBlocks[5]) % 2;

            // Perstatomos viršutinės schemos atminties blokų reikšmės
            for (int i = 5; i > 0; i--)
            {
                memoryBlocks[i] = memoryBlocks[i - 1];
            }
            // Pirmas poros simbolis įrašomas į viršutinę schemą
            memoryBlocks[0] = firstBit;

            return decodedBit;
        }

        // MDE rezultato suradimas
        // Įeities parametrai: 4 MDE bitai
        // Grąžinama: vienas bitas, taisantis klaidą
        private int GetErrorDecision(int[] mdeSyndrome)
        {
            int sumOfSyndrome = mdeSyndrome[0] + mdeSyndrome[1] + mdeSyndrome[2] + mdeSyndrome[3];
            if (sumOfSyndrome >= 3)
            {
                return 1;
            }
            return 0;
        }

        // Pašalinami dekoderio būsenos bitai
        // Įeities parametrai: dekoduotas vektorius (kodas)
        // Grąžinama: dekoduotas vektorius (kodas) be dekoderio būsenos bitų
        private int[] FixDecodedCode(int[] decodedCode)
        {
            int[] fixedCode = new int[decodedCode.Length / 2 - 6];
            int j = 0;
            for (int i = 6; i < decodedCode.Length / 2; i++)
            {
                fixedCode[j] = decodedCode[i];
                j++;
            }
            return fixedCode;
        }
    }
}
