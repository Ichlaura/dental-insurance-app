# Task It

A simple, user-friendly task management app built with Blazor and ASP.NET Core Identity.

**Team:** Joshua Mostert, Cherry Wilmer Machado Carreno

---

## Tech Stack

- **Framework:** ASP.NET Core / Blazor Server (.NET 10)
- **Auth:** ASP.NET Core Identity (cookie-based)
- **Database:** SQLite (via Entity Framework Core)
- **Styling:** Bootstrap 5

---

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [dotnet-ef CLI tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

Install the EF CLI tool if you don't have it:
```
dotnet tool install --global dotnet-ef
```

---

### Setup

1. **Clone the repo**
   ```
   git clone <repo-url>
   cd try-catch/Try-Catch-Masters-Project
   ```

2. **Restore dependencies**
   ```
   dotnet restore
   ```

3. **Create your local dev settings**  
   Create a file called `appsettings.Development.json` in the project root with:
   ```json
   {
     "DetailedErrors": true,
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     }
   }
   ```

4. **Apply database migrations**  
   This creates your local `app.db` SQLite database with all the required tables.
   ```
   dotnet ef database update
   ```

5. **Run the app**
   ```
   dotnet watch
   ```
   The app will be available at `https://localhost:5001` (or the port shown in the terminal).

---

## Notes
- Never commit secrets or connection strings with passwords.
- Each developer has their own local SQLite database — run migrations after pulling new changes.
- If a teammate adds a new migration, pull and run `dotnet ef database update` to stay in sync.
