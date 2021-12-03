using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Theory
{
    static class ErrorHandler
    {

        // Skaičiuojamas klaidų kiekis ir jų pozicijos
        // Įeities parametrai: kanalu persiųstas iškraipytas kodas ir kanalu siųstas pirminis užkoduotas kodas
        // Grąžinama: sąrašas su klaidų pozicijomis
        public static List<int> GetErrorsAndPositions(int[] distortedCode, int[] originalCode)
        {
            List<int> errors = new List<int>();
            for (int i = 0; i < originalCode.Length; i++)
            {
                if (distortedCode[i] != originalCode[i])
                {
                    errors.Add(i);
                }
            }
            return errors;
        }

        // Naudotojo nurodytos norimos taisyti pozicijos taisymo metodas
        // Įeities parametrai: taisomas vektorius, norima pataisyti pozicija
        // Grąžinama: pataisytas vektorius
        public static int[] ResolveErrorByHand(int[] distortedCode, int position)
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
