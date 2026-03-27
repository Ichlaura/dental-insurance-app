  Joshua Mostert  

  Cherry Wilmer Machado Carreno

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

3. **Apply database migrations**  
   This creates your local `app.db` SQLite database with all the required tables.
   ```
   dotnet ef database update
   ```

4. **Run the app**
   ```
   dotnet watch
   ```
   The app will be available at `https://localhost:5001` (or the port shown in the terminal).

---

### Notes
- `appsettings.Development.json` is excluded from Git — each developer has their own local copy.
- The SQLite database (`app.db`) is also excluded — run `dotnet ef database update` to generate it locally.
- Never commit secrets or connection strings with passwords to the repo.
