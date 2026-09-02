# 📚 Library Management System API

A RESTful Library Management System built with **ASP.NET Core Web API**, **Entity Framework Core**, and **SQL Server**.

The system provides APIs for managing library items, members, borrowing and returning items, as well as generating useful reports and analytics.

---

## 🚀 Technologies Used

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- LINQ
- Dependency Injection
- Swagger / OpenAPI

---

## ✨ Features

### 📚 Library Item Management

The API supports managing different types of library items such as:

- Books
- Magazines
- Newspapers

Each item can contain information such as:

- Title
- Author / Publisher
- Year of Publication
- Availability
- Item Type
- ISBN
- Number of Pages
- Issue Number
- Category
- Publication Date
- Region

Available operations:

- Add a new library item
- Update an existing item
- Delete an item
- List available items
- Search items by title
- Search items by author/publisher
- Search items by publication year
- Filter items by type

---

### 👤 Member Management

Members contain:

- Name
- Email
- Phone Number
- Membership ID
- Role (`Member` or `Admin`)

Available operations:

- Add a member
- Update a member
- Delete a member
- List all members
- Find a member by email
- View borrowing history for a specific member

---

### 🔄 Borrowing & Returning

Members can borrow available library items for a specified duration.

When an item is borrowed:

1. The member is validated.
2. The library item is validated.
3. Item availability is checked.
4. Borrow date is automatically recorded.
5. Due date is calculated automatically.
6. The borrowing record is stored.
7. The item becomes unavailable.

When an item is returned:

1. The borrowing record is located.
2. Return date is recorded.
3. Late days are calculated.
4. A fine is calculated if necessary.
5. The item becomes available again.

The current overdue fine is:

```text
5.00 per late day
```

---

## 📊 Reports & Analytics

The API provides several reporting endpoints:

- Most borrowed item
- Members with the highest number of borrowings
- Top 5 popular items
- Total fines collected
- Number of borrowed items per type
- Fines collected over time

---

# 🔗 API Endpoints

## Library Items

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/LibraryRepo/AddItem` | Add a new library item |
| PUT | `/api/LibraryRepo/UpdateItem/{id}` | Update an existing item |
| DELETE | `/api/LibraryRepo/DeleteItem/{id}` | Delete an item |
| GET | `/api/LibraryRepo/AvailableItems` | Get all available items |
| GET | `/api/LibraryRepo/SearchByYear/{year}` | Search items by publication year |
| GET | `/api/LibraryRepo/SearchByTitle?search=value` | Search items by title |
| GET | `/api/LibraryRepo/SearchByAuthor?search=value` | Search items by author/publisher |
| GET | `/api/LibraryRepo/FilterByType/{type}` | Filter items by type |

---

## Members

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/MemberRepo/ListMember` | Get all members |
| POST | `/api/MemberRepo/AddMember` | Add a new member |
| PUT | `/api/MemberRepo/UpdateMember/{id}` | Update a member |
| DELETE | `/api/MemberRepo/DeleteMember/{id}` | Delete a member |
| GET | `/api/MemberRepo/ShowBorrowingHistory/{memberId}` | Get borrowing history for a member |
| GET | `/api/MemberRepo/GetMemberByEmail/{email}` | Find a member by email |

---

## Borrowings

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/BorrowingRepo/BorrowItem` | Borrow an item |
| PUT | `/api/BorrowingRepo/ReturnItem/{borrowingId}` | Return a borrowed item |
| GET | `/api/BorrowingRepo/ActiveBorrowings` | Get all active borrowings |
| GET | `/api/BorrowingRepo/ActiveBorrowings/{memberId}` | Get active borrowings for a member |

Example borrow request:

```text
POST /api/BorrowingRepo/BorrowItem?memberId=1&itemId=3&durationDays=14
```

---

## Reports

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/ReportRepo/MostBorrowedItem` | Get the most borrowed item |
| GET | `/api/ReportRepo/TotalFines` | Get total collected fines |
| GET | `/api/ReportRepo/MembersWithMostBorrowings` | Get members with the most borrowings |
| GET | `/api/ReportRepo/PopularItems` | Get top 5 popular items |
| GET | `/api/ReportRepo/BorrowedItemsPerType` | Get borrowed item count grouped by type |
| GET | `/api/ReportRepo/FinesOverTime` | Get fines grouped by return date |

---

# 🗄️ Database

The project uses **SQL Server** with three main tables:

### Members

Stores library member information.

```text
Members
├── Id
├── Name
├── Email
├── PhoneNumber
├── MembershipId
└── RoleOfMem
```

### LibraryItems

Stores books, magazines, and newspapers.

```text
LibraryItems
├── Id
├── ItemType
├── Title
├── AuthorPublisher
├── YearOfPublication
├── IsAvailable
├── ISBN
├── NumberOfPages
├── IssueNumber
├── Category
├── PublicationDate
└── Region
```

### Borrowings

Stores borrowing transactions.

```text
Borrowings
├── Id
├── MemberId
├── ItemId
├── BorrowDate
├── DueDate
├── ReturnDate
└── Fine
```

Relationships:

```text
Members
   │
   │ 1
   │
   └──────────< Borrowings >──────────┐
                                      │
                                      │ 1
                                LibraryItems
```

A member can have multiple borrowing records, and a library item can appear in multiple borrowing records over time.

---

# ⚙️ Getting Started

## 1. Clone the repository

```bash
git clone <your-repository-url>
```

Then navigate to the project:

```bash
cd Library-Management-System-API
```

---

## 2. Configure SQL Server

The project currently uses SQL Server LocalDB:

```text
Data Source=(localdb)\ProjectModels
Initial Catalog=project1
Integrated Security=True
TrustServerCertificate=True
```

Make sure the SQL Server instance exists, or update the connection string according to your environment.

---

## 3. Create the database

Create a database named:

```sql
project1
```

Then execute the included:

```text
Create table.sql
```

script to create the required tables and relationships.

---

## 4. Restore dependencies

```bash
dotnet restore
```

---

## 5. Run the API

```bash
dotnet run
```

---

## 6. Open Swagger

When the application is running in Development mode, Swagger can be used to explore and test the API.

Open:

```text
https://localhost:<port>/swagger
```

Swagger provides an interactive interface for testing all available API endpoints.

---

# 📁 Project Structure

```text
Library-Management-System-API
│
├── Create table.sql
│
├── WebApplication1.slnx
│
└── WebApplication1
    │
    ├── Controllers
    │   ├── BorrowingRepo.cs
    │   ├── LibraryRepo.cs
    │   ├── MemberRepo.cs
    │   └── ReportRepo.cs
    │
    ├── Borrowing.cs
    ├── LibraryItems.cs
    ├── Member.cs
    ├── Data.cs
    ├── Program.cs
    └── WebApplication1.csproj
```

