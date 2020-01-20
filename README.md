### Database configuration:
\> SQLLocalDb create myDb  
\> SQLLocalDb start myDb  
\> SQLCmd -S "(localdb)\myDb"  
\> CREATE DATABASE [cookbook];  
\> GO

W projekcie: 
1. View > Server Explorer
2. Connect to Database
3. Server name: (localdb)\myDb>
4. Select or enter database name: cookbook
5. Test Connection > OK

Jak nie macie frameworków to:
1. Tools > NuGet Package Manager > Manage NuGet Packages for Solution
2. Szukacie i instalujecie :
- Microsoft.EntityFrameworkCore (v 3.1.0)
- Microsoft.EntityFrameworkCore.Design (v 3.1.0)
- Microsoft.EntityFrameworkCore.SqlServer (v 3.1.0)

Update bazy:  
\> Install-Package Microsoft.EntityFrameworkCore.Tools  
\> Update-Database
