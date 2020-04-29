using System;

/*
 * Zadanie 2
 * 
 * Dla drzewa BST(w węźle nie ma referencji do ojca, są tylko referencje do dzieci) napisz metodę zamieniającą węzeł o danym kluczu k z jego rodzicem(wykonujemy obrót).
 * Zakładamy, że klucze są unikalne.
 * Najpierw sprawdzamy czy taki węzeł istnieje, jeśli klucza nie ma w drzewie albo jest to korzeń to nie robimy nic.
 */

namespace Zadanie_2
{
    class Wezel
    {
        public Wezel Lewy;
        public Wezel Prawy;
        public int Wartosc;

        public void WyswietlPreOrder()
        {
            Console.Write($" {Wartosc}");

            if (Lewy != null) Lewy.WyswietlPreOrder();
            if (Prawy != null) Prawy.WyswietlPreOrder();
        }

        public Wezel ZnajdzRodzica(int klucz) // O(logn)
        {
            if(klucz < Wartosc)
            {
                if (Lewy == null) return null;
                if (Lewy.Wartosc == klucz) return this;
                return Lewy.ZnajdzRodzica(klucz);
            }
            
            if (Prawy == null) return null;
            if (Prawy.Wartosc == klucz) return this;
            return Prawy.ZnajdzRodzica(klucz);
        }

        public Wezel(int wartosc)
        {
            Wartosc = wartosc;
        }

        public void Dodaj(int wartosc)
        {
            if(wartosc <= Wartosc)
            {
                if(Lewy == null) Lewy = new Wezel(wartosc);
                else Lewy.Dodaj(wartosc);
            }
            else
            {
                if (Prawy == null) Prawy = new Wezel(wartosc);
                else Prawy.Dodaj(wartosc);
            }
        }
    }

    class Drzewo
    {
        Wezel glowa;

        public void WyswietlPreOrder()
        {
            if(glowa != null) glowa.WyswietlPreOrder();

            Console.WriteLine();
        }

        public void Dodaj(int wartosc)
        {
            if(glowa == null)
                glowa = new Wezel(wartosc);
            else
                glowa.Dodaj(wartosc);
        }

        public void ZamienZOjcem(int klucz) // O(logn), ogólnie zamiana jest O(1) ale znajdowanie rodzica jest O(logn)
        {
            if (glowa == null) return;

            Wezel Rodzic = glowa.ZnajdzRodzica(klucz);

            if (Rodzic == null) return;
            
            // Zależnie czy klucz jest prawym czy lewym dzieckiem mamy inne rotacje
            if(Rodzic.Lewy != null && Rodzic.Lewy.Wartosc == klucz) // Rotacja prawa
            {
                // po zamianie dziadek ma wskazywać na węzeł z kluczem
                Wezel dziadek = glowa.ZnajdzRodzica(Rodzic.Wartosc); 

                Wezel Pom = Rodzic.Lewy;
                Rodzic.Lewy = Pom.Prawy;
                Pom.Prawy = Rodzic;

                // Rodzic = Pom;
                if (dziadek == null) glowa = Pom;
                else
                {
                    if (dziadek.Lewy != null && dziadek.Lewy == Rodzic) dziadek.Lewy = Pom;
                    else dziadek.Prawy = Pom;
                }
            }
            else // Rotacja lewa
            {
                // po zamianie dziadek ma wskazywać na węzeł z kluczem
                Wezel dziadek = glowa.ZnajdzRodzica(Rodzic.Wartosc);

                Wezel Pom = Rodzic.Prawy;
                Rodzic.Prawy = Pom.Lewy;
                Pom.Lewy = Rodzic;

                // Rodzic = Pom;
                if (dziadek == null) glowa = Pom;
                else
                {
                    if (dziadek.Lewy != null && dziadek.Lewy == Rodzic) dziadek.Lewy = Pom;
                    else dziadek.Prawy = Pom;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Drzewo bst = new Drzewo();

            bst.Dodaj(0);

            bst.Dodaj(-2);
            bst.Dodaj(-1);
            bst.Dodaj(-3);

            bst.Dodaj(2);
            bst.Dodaj(1);
            bst.Dodaj(3);
            bst.Dodaj(4);

            /*
             *               0
             *       -2             2
             *    -3    -1      1      3
             *                            4
             */

            Console.WriteLine("Pre-Order drzewa przed zamianą: ");
            bst.WyswietlPreOrder();

            Console.WriteLine();

            bst.ZamienZOjcem(3);

            /*
             *               0
             *       -2             3
             *    -3    -1      2      4
             *               1
             */

            Console.WriteLine("Pre-Order drzewa po zamianie 3 z rodzicem: ");
            bst.WyswietlPreOrder();

            Console.ReadKey();
        }
    }
}
