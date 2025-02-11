# Travel Journal App
A C# WinForms application for logging, managing, and organizing travel experiences.

## Features
- Log travel details (trip names, locations, dates, ratings, notes)
- Mark favorite trips
- View, edit, and delete trips
- Filter trips by categories
- Beautiful and modern UI using Guna UI

## Technologies Used
- C# WinForms
- SQL Server
- .NET Framework 4.8
- Guna UI (Modern UI Components)

## Database Setup
### Restore from Backup (`.bak`)
1. Download `TravelJournalDB.bak` from the `Database/` folder.
2. Move it to `C:\` (or any preferred location).
3. Open **SQL Server Management Studio (SSMS)**.
4. Right-click **Databases** → **Restore Database**.
5. Select **Device** → **Browse** → Choose `TravelJournalDB.bak`.
6. Click **OK** to restore the database.

### Update the Connection String in `App.config`
- Open `App.config` in the **WinForms UI project**
- Update the connection string:

#### **👉 Using SQL Server Authentication**  
```xml
<connectionStrings>
    <add name="MyDbConnection"
         connectionString="Server=YOUR_SERVER;Database=TravelJournalDB;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
         providerName="System.Data.SqlClient"/>
</connectionStrings>
```
👉 Using Windows Authentication (Recommended)
xml
Copy
Edit
<connectionStrings>
    <add name="MyDbConnection"
         connectionString="Server=.;Database=TravelJournalDB;Integrated Security=True;"
         providerName="System.Data.SqlClient"/>
</connectionStrings>
Now, the database is ready to use!

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/HamzaKherazi/Travel-Journal-App.git
   ```
2. Open the solution file TravelJournal.sln in Visual Studio.

3. Restore the database using the steps above.

4. Build the project (Ctrl + Shift + B).

5. Run the app (F5).
