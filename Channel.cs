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
     
    }
}
