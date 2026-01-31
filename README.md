# Inventory Management Backend

A backend standalone peoject built for learning and practicing backend development with ASP.NET Core.

## Overview

This project provides CRUD APIs for managing inventory-related entities such as products, categories, suppliers, warehouses and stock movements.
It is designed as a backend-only system with no frontend, focusing purely on application structure and data flow.

## Purpose
This project was built to:
- Practice system design and clean architecture
- Understand entity relationships
- Learn about Unit of Work pattern
- Implement CRUD operations across multiple related tables

## Tech Stack
- ASP.NET Core (C#)
- Relational Database (SQL Server)
- ORM (EF Core)

## Domain Entities
- Product
- Category
- Supplier
- Warehouse
- StockMovement
- WarehouseProduct

## Features
- Full CRUD operations for all domain entities
- Clean Architecture-based project structure
- Database persistence using EF Core
- RESTful API design

## Architecture

This project follows 'Clean Architecture', with clear separation between:
- Domain Layer: Core business entites and rules
- Application layer: Use cases and business logic
- Infrastructure layer: Database access and technical implementations
- API layer: REST Controllers/API endpoints

## API Scope

This project does not include:
- Authentication or authorization
- Frontend or UI
- Role-based access control

It is intended purely as a backend learning project.

## Notes
- Built for educational purposes
- Not production-ready
- Can be extended with authentication and frontend support in the future
