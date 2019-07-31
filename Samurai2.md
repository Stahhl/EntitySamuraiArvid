# Samurai2 - Hämta och modifiera samurajer

## Intro

Vi fortsätter med vår samuraiapp. 

Nu ska vi lägga till en console-app och lägga till metoder för att lägga till och  ta bort rader i tabellerna.

## Ny console app

Lägg till ett nytt projekt av typen **Console App (.NET Core)**. 

Döp projektet till **EfSamurai.App**

## 
## AddOneSamurai

Skriv en metod **AddOneSamurai**, som lägger till en Samuraj

    private static void AddOneSamurai()
    {
        var samurai = new Samurai { Name = "Zelda" };
    
        using (var context = new SamuraiContext())
        {
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }
    }

...alternativt utan using:

    private static void AddOneSamurai()
    {
        var samurai = new Samurai { Name = "Zelda" };
    
        var context = new SamuraiContext();
        context.Samurais.Add(samurai);
        context.SaveChanges();
    }



## AddSomeSamurais

Skriv en metod **AddSomeSamurais**, som lägger till flera Samurajer. Använd **AddRange**.


## AddSomeBattles

Skriv en metod **AddSomeBattles** som lägger till ett par Battles. Med tillhörande BattleLog och ett par BattleEvents.


## AddOneSamuraiWithRelatedData

Skriv en metod **AddOneSamuraiWithRelatedData** som skapar en samuraj och fyller hela databasen. Alltså alla tabeller och kolumner ska användas.


## ClearDatabase

Skriv en metod ClearDatabase som rensar databasen på allt innehåll.

Tips: anropa denna metod i början av ditt program så är det lättare att testa.


## ListAllSamuraiNames

Skriv en metod ListAllSamuraiNames som skriver ut namnen på alla samurajer


## ListAllSamuraiNames_OrderByName

Skriv ut namnen och sortera på förnamn


## ListAllSamuraiNames_OrderByIdDescending

Skriv ut namnen och sortera i omvände ordning på Id.


## FindSamuraiWithRealName(string name)

Hitta den samuraj som har ett visst realname (SecretIdentity).

Skriv olika meddelande om samurajen hittades eller ej.


## ListAllQuotesOfType(QuoteStyle quoteStyle)

Lista alla citat av en viss typ


## ListAllQuotesOfType_WithSamurai(QuoteStyle quoteStyle)

Lista alla citat av en viss typ tillsammans med Samurajen som sa det. T.ex:


    'Carpe diem' is a cheesy quote by Kalle
    'Everything happens for a reason' is a cheesy quote by Kalle


## ListAllBattles(DateTime from, DateTime to, bool? isBrutal)

Lista alla slag som pågår inom ett tidsintervall. Och filtrera på **isBrutal** om den är något annat än null.


    'Battle 1' is a brutal battle within the period
    'Battle 2' is not a brutal battle within the period

