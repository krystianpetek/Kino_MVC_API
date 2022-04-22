# Aplikacja do zarządzania kinem

Aplikacji typu CRUD zbudowana przy pomocy ASP.NET MVC do zarządzania kinem.

## Funkcjonalności
Funkcje klienta: 
- przeglądanie aktualnego repertuaru kinowego
- przeglądanie emisji filmów, przeszłych a także tych, które się nie odbyły
- ma możliwość zarejestrowania się oraz zalogowania
- po zalogowaniu użytkownik może przeglądać jego wszystkie rezerwacje w kinie
- rezerwować kolejne bilety na przyszłe filmy i usuwać rezerwacje biletów tylko z tych emisji, które jeszcze się nie odbyły

Funkcje pracownik:
- przeglądanie klientów
- przeglądanie i edytowanie sal kinowych oraz reperturu
- wykonywać wszystkie operacje na emisjach filmu
- przeglądać rezerwacje biletów, oraz ma wszystkie możliwości klienta

Funkcje administratora:
- dodawanie, modyfikacja czy usuwanie:
  - sal
  - filmów
  - klientów
  - emisję filmów i rezerwacje biletów

## Aby uruchomic aplikacje w Visual Studio, należy: 
- w Console Package Manager należy wybrać `Default Project` jako `ProjektAPI`
- w konsoli wywołać polecenie `update-database`
- PPM na Solution -> Properties -> Project -> Multiple startup projects 
- `ProjektAPI` wybieramy Start 
- `ProjektMVC` wybieramy Start
- Uruchom `F5`

## API
W aplikacji zostało zaimplementowane `Rest API` wraz z autoryzacją `API Key`

Gałęzie API `https://localhost:44385/`:
- Sale kinowe - `api/Sala/`
- Filmy - `api/Film/`
- Klienci - `api/Klient/`
- Emisja filmów - `api/Emisja/`
- Rezerwacja biletow - `api/Rezerwacja/`

### Przyklad użycia aby zobaczyć emisje filmów:

|Ścieżka      | Metoda  |  Opis | 
|-------------|:-------:|-------|
|api/Emisja/  | GET     |Zwraca listę emisji filmów|
|api/Emisja/1 | GET     |Zwraca konkretną emisje filmu po polu Id|
|api/Emisja/  | POST    |Dodawanie konkretnej emisji filmu, autoinkrementacja pola Id|
|api/Emisja/1 | PUT     |Aktualizacja konkretnej emisji filmu po polu Id|
|api/Emisja/1 | DELETE  |Usuwanie konkretnej emisji filmu po polu Id|
