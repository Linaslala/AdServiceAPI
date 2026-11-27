# AdServiceAPI

Ett REST API för annonser, som på en köp- och säljsida. Byggt med ASP.NET Core Web API (.NET 10, controllers), Entity Framework Core (Code First) och SQL Server.

## Starta projektet

**Du behöver:** .NET 10 SDK och SQL Server på `localhost` (Windows-inloggning).

1. Klona repot och öppna `AdServiceAPI.slnx` i Visual Studio.
2. Tryck **F5**.

Databasen `AdServiceDb` skapas automatiskt vid start (migrationerna körs av `DataInitializer`), och tre exempelannonser läggs in. Swagger UI öppnas direkt på `https://localhost:7252`.

Alla anrop finns också i `AdServiceAPI/AdServiceAPI.http` och kan köras direkt i Visual Studio.

## En annons

| Fält | Typ | Regel |
|---|---|---|
| `id` | int | Sätts av databasen |
| `title` | string | Krävs, 3–50 tecken |
| `product` | string | Krävs, högst 50 tecken |
| `price` | decimal | 0–100 000 |
| `startDate` | date (`ÅÅÅÅ-MM-DD`) | Krävs |
| `endDate` | date (`ÅÅÅÅ-MM-DD`) | Krävs, inte före `startDate` |

## Endpoints

| Metod | Adress | Gör | Statuskoder |
|---|---|---|---|
| GET | `/Ads` | Listar alla annonser | 200 |
| GET | `/Ads/{id}` | Visar en annons | 200, 404 |
| POST | `/Ads` | Skapar en annons | 201, 400 |
| PUT | `/Ads/{id}` | Uppdaterar hela annonsen | 200, 400, 404 |
| PATCH | `/Ads/{id}` | Uppdaterar en del av annonsen | 200, 400, 404 |
| DELETE | `/Ads/{id}` | Tar bort en annons | 204, 404 |

## Validering (VG)

POST, PUT och PATCH kontrollerar datan och svarar med **400 Bad Request** och felmeddelanden när den är ogiltig. Det gäller till exempel för kort titel, negativt pris, datum som saknas eller ett slutdatum före startdatum.

## PATCH (VG)

PATCH följer JSON Patch (RFC 6902). Exempel som ändrar priset:

```json
[
  { "op": "replace", "path": "/price", "value": 500 }
]
```

Patchen läggs på en in-modell (`UpdateAd`) och inte på databasens entitet, så `id` går inte att ändra. Resultatet valideras innan något sparas.

## Struktur

- `Controllers/AdsController.cs`: endpoints och statuskoder
- `Services/`: `IAdService` och `AdService`, som pratar med databasen
- `Ads/`: `Ad` (entiteten), `NewAd` och `UpdateAd` (in-modeller med validering)
- `Data/`: `ApplicationDbContext` och `DataInitializer` (migrering och exempeldata)