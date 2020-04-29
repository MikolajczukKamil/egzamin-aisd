using System;
using System.Collections.Generic;

/*
 * Zadanie 3
 * 
 * W krainie Asob nie wszystkie miasta połączone są poprzez system dróg.
 * System dróg opisany jest przez graf gdzie wierzchołkami są miasta a krawędziami drogi.
 * Władze Asob postanowiły ustalić jak wielki to jest problem i wypisać listę wszystkich par miast pomiędzy którymi nie można przemieszczać się autem.
 * Napisz metodę która rozwiąże zadanie władz Asob.
 * Wykorzystaj stos.
 */

namespace Zadanie_3
{
    class Program
    {
        static int NieOdwiedzone = 0;
        static int Odwiedzone = 1;
        static int OdwiedzoneWPoprzedniejTurze = 2;

        static void GrupyWMiascie(List<int>[] Graf) // O(V + E)
        {
            // Problem sprowadza sie do znalezienia skladowych spojnych grafu

            int[] odwiedzenia = new int[Graf.Length];
            int grupa = 1;

            // Wierzchołek od którego zaczynam wyznaczanie składowej grafu
            int poczatkowy = 0;

            do
            {
                // DFS, O(V + E)
                Stack<int> stos = new Stack<int>();
                stos.Push(poczatkowy);

                while (stos.Count > 0)
                {
                    int wierzcholek = stos.Pop();

                    if (odwiedzenia[wierzcholek] != NieOdwiedzone) continue;

                    odwiedzenia[wierzcholek] = Odwiedzone;

                    foreach (var sasiad in Graf[wierzcholek])
                        if (odwiedzenia[sasiad] == NieOdwiedzone)
                            stos.Push(sasiad);
                }

                poczatkowy = -1;

                Console.WriteLine($"Grupa {grupa}");

                for (int miasto = 0; miasto < odwiedzenia.Length; miasto++) // O(V)
                {
                    if(odwiedzenia[miasto] == Odwiedzone)
                    {
                        odwiedzenia[miasto] = OdwiedzoneWPoprzedniejTurze;

                        Console.Write($" Miasto {miasto} ");
                    }
                    else if(odwiedzenia[miasto] == NieOdwiedzone && poczatkowy == -1)
                        poczatkowy = miasto;
                }

                Console.WriteLine('\n');

                grupa++;
            } while (poczatkowy != -1);
        }

        static void Main(string[] args)
        {
            // Graf w postaci listy wierzchołków
            List<int>[] Miasta = new List<int>[6];

            for (int i = 0; i < Miasta.Length; i++)
                Miasta[i] = new List<int>();

            // podgraf 1
            Miasta[0].Add(1);
            Miasta[1].Add(0);

            // podgraf 2
            Miasta[2].Add(3);
            Miasta[3].Add(2);

            // podgraf 3
            Miasta[4].Add(5);
            Miasta[5].Add(4);

            // W przygotowanym grafie są 3 grupy - podgrafy
            // każda grupa to po prostu para kolejnych wierzchołków

            Console.WriteLine($"Odseparowane grupy w mieście Asob \n");
            GrupyWMiascie(Miasta);

            Console.ReadKey();
        }
    }
}
