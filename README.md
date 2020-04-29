# Algorytmy i Struktury danych, Egzamin 2019

Rozwiązania do zadań z egzaminu z Algorytmów i Struktur danych
4 zadania z egzaminu głównego oraz 1 z poprawy

## Grupa Z1

Uwaga: We wszystkich zadaniach piszemy komentarze wyjaśniające sposób rozwiązania oraz uzasadniamy złożoność rozwiązania. 

### Zadanie 1
Opracuj i zaimplementuj algorytm naprawiania 4-nego kopca maksymalnego (węzeł ma czworo dzieci o kluczach mniejszych od rodzica).
Napisz dodatkowo metodę budowy takiego kopca.
Zilustruj działanie metod. 

### Zadanie 2
Dla drzewa BST (w węźle nie ma referencji do ojca, są tylko referencje do dzieci) napisz metodę zamieniającą węzeł o danym kluczu k z jego rodzicem (wykonujemy obrót).
Zakładamy, że klucze są unikalne.
Najpierw sprawdzamy czy taki węzeł istnieje, jeśli klucza nie ma w drzewie albo jest to korzeń to nie robimy nic. 

### Zadanie 3
W krainie Asob nie wszystkie miasta połączone są poprzez system dróg.
System dróg opisany jest przez graf gdzie wierzchołkami są miasta a krawędziami drogi.
Władze Asob postanowiły ustalić jak wielki to jest problem i wypisać listę wszystkich par miast pomiędzy którymi nie można przemieszczać się autem.
Napisz metodę która rozwiąże zadanie władz Asob.
Wykorzystaj stos.

### Zadanie 4
W mieście Adu układ ulic można opisać przy pomocy grafu, wierzchołkami są skrzyżowania (i punkty wylotowe na granicy miasta) a krawędziami ulice z wagami — czasem przejazdu pomiędzy wierzchołkami w minutach.
Na skrzyżowaniach usytuowana jest sygnalizacja świetlna.
Skrzyżowanie można opuścić tylko w określonych momentach, jest to wielokrotność f minut (sygnalizacja świetlna w całym mieście działa równocześnie, albo idą piesi, albo jadą auta).
Erdna musi dotrzeć w najkrótszym czasie ze skrzyżowania s do dowolnego skrzyżowania lub punktu wylotowego. Napisz metodę która rozwiąże zadanie Erdny.

<p>
  * Użyłem grafu testowego w postaci siatki 3x3 z rosnącymi po kolei wagami

  ![Graf testowy do zadania 4](/graf-do-zadania-4.png)
</p>

### Zadanie 4 poprawa
W krainie Kemo nie wszystkie miasta połączone są poprzez system dróg.
System dróg opisany jest przez graf gdzie wierzchołkami są miasta a krawędziami drogi Władze Kemo postanowiły ustalić jaką minimalną liczbę dróg należy wybudować aby rozwiązać problem Napisz metodę która rozwiąże zadanie władz Kemo.
Wykorzystaj stos, Jako rozwiązanie wypisz listę (możliwie najmniejszą) potrzebnych dróg.
Przykładowo dla miast indeksowanych od 1 do 6 i drogami przedstawionymi jako listy sąsiedztwa

<p>
  L(1) = { 6 } <br>
  L(2) = { 3, 5 } <br>
  L(3) = { 2, 4, 5 } <br>
  L(4) = { 3, 5 } <br>
  L(5) = { 2, 3, 4 } <br>
  L(6) = { 1 } <br>

  ![Graf do zadania 4 z poprawy](/graf-do-zadania-4-poprawa.png)
</p>

Wystarczy dodać krawędź [1, 2] (albo [1, 3] lub [2, 3] itd.)