using System;
using System.Collections.Generic;

/*
 * Zadanie 4 poprawa
 * 
 * W krainie Kemo nie wszystkie miasta połączone są poprzez system dróg.
 * System dróg opisany jest przez graf gdzie wierzchołkami są miasta a krawędziami drogi Władze Kemo postanowiły ustalić jaką minimalną liczbę dróg nale2y wybudować aby rozwiązać problem Napisz metodę która rozwiąże zadanie władz Kemo.
 * Wykorzystaj stos, Jako rozwiązanie wypisz listę (możliwie najmniejszą) potrzebnych dróg.
 * Przykładowo dla miast indeksowanych od 1 do 6 i drogami przedstawionymi jako listy sąsiedztwa
 * 
 * L(1) = {6}
 * L(2) = {3, 5}
 * L(3) = {2, 4, 5}
 * L(4) = {3, 5}
 * L(5) = {2, 3, 4}
 * L(6) = {1}
 * 
 * Wystarczy dodać krawędź [1, 2] (albo [1, 3] lub [2, 3] itd.)
*/

namespace Zadanie_4_z_Poprawy
{
    class Program
    {
        static int NieOdwiedzone = 0;
        static int Odwiedzone = 1;
        static int OdwiedzoneWPoprzedniejTurze = 2;

        // Bazuje na zadaniu 3 z K2 gr Z1
        //# <- Główne zmiany
        static void JakieDrogiNalezyDodac(List<int>[] Graf) // O(V + E)
        {
            // Problem sprowadza sie do znalezienia skladowych spojnych grafu

            //# Lista pierwszych miast w kolejnych grupach (składowych spójnych grafu)
            List<int> lista = new List<int>();

            int[] odwiedzenia = new int[Graf.Length];

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

                //# Czy znaleziono pierwszy element
                bool znaleziono = false;

                for (int miasto = 0; miasto < odwiedzenia.Length; miasto++) // O(V)
                {
                    if (odwiedzenia[miasto] == Odwiedzone)
                    {
                        odwiedzenia[miasto] = OdwiedzoneWPoprzedniejTurze;

                        //#
                        if(!znaleziono)
                        {
                            lista.Add(miasto);
                            znaleziono = true;
                        }
                    }
                    else if (odwiedzenia[miasto] == NieOdwiedzone && poczatkowy == -1)
                        poczatkowy = miasto;
                }
            } while (poczatkowy != -1);

            //# Wyświetlam brakujące połączenia - dowolne pary jest ich (lista.Length nad 2) = 1/2*(L-1)*L

            Console.WriteLine($"Należy dodać {(lista.Count - 1) * lista.Count / 2} dróg");

            for (int i = 0; i < lista.Count; i++)
                for (int j = i + 1; j < lista.Count; j++)
                    Console.WriteLine($"[{lista[i] + 1}, {lista[j] + 1}]");
        }

        static void Main(string[] args)
        {
            // Graf w postaci listy wierzchołków
            List<int>[] Miasta = new List<int>[6];

            for (int i = 0; i < Miasta.Length; i++)
                Miasta[i] = new List<int>();

            //# podgraf 1
            Miasta[1].Add(2);
            Miasta[1].Add(4);

            Miasta[2].Add(1);
            Miasta[2].Add(3);
            Miasta[2].Add(4);

            Miasta[3].Add(2);
            Miasta[3].Add(4);

            Miasta[4].Add(1);
            Miasta[4].Add(2);
            Miasta[4].Add(3);

            //# podgraf 2
            Miasta[0].Add(5);
            Miasta[5].Add(0);

            /*#
                //# podgraf 3, zwiększyć wielkość listy do 8
                Miasta[6].Add(7);
                Miasta[7].Add(8);
            */

JakieDrogiNalezyDodac(Miasta);

            Console.ReadKey();
        }
    }
}
