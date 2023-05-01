# NodeGuard

Treasury management is an essential aspect of operating a Lightning node. However, managing a Lightning node with a significant amount of funds can be challenging and complex when it comes to security mechanisms. Lightning channels require liquidity from a Bitcoin treasury.


We understand the importance of reducing the threat surface as much as possible, following the mantra "not your keys, not your coins." This is why we have developed a way to open lightning channels without granting access to private keys for node operators.

NodeGuard is a treasury management solution for lightning nodes that allows you to manage your Bitcoin treasury without worrying about managing private keys. NodeGuard is a web application written in ASP.NET Core Blazor that provides an intuitive and easy-to-use UI for non-technical users who manage a Bitcoin treasury in lightning nodes.

With NodeGuard, you can rest easy knowing that your on-chain funds are secure, and your lightning channels have the necessary liquidity to operate at scale with proper security mechanisms.


## Benefits of using NodeGuard
* No access to private keys for technical members
* Easy to use and intuitive web application
* Secure management of your Bitcoin treasury
* Reduced risk and increased peace of mind

----------------------
## Features

- Funding and opening of a lightning channel through read-only(no private key access) multisig wallets
- Asynchronous approval process based on Role-based Access Control (RBAC) and multisig wallets.
- Automatic sweeping of funds in lightning nodes to avoid having funds on the node hot wallets
- Channel creation interception with returning address to multisig wallets to avoid having funds on hot wallets
- In-browser notification systems for channel approvals
- Optional remote signing through AWS Lambda functions for channel funding transactions, separating the NodeGuard keys from the actual software
- Two-factor authentication

# Dev environment quickstart

1. Run polar regtest network with Polar, import devnetwork.polar.zip (in the root of this repo) and start it
2. Open FundsManager.sln with Visual Studio or your favourite IDE/EDITOR
3. Set startup project to docker-compose
4. Run

##Requirements

- VS Code / Visual Studio
- Docker desktop
- Dotnet SDK 6+
- Dotnet-ef global tool
- [Polar lightning](https://lightningpolar.com/)
- AWS Lambda function + AWS credentials for the Remote FundsManagerSigner, check [this](#trusted-coordinator-signing)


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


## Developing

### Visual Studio
Launch the FundsManager Docker VS task
Launch The FundsManager VS task

### Rider/IntelliJ
Import and start `devnetwork.polar.zip` in polar
Launch the FundsManager Docker NOVS task
Launch The FundsManager NOVS task

### Visual Studio Code
Import and start `devnetwork.polar.zip` in polar
Start docker compose from terminal (see below)
Then, start the vscode launch configuration `Launch against running docker-compose env (DEV)`
Navigate to http://localhost:38080/

### Starting docker compose from terminal
Start all the dependencies in docker-compose by running:
```bash
cd docker
docker-compose -f docker-compose.dev-novs.yml up -d
```


