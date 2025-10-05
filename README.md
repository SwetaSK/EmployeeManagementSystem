# Employee Management System (ASP.NET MVC + ADO.NET)

A simple **Employee Management System** built using **ASP.NET MVC**, **ADO.NET**, **SQL Server**, and **Bootstrap**.  
Implements **CRUD operations**, **Login Authentication**, and **Role-Based Access Control (RBAC)** for Admin and User.

---

## Features
- Login with Admin/User roles  
- Admin: Add, Edit, Delete Employees  
- User: View-only access  
- Bootstrap 5 responsive design  
- Secure session handling  
- SQL Server database integration using ADO.NET  

---

## Database Setup
1. Go to the `/Database` folder.
2. Open `Create_EmployeeDB.sql` in SQL Server Management Studio (SSMS).
3. Execute the script to create tables.

---

## Roles
| Role | Access |
|------|---------|
| Admin | Full CRUD |
| User  | View only |

---

## Tech Stack
- ASP.NET MVC
- ADO.NET
- C#
- SQL Server
- Bootstrap 5
- HTML, CSS

----

##  How to Run
1. Clone or download the repository.
2. Open `EmployeeManagementSystem.sln` in Visual Studio.
3. Restore NuGet packages (if prompted).
4. Open `/Database/Create_EmployeeDB.sql` in SQL Server Management Studio and execute it.
5. Update the connection string in `Web.config` with your SQL Server name.
6. Run the project (Ctrl + F5) — the login page will open.

