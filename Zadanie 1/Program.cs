using System;

/*
 * Zadanie 1
 * 
 * Opracuj i zaimplementuj algorytm naprawiania 4-nego kopca maksymalnego (węzeł ma czworo dzieci o kluczach mniejszych od rodzica).
 * Napisz dodatkowo metodę budowy takiego kopca.
 * Zilustruj działanie metod. 
 */

namespace Zadanie_1
{
    class Program
    {
        static void Zamien(int[] tab, int i, int j)
        {
            int tmp = tab[i];
            tab[i] = tab[j];
            tab[j] = tmp;
        }

        static void Napraw(int[] Kopiec, int rodzic) // O(logn), średnio
        {
            // Na początku za największy uznaję rodzica
            int najwiekszaWartosc = Kopiec[rodzic];
            int indexNajwiekszejWartosci = rodzic;

            // Szukam największej wartości w zbiorze wierzchołka i jego dzieci
            for (int i = 1; i <= 4; i++) // O(1)
            {
                int dziecko = 4 * rodzic + i;

                if (dziecko < Kopiec.Length && Kopiec[dziecko] > najwiekszaWartosc)
                {
                    najwiekszaWartosc = Kopiec[dziecko];
                    indexNajwiekszejWartosci = dziecko;
                }
            }

            if(indexNajwiekszejWartosci != rodzic)
            {
                // Jeżeli największa wartość nie jest ropdzicem
                // to największe dziecko awansuję na rodzica
                Zamien(Kopiec, rodzic, indexNajwiekszejWartosci);

                // i sprawdzam co się dzieje u nowego dziecka czy też tam nie potrzeba zmian
                Napraw(Kopiec, indexNajwiekszejWartosci);
            }
        }

        static void Buduj(int[] Kopiec) // O(n*logn)
        {
            int ostatniRodzic = (Kopiec.Length + 2) / 4 - 1;

            // zaczynam od ostatniego rodzica i naprawiam kolejnych rodziców
            for (int i = ostatniRodzic; i >= 0; i--) // O(n)
                Napraw(Kopiec, i); // O(logn)
        }

        static void Main(string[] args)
        {
            int[] Kopiec = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            Buduj(Kopiec);


            Console.WriteLine("Korzeń potem jego dzieci i ich dzieci czyli wnuki \n");

            Console.WriteLine($"{Kopiec[0]}");

            for (int dziecko = 1; dziecko <= 4; dziecko++)
            {
                Console.Write($" {Kopiec[dziecko]}: ");

                for (int i = 1; i <= 4; i++)
                {
                    int wnuczek = 4 * dziecko + i;

                    if (wnuczek < Kopiec.Length)
                        Console.Write($" {Kopiec[wnuczek]}");
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
