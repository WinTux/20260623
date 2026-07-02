## Generar contenedor para SQL Server

```bash
docker pull mcr.microsoft.com/mssql/server:2022-latest

docker run -d --name sqlserver -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=123456ABCxyz' -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest
```

