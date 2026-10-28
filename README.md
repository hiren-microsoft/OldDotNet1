# Legacy .NET Orders

Order processing sample app targeting **.NET 11 RC2** (`net11.0`) with an
SDK-style project. This is a prerelease framework, not a stable LTS release.

## Build

Install the .NET SDK specified in [global.json](global.json)
(`11.0.100-rc.2.26504.105`, or a newer patch in the same feature band), then run:

```powershell
dotnet build .\LegacyOrderProcessor.sln --configuration Release
```

Running the application requires a reachable SQL Server with a
`ProcessedOrders` table and suitable configuration in
[App.config](LegacyOrderProcessor/App.config). From the repository root, run:

```powershell
dotnet run --project .\LegacyOrderProcessor\LegacyOrderProcessor.csproj -- .\LegacyOrderProcessor\orders.csv
```

## Behavior
- Loads `orders.csv`
- Applies discount rules from `App.config`
- Persists results to SQL Server using `System.Data.SqlClient`
