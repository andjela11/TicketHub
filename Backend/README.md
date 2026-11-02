Pokretanje projekta:

Klonirati projekat sa github-a: https://github.com/andjela11/TicketHub

Navigirati se u /Backend folder: "cd Backend"

kreiraj user secrets za povezivanje na bazu:
dotnet user-secrets init --project src/TicketHub.Api
dotnet user-secrets set "ConnectionStrings:SqlServer" "Server=localhost,1433;Database=master;User Id=sa;Password=...;TrustServerCertificate=True" --project src/TicketHub.Api
dotnet user-secrets set "ConnectionStrings:Redis" "localhost:6379,password=..." --project src/TicketHub.Api

Izvrsiti:
cp .env.example .env

Dodati vrednosti promenljivama:
SA_PASSWORD=
REDIS_PASSWORD=

Ove promenljive se korste za password za bazu.
Za ovaj projekat potrebno je instalirati .NET 10, kao i Docker ili Docker kroz WSL.
Baze koje se koriste se MSSQL Server i Redis.
Pokretanje projekta:
Najpre izvrsiti komandu "docker compose up" kojom ce se pokrenuti baze a zatim
"dotnet run --project src/TicketHub.Api --launch-profile https"
