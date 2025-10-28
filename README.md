# 🛍️ E-Commerce API

A powerful **ASP.NET Core Web API** built with **.NET 8**, designed for e-commerce platforms.  
This API includes role-based access for **Admin**, **Suppliers**, and **Users**, along with category management, authentication, and more.

---

## 🚀 Features

- **Role-based authentication** using **JWT tokens**
- **User, Supplier, and Admin** access control
- **Category and Product management**
- **SQL Server (SSMS)** database integration
- **Swagger UI** for API documentation and testing
- **Entity Framework Core** for data access
- **Clean architecture** and RESTful endpoints

---

## 🧱 Tech Stack

| Component | Technology |
|------------|-------------|
| Framework | ASP.NET Core Web API (.NET 8) |
| Authentication | JWT (JSON Web Token) |
| Database | Microsoft SQL Server (SSMS) |
| ORM | Entity Framework Core |
| API Docs | Swagger / Swashbuckle |
| IDE | Visual Studio 2022 |

---

## ⚙️ Setup Instructions

### 1️⃣ Clone the repository
```bash
git clone https://github.com/yourusername/your-repo-name.git
cd your-repo-name
```

### 2️⃣ Configure the database
- Open the project in **Visual Studio**  
- Update your `appsettings.json` with your SQL Server connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ECommerceDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 3️⃣ Apply migrations
```bash
dotnet ef database update
```

### 4️⃣ Run the project
Press **F5** in Visual Studio or run:
```bash
dotnet run
```

The API will be available at:  
➡️ `https://localhost:5001/swagger` (or your configured port)

---

## 🔑 Authentication

The API uses **JWT-based authentication**.  
To access protected endpoints:
1. Log in with valid credentials to receive a token.
2. In Swagger, click **Authorize** and enter your token:
   ```
   Bearer <your_jwt_token>
   ```

---

## 🧪 API Documentation

Swagger UI is enabled by default.  
Access it at:
```
https://localhost:5001/swagger
```

---

## 🧑‍💻 Roles Overview

| Role | Description |
|------|--------------|
| **Admin** | Manage users, suppliers, categories, and products |
| **Supplier** | Manage their own products and categories |
| **User** | Browse and order products |

---

## 📂 Project Structure

```
ECommerceAPI/
├── Controllers/
├── Models/
├── Data/
├── Services/
├── DTOs/
├── appsettings.json
└── Program.cs
```

---

## 🛠️ Future Improvements

- Payment gateway integration  
- Product image uploads  
- Order management & tracking  
- Role-based dashboards  

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---

## 👤 Author

**Anandhu C**  
📧 Email: anandhuc6566@gmail.com  
