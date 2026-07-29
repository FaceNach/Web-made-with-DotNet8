# Web-made-with-DotNet8

## English

### Project Overview
This repository contains an ASP.NET Core MVC web application built with .NET 8 using a layered architecture:

- **BulkyWeb**: presentation layer (MVC + Razor + Identity UI)
- **Bulky.DataAccess**: data access layer (EF Core + repositories + unit of work)
- **Bulky.Models**: domain models and view models
- **Bulky.Utily**: shared utilities (roles constants and email sender stub)

The app includes public customer pages and an admin area for managing categories and products (including image upload).

### Technologies
- **.NET 8 / ASP.NET Core MVC**
- **Entity Framework Core 8**
- **SQL Server** (configured provider in `Program.cs`)
- **ASP.NET Core Identity**
- **Razor Pages / Razor Views**
- **Bootstrap 5**
- **jQuery + jQuery Validation**
- **TinyMCE**
- **Toastr** (notifications in UI)

### Main Features
- Product catalog for customers
- Product details page
- Admin CRUD for categories
- Admin CRUD/upsert for products
- Product image upload to `wwwroot/images/product`
- Role-based authorization for admin routes
- Seed data for categories and products via `ApplicationDbContext`

### Project Structure
```text
Web-made-with-DotNet8/
├── Bulky.sln
├── BulkyWeb/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── Areas/
│   │   ├── Customer/ (HomeController + customer views)
│   │   ├── Admin/ (CategoryController, ProductController + admin views)
│   │   └── Identity/ (scaffolded Identity pages)
│   ├── Views/Shared/ (layout, partials, notifications)
│   └── wwwroot/ (css, js, images, libraries)
├── Bulky.DataAccess/
│   ├── Data/ApplicationDbContext.cs
│   ├── Repository/
│   │   ├── IRepository/ (contracts)
│   │   ├── Repository.cs (generic repository)
│   │   ├── CategoryRepository.cs
│   │   ├── ProductRepository.cs
│   │   └── UnitOfWork.cs
│   └── Migrations/
├── Bulky.Models/
│   ├── Category.cs
│   ├── Product.cs
│   ├── ApplicationUser.cs
│   └── ViewModels/ProductVM.cs
└── Bulky.Utily/
    ├── SD.cs
    └── EmailSender.cs
```

### How to Run
1. Install **.NET 8 SDK**.
2. Update `ConnectionStrings:DefaultConnection` in:
   - `BulkyWeb/appsettings.json`
3. From repository root, run:
   - `dotnet restore`
   - `dotnet build`
   - `dotnet ef database update --project Bulky.DataAccess --startup-project BulkyWeb`
   - `dotnet run --project BulkyWeb`

---

## Español

### Resumen del Proyecto
Este repositorio contiene una aplicación web ASP.NET Core MVC construida con .NET 8 y arquitectura en capas:

- **BulkyWeb**: capa de presentación (MVC + Razor + Identity UI)
- **Bulky.DataAccess**: capa de acceso a datos (EF Core + repositorios + unit of work)
- **Bulky.Models**: modelos de dominio y view models
- **Bulky.Utily**: utilidades compartidas (constantes de roles y sender de email base)

La app incluye páginas públicas para clientes y un área de administración para gestionar categorías y productos (incluyendo carga de imágenes).

### Tecnologías
- **.NET 8 / ASP.NET Core MVC**
- **Entity Framework Core 8**
- **SQL Server** (proveedor configurado en `Program.cs`)
- **ASP.NET Core Identity**
- **Razor Pages / Razor Views**
- **Bootstrap 5**
- **jQuery + jQuery Validation**
- **TinyMCE**
- **Toastr** (notificaciones en UI)

### Funcionalidades Principales
- Catálogo de productos para clientes
- Página de detalle de producto
- CRUD de categorías en área admin
- CRUD/upsert de productos en área admin
- Carga de imágenes de producto en `wwwroot/images/product`
- Autorización por roles para rutas de administración
- Datos semilla de categorías y productos desde `ApplicationDbContext`

### Estructura del Proyecto
```text
Web-made-with-DotNet8/
├── Bulky.sln
├── BulkyWeb/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── Areas/
│   │   ├── Customer/ (HomeController + vistas de cliente)
│   │   ├── Admin/ (CategoryController, ProductController + vistas admin)
│   │   └── Identity/ (páginas Identity scaffolded)
│   ├── Views/Shared/ (layout, parciales, notificaciones)
│   └── wwwroot/ (css, js, imágenes, librerías)
├── Bulky.DataAccess/
│   ├── Data/ApplicationDbContext.cs
│   ├── Repository/
│   │   ├── IRepository/ (contratos)
│   │   ├── Repository.cs (repositorio genérico)
│   │   ├── CategoryRepository.cs
│   │   ├── ProductRepository.cs
│   │   └── UnitOfWork.cs
│   └── Migrations/
├── Bulky.Models/
│   ├── Category.cs
│   ├── Product.cs
│   ├── ApplicationUser.cs
│   └── ViewModels/ProductVM.cs
└── Bulky.Utily/
    ├── SD.cs
    └── EmailSender.cs
```

### Cómo Ejecutarlo
1. Instala el **SDK de .NET 8**.
2. Actualiza `ConnectionStrings:DefaultConnection` en:
   - `BulkyWeb/appsettings.json`
3. Desde la raíz del repositorio ejecuta:
   - `dotnet restore`
   - `dotnet build`
   - `dotnet ef database update --project Bulky.DataAccess --startup-project BulkyWeb`
   - `dotnet run --project BulkyWeb`
