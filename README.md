# WordyforestDotnet API

WordyforestDotnet is a web-based vocabulary learning platform backend, designed
using a clean N-Tier architecture. The API enables users to discover new words,
manage custom vocabulary lists, and interact with other users' lists. This
project provides the backend services consumed by a separate React frontend
application.

## Features

- **JWT Authentication**: Secure token-based user authentication and
  authorization
- **Random Word Discovery**: Get new vocabulary words randomly fetched from the
  database
- **Custom Vocabulary Lists**: Create and manage personal vocabulary lists
- **List Subscriptions**: Subscribe to lists shared by other users
- **Word Information**: Access detailed word data:
  - Word type (noun, verb, etc.)
  - Definitions and descriptions
  - Example sentences
  - Synonyms

## Technologies

### Backend

- **ASP.NET Core 8.0** – Modern, cross-platform framework for building APIs
- **Entity Framework Core** – Object-relational mapper (ORM) for PostgreSQL
- **PostgreSQL** – Open-source relational database
- **ASP.NET Core Identity & JWT** – Secure user authentication and role-based
  access control

### Frontend

> ⚠️ The frontend is no longer included in this repository. A separate
> React-based client consumes this API.
> [WordyforesyClient](https://github.com/cuberkam/WordyforestClient.git)

## Architecture

This project uses N-Tier Architecture with clear separation of concerns:

```
Solution
│
├── WordyforestDotnet.Api
│   ├── Controllers → HTTP endpoints
│   ├── Extensions → Middleware, service registration, JWT config
│   ├── appsettings.json → Configuration file
│   └── Program.cs → Entry point
│
├── WordyforestDotnet.BusinessLayer
│   └── Services → Business logic and service interfaces
│
├── WordyforestDotnet.DataAccessLayer
│   ├── Context → DbContext definition
│   ├── Repositories → Repository pattern for data access
│   └── Migrations → EF Core migrations
│
└── WordyforestDotnet.EntityLayer
    ├── Entities → EF Core entity models
    └── DTOs → Data transfer objects
```

## Getting Started

1. Ensure you have the .NET 8.0 SDK installed
2. Configure your PostgreSQL connection string in `appsettings.json`
3. Run database migrations to set up the schema
4. Build and run the application
5. React (optional: frontend not included here)

## License

This project is licensed under the Apache License 2.0 - see the
[LICENSE](LICENSE) file for details.

```
Copyright 2025

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```
