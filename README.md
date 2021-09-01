# StoreManagementApplication

### An Asp.Net web forms appplication that helps users manage various stores and track the total number of products in the stores.

### Authentication in this application was configured using forms authentication by providing a custom implementation for the memebership provider class.

### This app contains support for all CRUD (creating, reading, updating and deleting) operations on stores. sStore owners can login to manage their stores but anonymous users can view all the stores in the appication and the details of all stores.

### Image upload is also supported and images are stored in Cloudinary.

### Error logging was configured using NLog to enable ease in debugging.

### The architecture used in this project is the 3-tier architecture as a result of the size of the application.

### Dummy data for users were seeded into this application, as such you could login to the application if you have it running with any of the following:

#### "UserName": "dwoodrooffe0", "Password": "wTemF1YI3x"

#### "UserName": "efourmy4", "Password": "MkK4AsqDa8v",

### "UserName": "vhaffenden1", "Password": "flH35aR"

## To have this app running on your local machine please follow these steps:

## NB: You must have the .Net sdk along side SqlServer and an SqLServer Management plugin such as SSMS installed locally on your machine.

#### You can download these here: [.NetSdk](https://dotnet.microsoft.com/download/visual-studio-sdks),

#### [SqlServer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### If you have git installed your local machine Navigate to any choosen folder in your terminal and run

##### git clone https://github.com/lexTutor/StoreManagementApplication.git

### OR

### Navigate tho the mid right of this git hub page and click on the drop down part of the code button and then click on

#### Download Zip

### Open the folder in your local machine

### Navigate to the StoreApp.DataAccess folder you will find a file Queries.sql, open it with your prefered SQL Server management plugin and run those queries.

### Next navigate to the StoreApp.NetFramework folder/project.

### Open the Web.config file you will find a section on top that looks like this

     <connectionStrings>

    <add name="DefaultConnection" connectionString="Data Source= .;Initial Catalog=StoreApp;Integrated Security=True" />

     </connectionStrings>

### Change the DataSource to your local SQLServer Data source.

### Finally, while still in this directory, open your terminal and enter dotnet run.

#### The application should start and display this landing page.
