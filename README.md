# FundsManager

FundsManager is a web-app to control, audit and manage the governance of a Lightning Network node

## Getting started

1. Open FundsManager.sln with Visual Studio
2. Set startup project to docker-compose
3. Run

## Requirements

- VS Code / Visual Studio
- Docker desktop
- Dotnet SDK 6+
- Dotnet-ef global tool
- Polar

## Test network
- Import network file to Polar from devnetwork.zip (on the root of this repo)

## Migrations

This project uses NPGSQL(postgres) database provider for EfCore (ORM). You need to install dotnet-ef global tool
```
dotnet tool install -g dotnet-ef
```

- To update the database (create it & apply migrations) you shall do:
    ```
    cd src && dotnet ef database update
    ```
- To create a new migration
  ```
  cd src && dotnet ef migrations add changeInEntityExampleAddedNewField // This is an example
  ```
- To remove a non-applied migration (once a migration is applied, you have to drop the database to remove it)
    ```
    cd src && dotnet ef migrations remove
    ```


