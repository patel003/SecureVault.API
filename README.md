# SecureVault.API

A secure ASP.NET Core Web API for storing and managing encrypted personal secrets (passwords, notes, vault items) using JWT authentication and role-based authorization.

This project demonstrates real-world backend architecture including authentication, encryption, layered architecture, and secure data handling.



## Features

- User Registration & Login (JWT Authentication)
- Role Based Authorization (Admin / User)
- Full Vault CRUD Operations
- AES Encryption for stored secrets
- Secure Password Hashing
- Clean Architecture Implementation
- DTO Based Request/Response
- Global Exception Handling & Validation
- Protected Endpoints using Authorization Header



## Project Architecture (Clean Architecture)

SecureVault.API            → Controllers / Endpoints
SecureVault.Application    → Services / Interfaces / DTOs
SecureVault.Domain         → Entities / Core Models
SecureVault.Infrastructure → Encryption & JWT Logic
SecureVault.Persistence    → Database & Repository Logic



## Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- AES Encryption
- Clean Architecture



##  Authentication Flow

1. User registers
2. Password stored as hashed value
3. User logs in
4. Server generates JWT Token
5. Token required for all protected APIs

Authorization: Bearer {token}



##  Vault CRUD Operations

Create Secret  
POST /api/vault

Get All Secrets  
GET /api/vault

Get Secret By Id  
GET /api/vault/{id}

Update Secret  
PUT /api/vault/{id}

Delete Secret  
DELETE /api/vault/{id}



##  API Testing (Postman)

All endpoints were tested using Postman.

Steps:
1. Register a new user
2. Login to receive JWT token
3. Copy token
4. Add token in Authorization header
5. Perform full CRUD operations on vault items



##  Security Implementation

- Password hashing before storage
- AES encryption for sensitive data
- JWT token expiration
- Role based endpoint protection
- User can only access own secrets
- Admin can access all records



##  Setup & Run Locally

Clone Repository
git clone https://github.com/patel003/SecureVault.API.git
cd SecureVault.API

Configure Database (appsettings.json)
"ConnectionStrings": {
  "DefaultConnection": "YOUR_SQL_CONNECTION_STRING"
}

Apply Migration
dotnet ef database update

Run Project
dotnet run
---

## API Testing (Postman)

All API endpoints were tested using Postman.

### Testing Flow

1. Register a new user
2. Login to receive JWT token
3. Copy the token from response
4. Add token in Authorization header

Authorization: Bearer {token}

### Tested Operations

Auth:
- Register User
- Login User

Vault:
- Get all vault items
- Add new vault item
- Update vault item
- Delete vault item

The project was fully verified through Postman including authentication and protected routes.



##  Concepts Implemented

- Repository Pattern
- Service Layer Pattern
- DTO Mapping
- JWT Authentication
- Role Based Authorization
- Encryption/Decryption Flow
- Middleware Exception Handling
- Clean Architecture Separation



## Author
Vikas Patel



##  License
This project is created for learning and portfolio purposes.
