using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Coding_Theory
{
    static class InputHandler
    {

        // Įvedamas naudotojo vektorius
        // ir šis siunčiamas kodavimui
        // Grąžinama: įvestas naudotojo vektorius
        public static int[] HandleVector()
        {
            bool getVector = true;
            int[] vector = new int[] { };
            while (getVector)
            {
                Console.WriteLine("Iveskite dvejetaini vektoriu: Pvz. 010010");
                // Įvedamas vektorius
                string givenVector = Console.ReadLine();
                // patikrinamas vektoriaus tinkamumas
                if (Regex.IsMatch(givenVector, "^[01]+$"))
                {
                    vector = new int[givenVector.Length];
                    // Paruošiamas vektorius kodavimui (iš string verčiama į int)
                    for (int i = 0; i < givenVector.Length; i++)
                    {
                        vector[i] = Convert.ToInt32(givenVector[i].ToString());
                    }
                    getVector = false;
                }
                else
                {
                    Console.WriteLine("\nVektorius nera dvejetainis\n");
                }
            }

            return vector;
        }

        // Apdoroja naudotojo teksto įvedimą ir tekstą paverčia vektoriumi 
        // Grąžinama: įvestas naudotojo tekstas paverstas bitų srautu
        public static int[] HandleText()
        {
            Console.WriteLine("Iveskite teksta:\nNoredami baigti iveskite tuscia eilute");
            // Įvedamas tekstas, kuris gali susidaryti iš kelių eilučių
            string giventext = "";
            string line;
            while (!String.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                giventext = giventext + line + '\n';
            }
            // tekstas paverčiamas į bitų seką
            string binaryText = StringToBinary(giventext);
            int[] vector = new int[binaryText.Length];
            for (int i = 0; i < binaryText.Length; i++)
            {
                vector[i] = Convert.ToInt32(binaryText[i].ToString());
            }

            return vector;
        }

        // Teksto vertimas bitais
        // Įeities parametrai: tekstas
        // Grąžinama: teksto bitai
        public static string StringToBinary(string text)
        {
            StringBuilder binaryCode = new StringBuilder();

            foreach (char c in text.ToCharArray())
            {
                binaryCode.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return binaryCode.ToString();
        }
        
        // Bitų vertimas tekstu 
        // Įeities parametrai: bitų eilutė
        // Grąžinama: tekstas
        public static string BinaryToString(string binaryText)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < binaryText.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(binaryText.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

    }
   
}
