Example ASP NET CORE REST API

1. **Please make sure MS SQL servcer is installed**
   - Or use external IP for SQL
     
3. **Add connection string inside "appsettings.json"**

4. {
    "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=WeightManagerDB;Trusted_Connection=True;"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*"
}

**4. Execute EF migrations**

   a) dotnet ef database update
   (Just in case can remove all migrations and add new)

**5. Run API**

   Paginated members endpoint: https://localhost:7157/api/Members/paginated
   All members endpoint: https://localhost:7157/api/Members/
