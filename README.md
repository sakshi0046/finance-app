# Finance App

A modern, full-featured financial tracking web application built with **ASP.NET Core MVC** and **Entity Framework Core** (MySQL). It allows users to manage personal finances through accounts, categorized transactions, and detailed reports — all with role-based access and secure authentication.

## 📸 Features

- 🔐 **User Authentication**
  - Secure registration & login using SHA256 password hashing
  - Cookie-based authentication with session support
  - Role-based access (User / Admin)

- 🧑‍💼 **Admin Panel**
  - View, edit, delete users
  - Assign roles

- 🏦 **Accounts Management**
  - Create checking, savings, or custom accounts
  - View balances, edit account info

- 💸 **Transaction Tracking**
  - Deposit, withdraw, transfer funds
  - Categorize transactions
  - View detailed transaction history

- 📊 **Reports**
  - Monthly summary with total deposits, withdrawals, net change
  - Balance overview across accounts
  - Export reports as CSV

- 🧩 **Custom Categories**
  - Create and manage personal transaction categories

## 🧱 Tech Stack

| Layer            | Technology                  |
|------------------|-----------------------------|
| Backend          | ASP.NET Core MVC (v8.0)     |
| ORM & DB         | Entity Framework Core + MySQL (via Pomelo) |
| UI               | Razor Views + Bootstrap     |
| Auth             | Cookie Authentication + Claims |
| Frontend Tools   | jQuery, Bootstrap (via CDN) |

## 📁 Project Structure

```tree
sakshi0046-finance-app/
├── Controllers/       # MVC Controllers
├── Models/            # Entity models and ViewModels
├── Views/             # Razor Pages (UI)
├── Data/              # DbContext and EF Config
├── Helpers/           # Password hashing, etc.
├── Migrations/        # EF Core DB Migrations
├── wwwroot/           # Static files (CSS, JS, libs)
├── appsettings.json   # DB config and logging
└── Program.cs         # App entry point
````

## 🔒 Security Notes

* Passwords are hashed using `SHA256` (see `PasswordHelper.cs`)
* User sessions are stored in `HttpContext.Session`
* Role-based access is enforced on controllers via `[Authorize(Roles = "...")]`
* SQL Injection prevention via EF Core


## 📄 License

This project is licensed under the MIT License.
See [LICENSE](./LICENSE) for details.


## ✨ Author

**Pavan Kalyan Sakshi**
© 2025 – All Rights Reserved.
