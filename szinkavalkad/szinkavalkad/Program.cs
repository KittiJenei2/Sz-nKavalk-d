using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szinkavalkad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> alapSzinek = new List<string> { "piros", "kék", "sárga", "zöld", "lila", "rózsaszín"};

            Console.WriteLine("Add meg hány színt szeretnél kitalálni: ");
            int darab = Convert.ToInt32(Console.ReadLine());
            while (darab > 6)
            {
                Console.WriteLine("Maximum 6 színt választhatsz");
                Console.WriteLine("Add meg hány színt szeretnél kitalálni: ");
                darab = Convert.ToInt32(Console.ReadLine());
            }
            Random rnd = new Random();
            List<string> feladvany = new List<string>();
            while (feladvany.Count < darab)
            {
                string color = alapSzinek[rnd.Next(alapSzinek.Count)];
                if (!feladvany.Contains(color))
                {
                    feladvany.Add(color);
                }
            }

            Console.WriteLine($"Találd ki a {darab} színt a helyes sorrendben!");
            Console.WriteLine("Választható színek: {piros, kék, zöld, sárga, lila, rózsaszín}");
            for (int i = 0; i < darab; i++)
            {
                Console.Write("?? ");
            }
            Console.WriteLine();

            bool talalt = false;
            int tippek = 0;

            while (!talalt)
            {
                tippek++;
                Console.WriteLine();
                Console.WriteLine($"Add meg a választott {darab} színt (szóközzel elválasztva):");
                string tipp = Console.ReadLine().ToLower();
                string[] szinTipp = tipp.Split(' ');

                if (szinTipp.Length != darab)
                {
                    Console.WriteLine($"Pontosan {darab} színt adj meg!");
                    continue;
                }

                int joHely = 0;
                int joSzin = 0;

                for (int i = 0; i < darab; i++)
                {
                    if (szinTipp[i] == feladvany[i])
                    {
                        joHely++;
                    }
                    else if (szinTipp.Contains(feladvany[i]))
                    {
                        joSzin++;
                    }
                }

                if (joHely == darab)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Gratulálok, kitaláltad a színeket!");
                    Console.WriteLine("-----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Ez {tippek} tippből sikerült!");
                    talalt = true;

                    if (tippek <= 5)
                    {
                        Console.WriteLine();
                        int szivSzam = 6 - tippek;

                        string[] szivForma = 
                        {
                            "  *****   *****  ",
                            " ******* ******* ",
                            " *************** ",
                            "  *************  ",
                            "   ***********   ",
                            "    *********    ",
                            "     *******     ",
                            "      *****      ",
                            "       ***       ",
                            "        *        "
                        };

                            
                        for (int i = 0; i < szivForma.Length; i++)
                        {
                            for (int j = 0; j < szivSzam; j++)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(szivForma[i] + "  ");
                                Console.ResetColor();
                            }
                            Console.WriteLine();
                        }
                    }

                }
                else
                {
                    Console.WriteLine($"Helyes szín, helyes helyen: {joHely}");
                    Console.WriteLine($"Helyes szín, rossz helyen: {joSzin}");
                }
            }
        }
    }
}
