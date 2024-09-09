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
			List<string> alapSzinek = new List<string> { "piros", "kék", "sárga", "zöld" };

			Random rnd = new Random();
			List<string> feladvany = new List<string>();
			while(feladvany.Count < 3)
			{
				string color = alapSzinek[rnd.Next(alapSzinek.Count)];
				if(!feladvany.Contains(color))
				{
					feladvany.Add(color);
				}
			}

            Console.WriteLine("Találd ki a 3 színt a helyes sorrendben!");
			Console.WriteLine("Választható színek: piros, kék, zöld, sárga");
            Console.WriteLine("??   ??   ??");
            Console.WriteLine();

            bool talalt = false;
			while(!talalt)
			{
                Console.WriteLine();
                Console.WriteLine("Add meg a választott 3 színt (szóközzel elválasztva):");
				string tipp = Console.ReadLine().ToLower();
				string[] szinTipp = tipp.Split(' ');

				if(szinTipp.Length != 3)
				{
					Console.WriteLine("Pontosan 3 színt adj meg!");
					continue;
				}

				int joHely = 0;
				int joSzin = 0;

				for(int i = 0; i < 3; i++)
				{
					if(szinTipp[i] == feladvany[i])
					{
						joHely++;
					}
					else if(szinTipp.Contains(feladvany[i]))
					{
						joSzin++;
					}
				}

				if(joHely == 3)
				{
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Gratulálok, kitaláltad a színeket!");
					talalt = true;
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
