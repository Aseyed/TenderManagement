# Tender Management API – .NET C#

* it's an interview project. you can read the project requirements in this file

## Backend Evaluation Project

### Overview

You are tasked with developing a Tender Management API for a company that oversees the publication of tenders and collection of vendor bids. Vendors can register and submit bids to open tenders, and administrators can review and update the bid statuses.

This project evaluates your skills in:
- .NET backend development
- Database modeling
- EF Core + Dapper integration
- Software architecture and best practices

---

### Objectives

You are expected to:

1. Design a normalized SQL Server schema (you define the table structures).
2. Use Entity Framework Core (EF Core) for write operations (POST, PUT, DELETE).
3. Use Dapper for read operations, especially where joins are involved.
4. Build a RESTful API using .NET (C#).
5. Implement JWT-based authentication for secure access.
6. Deploy the API to IIS.
7. Provide a Postman collection demonstrating all endpoints.

---

### Requirements and Expectations

Your implementation should:

- Follow RESTful conventions.
- Implement proper error handling (e.g., 400, 404, 500).
- Include input validation (e.g., required fields).
- Use `Status` as a reference table (not inline text or enums).
- Return related data using nested object models (e.g., Tender includes an array of Bids).
- Use appropriate HTTP status codes and messages.
- Use async/await throughout.
- Ensure clear separation of concerns (e.g., services, repositories, controllers).

---

### Authentication

Implement JWT-based authentication.

Endpoints:

- `POST /api/auth/register`: Register new user (username, password, role)
- `POST /api/auth/login`: Authenticate user and return JWT token

Users should have a role:
- `Admin`
- `Vendor`

Protected endpoints:
- Only **Admins** can:
  - Approve or reject bids
  - Create, update, or delete tenders

---

### Entities to Model

You are expected to design and implement the following entities:

- **User** – For authentication (including role)
- **Tender** – Projects open for bidding
- **Category** – Tender classification
- **Vendor** – A company that submits bids
- **Bid** – A vendor’s proposal on a tender
- **Status** – Reusable status values (used for both Tender and Bid)

---

### API Endpoints

#### Authentication

- `POST /api/auth/register`  
  Register a new user (username, password, role)

- `POST /api/auth/login`  
  Authenticate and return a JWT token

#### Tenders

- `GET /api/tenders`  
  Return a list of tenders  
  Includes: Id, Title, Description, Deadline, Category (object), Status (object)

- `GET /api/tenders/{id}`  
  Return tender details  
  Includes: Category, Status, Bids (Bid Id, Amount, Submission Date, Vendor, Status)

- `POST /api/tenders`  
  Create a new tender (Admin only)  
  Requires: Title, Description, Deadline, CategoryId, StatusId

- `PUT /api/tenders/{id}`  
  Update an existing tender (Admin only)

- `DELETE /api/tenders/{id}`  
  Delete a tender (Admin only)

#### Vendors

- `GET /api/vendors`  
  List all vendors  
  Optional: include summary of bids

- `GET /api/vendors/{id}`  
  Show vendor details including bids

- `POST /api/vendors`  
  Create a new vendor

#### Bids

- `POST /api/bids`  
  Submit a new bid  
  Requires: TenderId, VendorId, BidAmount, Comments  
  Status should default to `Pending`

- `PUT /api/bids/{id}/status`  
  Update bid status (Admin only)  
  Requires: StatusId

#### Lookups

- `GET /api/categories`  
  List of tender categories

- `GET /api/statuses`  
  List of all status values (e.g., Open, Closed, Pending)

---

### Technology Guidelines

- **EF Core** for create/update/delete
- **Dapper** for read operations (especially joins/aggregations)
- **SQL Server** as the database engine
- **JWT** for authentication
- Use `appsettings.json` for configuration (JWT secret, token expiration)

---

### Deployment

Deploy the API on **IIS**.

Include:
- Deployment instructions
- Authentication configuration notes

---

### Postman Requirements

Provide a Postman collection with:

- All endpoints
- Sample requests and responses
- Environment support (e.g., base URL, JWT token)
- README for authentication and using secured routes

---

### Deliverables

1. Source code (GitHub link or ZIP)
2. Database backup file (.bak)
3. Postman collection
4. Deployment instructions (IIS)
5. README file (this one)

---

### Evaluation Criteria

- Correct and complete database schema
- Clear separation of EF and Dapper responsibilities
- Code structure and maintainability
- Proper JWT authentication and role-based authorization
- RESTful API design and correct use of HTTP methods
- Robust error handling and validation
- Usability of the API (via Postman)
- Successful deployment on IIS

