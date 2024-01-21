# My web-forum api.

Welcome to my mock API for a chat forum app!
Developed and designed by myself - Callum Harris

## Table of Contents

- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Database](#database)
- [Authentication](#authentication)
- [License](#license)

## Overview

The project consists of a simple web API developed in ASPNET Core using C#.

- The project makes use of SQLite3 for the database. (May have missed the memo on "out-of-box integration")
- It connects to the DB using linq2db.
- It implements the MVC pattern.
- It contains a data access layer whereby services contact the DB.
- There is DI configured for the project .
- Controllers include JWT authentication.
- User passwords are hashed via SHA512.
- This API is configured to produce swagger documentation. 
- The project contains a Postman collection for testing as well.

## Prerequisites

ASPNET API Project dependencies

- Microsoft.NET.Sdk.Web (Project SDK)
- linq2db (Version: 5.3.2)
- linq2db.AspNet (Version: 5.3.2)
- linq2db.SQLite (Version: 5.3.2)
- Microsoft.AspNetCore.Authentication.JwtBearer (Version: 8.0.1)
- SQLite (Version: 3.13.0)
- Swashbuckle.AspNetCore (Version: 6.4.0)
- System.Data.SQLite.Core (Version: 1.0.118)

Further dependencies

- Postman
- SQLite3
- Git

## Installation

To begin we will install SQLite3
1. visit [SQLite downloads page](https://www.sqlite.org/download.html)
2. download the precompiled binaries for your current operating system.
3. unzip all files to '{PATH environemt variables root}\sqlite', defualt is usually C:\sqlite for windows.
4. go to your system environment variables and include C:\sqlite to the Path variable.
5. test sqlite installed correctly by opening a terminal and typing sqlite3

Install Microsofts SDK [here](https://visualstudio.microsoft.com/downloads/)
- make sure to grab the community edition.
- to configure a development environment when running the installer make sure to modify your installed SDK
    1. Press the modify button on the installer
    2. Under workloads ensure you have the following installed.
    -  ASP.NET and web development

Git installation guide [here](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git)😂

#### Project setup

To begin clone the repository by following the bellow.
```bash
# Clone the repository
git clone https://github.com/CallumJH/text-forum-web-api.git

# Navigate to the project directory
cd text-forum-web-app

# Install dependencies
dotnet restore
```

## Configuration
TODO

## Usage
TODO

## Database
TODO

## Authentication
The authentication process is as follows.

1. Create an account with the signup endpont.
2. When logging in with the credentials used above the response will contain an auth token.
3. In swagger you can use this token in the auth segment prepended with "Bearer {token}"
4. If in postman change the Bearer token value in the environment collection to the new provided token.
<b>⚠Currently there is not token refresh capabilities if your token expires you will need to fire the login again.</b>  

## License
![Alt text](https://m.media-amazon.com/images/I/61GSC7FWWtL._AC_UF894,1000_QL80_.jpg "a title")