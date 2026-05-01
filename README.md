# Bank Management System API

An ASP.NET Web API learning project designed to demonstrate fundamental concepts of building RESTful APIs using ASP.NET Core and Entity Framework Core.

## Overview

This project serves as a practical learning resource for understanding ASP.NET Core Web API development. It implements a complete CRUD (Create, Read, Update, Delete) API for a Bank Management System, providing hands-on experience with API controller patterns, DTOs (Data Transfer Objects), Entity Framework Core, and SQL Server database integration.

## What This Project Teaches

By working with this project, you will understand:

- **ASP.NET Core Web API Fundamentals**: Learn how to create and configure RESTful endpoints using minimal APIs and controller-based approaches
- **Entity Framework Core**: Understand ORM (Object-Relational Mapping) concepts, database context configuration, and LINQ queries
- **DTO Pattern**: Learn how to use Data Transfer Objects to separate API contracts from database entities
- **Controller Architecture**: Understand controller naming conventions, routing, and HTTP method mapping
- **RESTful API Design**: Learn proper HTTP verb usage (GET, POST, PUT, DELETE) and status code responses
- **Dependency Injection**: Understand how to inject services (like DbContext) into controllers
- **Soft Delete Pattern**: Implement logical deletion using IsDeleted flag instead of physical deletion

## Project Structure

```
WebApplication3/
|
├── Controllers/
|   ├── Accounts/
|   │   └── AccountsController.cs
|   ├── Branches/
|   │   └── BranchesController.cs
|   ├── Customers/
|   │   └── CustomersController.cs
|   ├── Managers/
|   │   └── ManagersController.cs
|   ├── CustomerAccounts/
|   │   └── CustomerAccountsController.cs
|   └── Transactions/
|       └── TransactionsController.cs
|
├── Data/
|   └── Context/
|       └── BankManagementSystemContext.cs
|
├── Models/
|   ├── Entities/
|   │   ├── Account.cs
|   │   ├── Branch.cs
|   │   ├── Customer.cs
|   │   ├── CustomerAccount.cs
|   │   ├── Manager.cs
|   │   └── Transaction.cs
|   └── Dto/
|       ├── CreateAccountDto.cs
|       ├── UpdateAccountDto.cs
|       ├── CreateBranchDto.cs
|       ├── UpdateBranchDto.cs
|       ├── CreateCustomerDto.cs
|       ├── UpdateCustomerDto.cs
|       ├── CreateManagerDto.cs
|       ├── UpdateManagerDto.cs
|       ├── CreateCustomerAccountDto.cs
|       ├── UpdateCustomerAccountDto.cs
|       ├── CreateTransactionDto.cs
|       └── UpdateTransactionDto.cs
|
├── Program.cs
├── appsettings.json
└── WebApplication3.csproj
```

## How Controllers Work

### Controller Naming Convention

The project follows ASP.NET Core controller naming best practices:

- Each entity has its own folder under `Controllers/`
- The folder name matches the pluralized entity name (e.g., `Accounts`, `Customers`)
- The controller class name is `{EntityName}Controller` (e.g., `AccountsController`)
- The controller inherits from `ControllerBase` and is decorated with `[ApiController]` and `[Route]` attributes

### Controller Structure

Each controller follows a consistent pattern:

```csharp
[ApiController]
[Route("api/[controller]")]
public class EntityNameController : ControllerBase
{
    private readonly BankManagementSystemContext _context;

    public EntityNameController(BankManagementSystemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() { ... }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id) { ... }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDto dto) { ... }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDto dto) { ... }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) { ... }
}
```

### HTTP Method Mapping

| Attribute | HTTP Verb | Purpose |
|-----------|----------|---------|
| `[HttpGet]` | GET | Retrieve data |
| `[HttpGet("{id:guid}")]` | GET | Retrieve specific record |
| `[HttpPost]` | POST | Create new record |
| `[HttpPut("{id:guid}")]` | PUT | Update existing record |
| `[HttpDelete("{id:guid}")]` | DELETE | Delete record (soft delete) |

### Route Mapping in Program.cs

In ASP.NET Core, controllers are mapped in `Program.cs` using the following approach:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<BankManagementSystemContext>(...);

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Map all controllers automatically
app.MapControllers();
```

The `[Route("api/[controller]")]` attribute automatically generates routes based on the controller name:
- `AccountsController` -> `/api/accounts`
- `CustomersController` -> `/api/customers`
- `BranchesController` -> `/api/branches`

## API Endpoints

| Entity | Method | Endpoint | Description |
|--------|--------|----------|-------------|
| Accounts | GET | `/api/accounts` | Get all accounts |
| Accounts | GET | `/api/accounts/{id}` | Get account by ID |
| Accounts | POST | `/api/accounts` | Create new account |
| Accounts | PUT | `/api/accounts/{id}` | Update account |
| Accounts | DELETE | `/api/accounts/{id}` | Delete account |
| Customers | GET | `/api/customers` | Get all customers |
| Customers | GET | `/api/customers/{id}` | Get customer by ID |
| Customers | POST | `/api/customers` | Create new customer |
| Customers | PUT | `/api/customers/{id}` | Update customer |
| Customers | DELETE | `/api/customers/{id}` | Delete customer |
| Branches | GET | `/api/branches` | Get all branches |
| Branches | GET | `/api/branches/{id}` | Get branch by ID |
| Branches | POST | `/api/branches` | Create new branch |
| Branches | PUT | `/api/branches/{id}` | Update branch |
| Branches | DELETE | `/api/branches/{id}` | Delete branch |
| Managers | GET | `/api/managers` | Get all managers |
| Managers | GET | `/api/managers/{id}` | Get manager by ID |
| Managers | POST | `/api/managers` | Create new manager |
| Managers | PUT | `/api/managers/{id}` | Update manager |
| Managers | DELETE | `/api/managers/{id}` | Delete manager |
| CustomerAccounts | GET | `/api/customeraccounts` | Get all customer accounts |
| CustomerAccounts | GET | `/api/customeraccounts/{id}` | Get customer account by ID |
| CustomerAccounts | POST | `/api/customeraccounts` | Create new customer account |
| CustomerAccounts | PUT | `/api/customeraccounts/{id}` | Update customer account |
| CustomerAccounts | DELETE | `/api/customeraccounts/{id}` | Delete customer account |
| Transactions | GET | `/api/transactions` | Get all transactions |
| Transactions | GET | `/api/transactions/{id}` | Get transaction by ID |
| Transactions | POST | `/api/transactions` | Create new transaction |
| Transactions | PUT | `/api/transactions/{id}` | Update transaction |
| Transactions | DELETE | `/api/transactions/{id}` | Delete transaction |

## Postman Collection

Import the Postman collection to test all API endpoints:

[Postman API Collection](https://www.postman.com/ayman-mohamed-1997/workspace/bank-management-system-api/collection](https://web.postman.co/workspace/My-Workspace~296437ab-4ba2-4301-834a-1a45692944ab/folder/39003960-dc963b98-fa0b-4963-acf9-28eafd0d56bd?action=share&source=copy-link&creator=39003960))

## Technologies Used

- ASP.NET Core 11.0
- Entity Framework Core
- SQL Server
- C#

## Author

This project is made by **Engineer Ayman Mohamed**, Full-Stack .NET Engineer.

---
