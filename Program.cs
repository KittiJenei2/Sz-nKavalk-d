using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elso
{
    internal class Program
    {
        static void Main(string[] args)
        {

           Random rnd = new Random();
            List<int> lista1 = new List<int>();
            List<int> lista2 = new List<int>();
            
            for(int i = 0; i < 100; i++) {
                lista1.Add(rnd.Next(0, 1001));
            }

            /*
            Console.WriteLine("1. lista: ");
            for (int i = 0; i < lista1.Count; i++)
            {
                Console.WriteLine(lista1[i]);
            }
            */

            for (int i = 0; i < 100; i++)
            {
                int szam = rnd.Next(100, 1001);
                if(szam % 2 == 0)
                {
                    lista2.Add(szam);
                }
            }

            /*
            Console.WriteLine();
            Console.WriteLine("2. lista: ");
            for (int i = 0; i < lista2.Count; i++)
            {
                Console.WriteLine(lista2[i]);
            }
            */

            Console.WriteLine();
            Console.WriteLine("Közös elemek: ");
            for (int i = 0; i < lista1.Count; i++)
            {
                for (int j = 0; j < lista2.Count; j++)
                {
                    if (lista1[i] == lista2[j])
                    {
                        Console.WriteLine(lista1[i]);
                    }
                }
            }

            for (int i = 0; i < lista1.Count; i++)
            {
                for (int j = 0; j < lista1.Count; j++)
                {
                    if (lista1[i] < lista1[j])
                    {
                        var tmp = lista1[i];
                        lista1[i] = lista1[j];
                        lista1[j] = tmp;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("1. lista rendezve: ");
            for (int i = 0; i < lista1.Count; i++)
            {
                Console.WriteLine(lista1[i]);
            }


            for (int i = 0; i < lista2.Count; i++)
            {
                for (int j = 0; j < lista2.Count; j++)
                {
                    if (lista2[i] < lista2[j])
                    {
                        var tmp = lista2[i];
                        lista2[i] = lista2[j];
                        lista2[j] = tmp;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("2. lista rendezve: ");
            for (int i = 0; i < lista2.Count; i++)
            {
                Console.WriteLine(lista2[i]);
            }

        }
    }
}
