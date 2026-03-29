# RentItEq - System Wypożyczalni Sprzętu

Aby uruchomić aplikację trzeba

1. Zbudować projekt

```sh
dotnet build
```

2. Uruchomić projekt

```sh
dotnet run
```

Aby stworzyć plik wykonywalny należy wykonać:

```sh
dotnet publish -c Release
```

Plik wykonywalny będzie dostępny w katalogu `bin/Release/net10.0/publish/`

# Krótkie uzasadnienie decyzji projektowych

1. Model danych

- `Device` to klasa abstrakcyjna ze wspólnymi polami (nazwa, numer seryjny, status), a `Laptop`, `Projector` i `Camera` dziedziczą po niej i dokładają swoje pola. Dziedziczenie tutaj ma sens, bo każdy typ sprzętu *jest* urządzeniem z dodatkowymi cechami - to nie jest wymuszone "żeby było obiektowo". Pole `Uuid` jest wyciągnięte do klasy bazowej `BaseEntity`, żeby nie powtarzać go w każdym modelu.
- `Rental` trzyma referencje do `User` i `Device`, daty i opłaty (jako `Money`), dzięki czemu obiekt wypożyczenia ma w sobie wszystko co potrzeba do wyświetlenia i rozliczenia.

2. Typ użytkownika jako enum

- `UserType` to enum (`Student`, `Employee`) zamiast hierarchii klas, bo jedyne co różni typy użytkowników to limit wypożyczeń. Robienie osobnych klas dla jednej liczby nie miałoby sensu. Limity siedzą w `RentalPolicy` jako słownik `UserType -> int` - nowy typ użytkownika to dopisanie jednej linijki.

3. Warstwy projektu

- Projekt jest podzielony na cztery warstwy:
  - modele (`Models/`),
  - logika biznesowa (`Services/`),
  - komendy (`Commands/`)
  - interfejs konsolowy (`Components/`, `UI/`).

`RentalService` nie wie nic o konsoli, `Display` nie wie nic o regułach biznesowych - każda warstwa zajmuje się swoim.

4. Wzorzec Command

- Operacje w menu to osobne klasy z interfejsem `ICommand`. `App` dostaje listę komend i daje użytkownikowi wybrać - nie zna szczegółów żadnej z nich. Nowa funkcjonalność to nowa komenda dorzucona do listy w `Program.cs`, bez ruszania istniejącego kodu.

5. Interfejsy między serwisami

- Serwisy nie zależą od siebie bezpośrednio, tylko przez wąskie interfejsy: `IUserProvider`, `IDeviceStatusUpdater`, `IRentalChecker`. Np. `RentalService` potrzebuje od `DeviceService` tak naprawdę tylko pobrania urządzenia i zmiany statusu, więc zależy od `IDeviceStatusUpdater`, a nie od całej klasy. Każdy serwis widzi tylko te metody, które faktycznie wywołuje.

6. Reguły biznesowe w jednym miejscu

- Stawki i limity (`BaseFeePerDay = 10 PLN`, `LateFeePerDay = 25 PLN`, limity per typ użytkownika) są zebrane w klasie `RentalPolicy`. Zmiana wysokości kary albo limitu to zmiana jednej wartości w jednym miejscu, zamiast szukania po całym projekcie.

7. Abstrakcja konsoli (`IConsole`)

- Wejście/wyjście konsolowe jest za interfejsem `IConsole`. `SystemConsole` to normalna konsola, a `ScriptedConsole` przyjmuje kolejkę gotowych odpowiedzi - do odpalenia scenariusza demo bez klikania. Tę samą abstrakcję można by wykorzystać pod testy.

8. DTO

- `DeviceDto` i pochodne (`LaptopDto`, `ProjectorDto`, `CameraDto`) przenoszą dane z komendy do serwisu. `AddDeviceCommand` zbiera input od użytkownika, pakuje go w DTO i przekazuje dalej. Serwis nie musi wtedy przyjmować kilkunastu luźnych parametrów, a komenda nie tworzy encji domenowych bezpośrednio. Są to rekordy (`record`), bo to tylko pojemniki na dane - nie potrzebują mutowalności ani żadnego zachowania.

9. Fabryka urządzeń

- `DeviceFactory` to statyczna klasa z jedną metodą `Create(DeviceDto)`, która pattern matchingiem tworzy właściwą encję. Jest prosta, bo nic więcej nie jest potrzebne - chodzi tylko o to, żeby tworzenie urządzeń z DTO było w jednym miejscu, a nie rozsiane po serwisie. Nowy typ sprzętu to nowy model, nowe DTO i jeden case w switchu.

10. Repozytorium

- Dostęp do danych jest przez generyczny `IRepository<T>`, zaimplementowany jako `JsonRepository<T>`. Serwisy zależą od interfejsu i nie wiedzą, że pod spodem jest JSON. Podmiana na inny sposób przechowywania (np. baza danych) to napisanie nowej implementacji repozytorium, bez zmian w logice biznesowej.