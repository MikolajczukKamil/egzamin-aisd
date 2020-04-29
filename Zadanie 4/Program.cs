using System;
using System.Collections.Generic;

/*
 * Zadanie 4
 * 
 * W mieście Adu układ ulic można opisać przy pomocy grafu, wierzchołkami są skrzyżowania (i punkty wylotowe na granicy miasta) a krawędziami ulice z wagami — czasem przejazdu pomiędzy wierzchołkami w minutach.
 * Na skrzyżowaniach usytuowana jest sygnalizacja świetlna.
 * Skrzyżowanie można opuścić tylko w określonych momentach, jest to wielokrotność f minut (sygnalizacja świetlna w całym mieście działa równocześnie, albo idą piesi, albo jadą auta).
 * Erdna musi dotrzeć w najkrótszym czasie ze skrzyżowania s do dowolnego skrzyżowania lub punktu wylotowego. Napisz metodę która rozwiąże zadanie Erdny.
*/

namespace Zadanie_4
{
    class Program
    {
        static void WyswietlSciezki(int[] poprzednie, int[] odleglosci, int start)
        {
            Console.WriteLine($" Ścieżki do kolejnych skrzyżowań zaczynając od {start}");

            for (int nr = 0; nr < odleglosci.Length; nr++)
            {
                Stack<int> stos = new Stack<int>();

                // zapisuje ścieżkę, ale muszę odwrócić kolejność
                int pom = nr;
                
                while (pom != start)
                    stos.Push(pom = poprzednie[pom]);

                Console.Write($" Do {nr} (d = {odleglosci[nr]}): < ");

                foreach (var skrzyzowanie in stos)
                    Console.Write($"{skrzyzowanie}, ");

                Console.WriteLine($"{nr} >");
            }
        }

        static int UwzglednijSwiatlaDlaSamochodow(int tDojazdu, int tPrzejazdu, int f) // O(1)
        {
            int odleglosc = tDojazdu + tPrzejazdu;
            int cykl = odleglosc / f;

            // Parzysty cykl to cykl dla samochodów - samochody mają zielone,  piesi czerwone
            // Nieparzysty cykl to cykl pieszych    - samochody mają czerwone, piesi zielone
            if (cykl % 2 == 0) return odleglosc;    // Jedziemy bo trafiliśmy na zielone
            else return (cykl + 1) * f;             // Czekamy na czerwonym na następny cykl

            // Uproszczona wersja może zwracać
            // (tDojazdu + tPrzejazdu + f - 1) / f * f
            // dopuszcza przejazd na n tej wielokrotności f
        }

        static void SciezkiDoSkrzyzowan(int[,] Graf, int start, int f) // O(V^2)
        {
            if (Graf.GetLength(0) != Graf.GetLength(1)) return;

            // Algorytm Digstry z miasta start
            int ileOdwiedzonych = 0;
            int iloscWierzcholkow = Graf.GetLength(0);

            bool[] odwiedzone = new bool[iloscWierzcholkow];
            int[] poprzednie = new int[iloscWierzcholkow];
            int[] odleglosci = new int[iloscWierzcholkow];

            for (int i = 0; i < iloscWierzcholkow; i++)
                if(i != start) odleglosci[i] = int.MaxValue / 2;

            // póki są nieodwiedzeni
            while (ileOdwiedzonych < iloscWierzcholkow) // O(V*V), sama pętla O(V) w środku druga też O(V)
            {
                // najblizszy nieodwiedzony
                int najblizszy = -1;
                int odleglosc = int.MaxValue;

                for (int i = 0; i < iloscWierzcholkow; i++) // O(V), można zastąpić kopcem wtedy O(logV)
                    if (!odwiedzone[i] && odleglosci[i] < odleglosc)
                    {
                        najblizszy = i;
                        odleglosc = odleglosci[i];
                    }

                // Odwiedzam najbliższego
                ileOdwiedzonych++;
                odwiedzone[najblizszy] = true;

                // Szukam niedwiedzonych sąsiadów odwiedzanego obiektu
                for (int i = 0; i < iloscWierzcholkow; i++)
                    if (!odwiedzone[i] && Graf[najblizszy, i] != 0)
                    {
                        // sprawdzam czy nie bliżej z najbliższym uwzgledniając f
                        int odlegloscDoSasiada = UwzglednijSwiatlaDlaSamochodow(odleglosc, Graf[najblizszy, i], f);

                        if (odlegloscDoSasiada < odleglosci[i])
                        {
                            // ustawiam lepsze rozwiązanie
                            odleglosci[i] = odlegloscDoSasiada;
                            poprzednie[i] = najblizszy;
                        }
                    }
            }

            WyswietlSciezki(poprzednie, odleglosci, start);
        }

        static void Main(string[] args)
        {
            // graf siatka 3 x 3 = 9 skrzyzowan, odległości to kolejne liczby
            // graf w postaci macierzy sąsiedztwa
            int[,] Skrzyzowania = new int[9, 9];
            int d = 1;

            Skrzyzowania[0, 1] = d++;
            Skrzyzowania[1, 2] = d++;

            Skrzyzowania[0, 3] = d++;
            Skrzyzowania[1, 4] = d++;
            Skrzyzowania[2, 5] = d++;

            Skrzyzowania[3, 4] = d++;
            Skrzyzowania[4, 5] = d++;

            Skrzyzowania[3, 6] = d++;
            Skrzyzowania[4, 7] = d++;
            Skrzyzowania[5, 8] = d++;

            Skrzyzowania[6, 7] = d++;
            Skrzyzowania[7, 8] = d++;

            // uzupełniam graf do grafu nieskierowanego
            for (int i = 0; i < Skrzyzowania.GetLength(0); i++)
                for (int j = 0; j < Skrzyzowania.GetLength(1); j++)
                    if (Skrzyzowania[i, j] != 0) Skrzyzowania[j, i] = Skrzyzowania[i, j];

            Console.WriteLine("Graf siatka 3 x 3 = 9 wierzchołków \n");

            SciezkiDoSkrzyzowan(Skrzyzowania, 0, 5);

            // I zaczynając od 8 czyli od drugiej strony
            Console.WriteLine();

            SciezkiDoSkrzyzowan(Skrzyzowania, 8, 5);

            Console.ReadKey();
        }
    }
}
