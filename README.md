# Aplikacja do zarządzania kinem

Aplikacji typu CRUD zbudowana przy pomocy ASP.NET MVC do zarządzania kinem. Cała aplikacja została stworzona z 3 mikroserwisow.
- `ASP.NET MVC`
- `ASP.NET Web API`
- `Console App`
Aplikacja została stworzona w najnowszej wersji SDK, `.NET 6`.

Projekt `ASP.NET MVC` komunikuje się z Web API przez endpointy które API wystawia publicznie.
Inaczej sytuacja ma się w przypadku komunikacji pomiędzy ASP.NET MVC a Console App, tutaj komunikacja odbywa się przez system kolejki, zrealizowany przy użyciu RabbitMQ.
Cała aplikacja została opublikowana jako pliki binarne oraz pliki startujące wszystkie 3 mikroserwisy.

- `API` stworzony przy użyciu `ASP.NET WebAPI` wraz z `Swagger`'em, gdzie możemy zobaczyć wszystkie endpointy które zostały zaimplementowane w API, możemy także wysyłać query oraz commandy z tego poziomu. Aby uderzać do endpointów w API potrzebujemy `API KEY` przekazywanego za każdym razem przez żądanie HTTP. API komunikuje się z bazą danych `Microsoft SQL Server` przy użyciu `ORM`, jednego z najpopularniejszych w .NET mianowicie `Entity Framework Core`.

- `Aplikacja Webowa` stworzony przy użyciu `ASP.NET MVC`, zgodnie z wzorcem MVC, czyli odpowiedzialność podzielona została na 3 warstwy, Model, View oraz Controller. Wszystkie dane dynamiczne, reprezentowane w projekcie pochodzą z API, które zwraca, dodaje, aktualizuje lub usuwa dane przez protokół HTTP, używając odpowiednich czasowników. Widok natomiast jest zrealizowany przy użyciu technologii `Razor Pages`, jest to kod HTML, który implementuje wstawki kodu C#, po odpowiednim oznaczeniu, że w HTML zaczynamy pisać kod C#, oznacza się to przy pomocy @. Ostatnia z ważnych rzeczy zaimplementowanych w webowej aplikacji, to pole do rozsyłania komunikatów o np. Ograniczonej czasowo, ekstra promocji na bilety lub komunikacji o zagrożeniu itp. Która wykorzystuje kolejkowanie `RabbitMQ`.

- Projekt `Console App` jest stworzona, aby odbierać wiadomości z aplikacji webowej, wpisujemy w polu wiadomość i odbieramy je w konsoli.

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
