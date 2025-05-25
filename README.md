# 🎓 Student Information System

A web-based student information system built with **C#**, **ASP.NET Core MVC**, and **SQL Server**. This system offers a role-based login experience for students, teachers, and administrators to manage academic workflows such as grades, course details, and announcements.

> 🔒 Authentication and authorization are implemented with **ASP.NET Identity**, and database schema changes are handled via **Entity Framework Core Migrations**.

---

## 📌 Features

### 👨‍🎓 Student Panel
- View personal grades and enrolled courses
- Read announcements
- Edit profile details

### 👩‍🏫 Teacher Panel
- Assign and edit grades
- Publish course announcements
- View student lists

### 🧑‍💼 Admin Panel
- Create and manage student/teacher accounts
- Add/update/delete courses
- Full control over the system database

---

## 🏗️ Tech Stack

- **Framework:** ASP.NET Core MVC
- **Language:** C#
- **Database:** SQL Server (with EF Core Migrations)
- **Frontend:** Razor Views, Bootstrap
- **Authentication:** ASP.NET Identity

---

## 🗂️ Project Structure

```

📁 Controllers         // MVC controllers (Student, Teacher, Admin)
📁 Migrations          // EF Core migrations
📁 Models              // Domain models (Student, Teacher, Course, Grade, etc.)
📁 Views               // Razor view files for each user role
📁 wwwroot             // Static assets (CSS, JS, images)
📄 Program.cs          // Entry point
📄 appsettings.json    // Configuration settings

````

---

## 🚀 Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/your-username/student-information-system.git
````

### 2. Set up the database

* Update your connection string in `appsettings.json`
* Apply migrations:

```bash
dotnet ef database update
```

### 3. Run the project

```bash
dotnet run
```

Access the web app at `https://localhost:5001`

---

## 👥 Authors

* **Mustafa Mansur Yönügül** – [@mustafayngl](https://github.com/mustafayngl)
* **Ahmet Can Ömercikoğlu** – [@canomercik](https://github.com/canomercik)
* **Tuğçe E.** – [@e-tugce](https://github.com/e-tugce)

---

## 📄 License

Apache-2.0 License

---
