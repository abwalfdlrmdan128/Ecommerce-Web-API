# Ecommerce Web API

This project is a backend **E-commerce Web API** built using **ASP.NET Core**, designed to simulate a real-world online shopping system. It provides a complete set of RESTful APIs to handle the core functionalities of an e-commerce platform.

## Features

-  User Authentication & Authorization (JWT)
-  Product Management (Create, Read, Update, Delete)
-  Shopping Cart Functionality
-  Order Processing System
-  Admin Management Features
-  Clean Architecture & Separation of Concerns
-  Dependency Injection

##  Technologies Used

- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- JWT Authentication  
- LINQ  

##  Project Structure

The project is organized using a layered architecture:

- **Controllers** → Handle HTTP requests  
- **Services** → Business logic  
- **Repositories** → Data access layer  
- **Models** → Entities and DTOs  

##  Purpose

This project is built as a practical implementation of a scalable and maintainable e-commerce backend system. It can be used as:

- A learning project for backend development  
- A base structure for real-world applications  
- A portfolio project  

--
# E-Commerce Web API

A backend E-Commerce Web API built with ASP.NET Core 8, SQL Server, and Redis.

## Technologies Used

- ASP.NET Core 8 Web API
- Entity Framework Core
- SQL Server 2022
- Redis
- Docker & Docker Compose

## Prerequisites

Before running the project, make sure you have:

- [Git](https://git-scm.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Getting Started

### 1. Clone the Repository

```bash
[git clone] (https://github.com/abwalfdlrmdan128/Ecommerce-Web-API)
```

### 2. Navigate to the Project Directory

```bash
cd Ecommerce-Web-API/SRC
```

### 3. Configure Environment Variables

Create a `.env` file in the `SRC` directory.

Copy the contents of `.env.example` into `.env` and set your SQL Server password:

```env
SQL_PASSWORD=Your_Strong_Password_Here
```

### 4. Run the Application

Make sure Docker Desktop is running, then execute:

```bash
docker compose up --build -d
```

Docker Compose will automatically:

- Build the API Docker image.
- Pull the SQL Server and Redis images if they are not available locally.
- Create and start all required containers.
- Configure communication between the services.

### 5. Access Swagger

Once the containers are running, open:

http://localhost:5000/swagger

## Useful Docker Commands

Check running containers:

```bash
docker compose ps
```

View application logs:

```bash
docker compose logs -f ecommerce-api
```

Stop containers:

```bash
docker compose stop
```

Start containers again:

```bash
docker compose start
```

Stop and remove containers:

```bash
docker compose down
```

Rebuild after code changes:

```bash
docker compose up --build -d
```

## Services

| Service | Port |
|---|---:|
| Ecommerce API | 5000 |
| SQL Server | 1433 |
| Redis | 6379 |

## Notes

- Never commit your real `.env` file or sensitive credentials.
- The `.env.example` file contains the required environment variable template.
- SQL Server data is persisted using a Docker named volume.
