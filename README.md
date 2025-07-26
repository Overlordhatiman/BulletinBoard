# Bulletin Board Web Application

This project is a full-stack web application for posting and managing classified ads. It consists of an ASP.NET Core Web API backend, an ASP.NET MVC frontend, and uses Microsoft SQL Server for data storage. It also includes Google Identity for authentication.

---

## 🚀 How to Start Working with This Project

Before running the project, ensure you have the following installed:
- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or another C# IDE

### 📂 Step-by-step Setup

1. **Clone the repository**:
   ```bash
   git clone https:/git@github.com:Overlordhatiman/BulletinBoard.git
   cd BulletinBoard
2. Setup the database:

    Open SQL Server Management Studio or any SQL client.

    Run the following scripts in order:

    a. Create database and tables:
     -- InitDb.sql
   b. Insert procedures:
   -- StoredProcedures.sql
3. Update the connection string:

    Open appsettings.json in the Web API project.

    Set the DefaultConnection to your SQL Server instance:
     ``` appsettings.json
     "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=BulletinBoardDb;Trusted_Connection=True;"
    }
4. Run the projects:

    Start the Web API project.

    Start the MVC frontend project.

## Setting Up Google Identity for Authentication

This app uses Google OAuth2 for authentication. To configure this:

   1. Go to the Google Cloud Console.

   2. Click "Select a project" > "New Project" and name your project.

   3. Navigate to "APIs & Services" > "OAuth consent screen":

      - Choose External and click Create.

      - Fill in the required fields (App name, email, etc.)

      - Add scopes like openid, email, and profile.

      - Add test users if needed.

   4. Go to "Credentials":

       - Click "Create Credentials" > "OAuth 2.0 Client IDs".

       - Choose Web Application.

       - Set Authorized redirect URIs:

          -  For example: https://localhost:port/signin-google

       - Save and copy your Client ID and Client Secret.

   5. Update your appsettings.json in the Web API project:

      ``` appsettings.json
        "Authentication": {
            "Google": {
              "ClientId": "YOUR_CLIENT_ID",
              "ClientSecret": "YOUR_CLIENT_SECRET"
            }
          }

   6. Now you can log in using your Google account.



