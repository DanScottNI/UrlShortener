# URL Short Form application
This application generates short-form URLs from longer URLs.

To run it, you''ll need the following installed:

* .NET 5.0
* A recent version of SQL Server
* A database called 'UrlBitlyClone' created on the SQL server.

## Getting Started
You can run this probably by opening a command prompt, navigating to the UrlBitlyClone directory and running dotnet run.

Opening it in Visual Studio, and running the web application from there will also work.

Currently, the application is set to attempt to execute database migrations when you hit a page. This can be disabled through the appsettings (see below).

There''s an API available for creating URLs as well. When running this locally, a Swagger page is available at /swagger.

## Testing 
The tests for this application are contained within the UrlBitlyClone.Tests project. These can be run using dotnet test or through Visual Studio.

## Configuration

The appsettings.json file can take a number of settings:

* BaseUrl - The URL that is used for the base URL for generated links.
* StringHashType - The type of hashing to be used for the URLs.  There are only really two at present, TruncatedMd5 and None. TruncatedMd5 is basically a MD5 hash is made up of the URL plus a random seed value, and the first 8 characters of the hash are used. None basically takes the URL as-is.
* UseInProcessMigrations - Whether or not you want to use in-process migration runner or an external one. Defaults to True (for using the in-process one).

## Possible improvements

The application could have the following improvements:

* Async - it''s all synchronous right now.
* Caching - This application could use caching through something like Redis for storing the generated URLs.
* Application could dog-food the API.
* Authentication
* Rate limiting on create calls.