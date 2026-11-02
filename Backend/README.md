Pokretanje projekta:

Klonirati projekat sa github-a: https://github.com/andjela11/TicketHub

Navigirati se u /Backend folder: "cd Backend"

Kreirati, u Backend folderu, .env fajl i dodati:
SA_PASSWORD=
REDIS_PASSWORD=

Ove promenljive se korste za password za bazu.
Za ovaj projekat potrebno je instalirati .NET 10, kao i Docker ili Docker kroz WSL.
Baze koje se koriste se MSSQL Server i Redis.
Pokretanje projekta:
Najpre izvrsiti komandu "docker compose up" kojom ce se pokrenuti baze a zatim
"dotnet run --launch-profile https"
