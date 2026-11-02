Pokretanje projekta:

Za ovaj projekat potrebno j einstalirati .NET 10, kao i Docer ili Docker kroz WSL.
Baze koje se korsite se MSSQL Server i Redis.
Potrebno je dodati .env fajl u Backend folderu (istom folderu gde je i docker-compose.yml).
U .env fajlu dodati:
SA_PASSWORD=
REDIS_PASSWORD=

Ove promenljive se korste za password za bazu
Pokretanje projekta:
Najpre izvrsiti komandu "docker compose up" kojom ce se pokrenuti baze a zatim
"dotnet run --launch-profile https"
