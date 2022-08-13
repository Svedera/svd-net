# Setup

Before first run you should create `.env` file. You can do this by copying
`.env.example` to `.env` and fill data base connection string.

If you run application in development mode - database will be updated automatically.
Otherwise you have to update manually:

```bash
dotnet ef database update --project Svd.Backend.Persistence/Svd.Backend.Persistence.csproj --startup-project Svd.Backend.PostOffice.Api/Svd.Backend.PostOffice.Api.csproj --context Svd.Backend.Persistence.SvdDbContext --configuration Debug 20220816184136_Initial
```

Api description will be available by the link:
[Swagger](https://localhost:7207/swagger/index.html)

## How to run

### Visual Studio

1. Go to root folder
2. Open Svd.Backend.sln
3. Choose Svd.Backend.PostOffice.Api as project to run
4. Press F5

### Command line

```bash
cd <root_folder>/Svd.Backend.PostOffice.Api
dotnet run
```

## How to run tests

### Visual Studio

Via > Test > Run all (Ctrl + R, A)

### Command line

```bash
cd <root_folder>
dotnet test
```

## Formatting

Run:

```bash
cd <root_folder>
dotnet format
```
