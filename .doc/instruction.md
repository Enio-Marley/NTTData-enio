[Back to README](../README.md)


## 🚀 How to run the project

### Prerequisites

- .NET 8 SDK
- PostgreSQL

1. Clone the repository:
```
git clone https://github.com/Enio-Marley/NTTData-enio.git
```

2. Configure the connection string in
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=SalesDb;Username=postgres;Password=postgres"
}
```

3.  Install the EF CLI and Run the database
```
dotnet tool install --global dotnet-ef
dotnet ef database update
```

## Run the application
```
dotnet run --project src/API
```

## Run Tests
```
dotnet test
```

## Events with Rebus 

When a sale is created, modified, or cancelled, an event is published using Rebus. Events are logged to the console using InMemory transport

## Author
<div style="display: flex;">
<span>Developed by </span> <a href="https://www.linkedin.com/in/enio-marley/"> Enio Marley </a>
</div>

<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./general-api.md">Previous: General API</a>
  <a href="./overview.md">Next: Overview</a>
</div>
