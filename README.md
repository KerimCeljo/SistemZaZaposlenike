#SistemZaZaposlenike
Kerim Čeljo

Web aplikacija razvijena u ASP.NET Core MVCC za upravljanje zaposlenicima, odjelima i projektima, uz dodatne funkcionalnosti kao što su konverzija plata i generisanje Excel izvještaja.


#Funkcionalnosti

- CRUD operacije nad zaposlenicima  
- Pregled zaposlenika po odjelima  
- Upravljanje projektima  
- Many-to-Many relacija (Zaposlenici ↔ Projekti) sa dodatnim atributima  
- Konverzija plata u strane valute (EUR, USD, GBP, CHF)  
- Generisanje Excel izvještaja  
- Dashboard sa ključnim metrikama  
- Validacija podataka (DataAnnotations)  
- Moderan UI (Bootstrap + custom CSS)


#Tehnologije

- ASP.NET Core MVC  
- Entity Framework Core  
- SQL Server  
- Bootstrap 5  
- JavaScript  


#Arhitektura

Aplikacija koristi MVC pattern:

-Models – EF Core entiteti  
-ViewModels – validacija i prikaz podataka  
-Controllers – poslovna logika  
-Views – Razor template-i  

Korišteni koncepti:
- Dependency Injection  
- Async/await  
- PRG pattern (Post/Redirect/Get)  
- TempData za poruke korisniku  


#Dashboard

Početna stranica prikazuje:

- Ukupan broj zaposlenika  
- Broj odjela  
- Broj projekata  
- Prosječnu platu  

Uz animirane “counter” komponente za bolji UX.


#Izvještaji

Omogućeno generisanje Excel izvještaja koji sadrži:

- zaposlenike grupisane po odjelima  
- osnovne podatke  
- broj zaposlenika po odjelu  
- prosječnu platu po odjelu  


#UI/UX poboljšanja

- Sticky navigation bar  
- Aktivni linkovi u navigaciji  
- Responsivan dizajn  
- Styled tabele i forme  
- Badge oznake za odjele  
- Dashboard kartice sa statistikama  


#Pokretanje aplikacije

1. Klonirati repozitorij:
"git clone" -> https://github.com/KerimCeljo/SistemZaZaposlenike.git

2. Pokrenuti migracije:
"dotnet ef database update"

3. Pokrenuti aplikaciju