using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Theory
{
    class Channel
    {
        private double probability;
        Random random = new Random();

        // Sukuriamas kanalas su duota klaidos tikimybe
        // Įeities parametrai: klaidos tikimybė
        public Channel(double probability)
        {
            this.probability = probability;
        }

        // Siutnimo kanalu realizacija. Imituojamas užkoduoto kodo siuntimas kanalu
        // Įeities parametrai: užkoduotas kodas
        // Grąžinama: kanalu persiųstas iškraipytas kodas
        public int[] SendCode(int[] encodedCode)
        {
            int[] distortedCode = new int[encodedCode.Length];

            for(int i = 0; i < encodedCode.Length; i++)
            {
                // Kiekvienam siunčiamam kūno Fq elementui traukiamas atsitiktinis skaičius a iš intervalo [0,1]. 
                double randomValue = random.NextDouble();
                // Jei a mažesnis už klaidos tikimybę pe, siunčiamą elementą kanalas turi iškraipyti, jei ne - neturi. 
                if (randomValue < probability)
                {
                    // Iškraipymas
                    if (encodedCode[i] == 0)
                        distortedCode[i] = 1;
                    else
                        distortedCode[i] = 0;
                }
                else
                {
                    distortedCode[i] = encodedCode[i];
                }
            }
            return distortedCode;
        }

        // Skaičiuojamas klaidų kiekis ir jų pozicijos
        // Įeities parametrai: kanalu persiųstas iškraipytas kodas ir kanalu siųstas pirminis užkoduotas kodas
        // Grąžinama: sąrašas su klaidų pozicijomis
        public List<int> GetErrorsAndPositions(int[] distortedCode, int[] originalCode)
        {
            List<int> errors = new List<int>();
            for (int i = 0; i < originalCode.Length; i++)
            {
                if(distortedCode[i] != originalCode[i])
                {
                    errors.Add(i);
                }
            }
            return errors;
        }

        // Vartotojo nurodytos norimos taisyti pozicijos taiymo metodas
        // Įeities parametrai: taisomas vektorius, norima pataisyti pozicija
        // Grąžinama: pataisytas vektorius
        public int[] ResolveErrorByHand(int[] distortedCode, int position)
        {
            switch (distortedCode[position])
            {
                case 0:
                    distortedCode[position] = 1;
                    break;
                case 1:
                    distortedCode[position] = 0;
                    break;
            }
            return distortedCode;
        }
     
    }
}
