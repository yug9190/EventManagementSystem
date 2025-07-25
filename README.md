# Event Management System in .Net Core MVC

This is an Event Management System built in Asp .Net Core 8.0 and PostgreSQL.

# Table of Contents

- [Introduction](#introduction)
- [Models](#models)
- [Controllers](#controllers)
- [Views](#views)
- [Installation](#installation)
- [Security](#security)
- [Conclusion](#conclusion)

---

## Introduction
This document provides a comprehensive overview of the Event Management System built in Asp .Net Core MVC. The system allows users to manage events and categories, with authentication implemented to ensure data security.

## Models
### AppUser
- Inherits from `IdentityUser`.
- Additional properties: Name, Address.

### Event
- Represents an event.
- Properties include: EventID, EventName, Category, StartDate, EndDate, Venue, Status, BookingUserName.

### Category
- Represents a category for events.
- Properties include: CategoryID, CategoryName, Description, PricePerHour, PricePerDay, IsActive, MaxCapacity.

## Controllers
- **AccountController**: Manages user authentication, including login, register, and logout functionalities.
- **EventsController**: Handles CRUD operations for events.
- **CategoriesController**: Handles CRUD operations for categories.

# 🎫 Real-Time Web-Based Event Management System

A modern event management platform developed using **ASP.NET Core MVC**, **SignalR**, and **Entity Framework Core**. This system allows admins, organizers, and attendees to manage and participate in events with real-time features and a responsive UI.

---
<img width="975" height="512" alt="image" src="https://github.com/user-attachments/assets/a4f37dab-7ea1-41ee-95d4-0f13121f78ce" />
<img width="975" height="512" alt="image" src="https://github.com/user-attachments/assets/f0c3ae4d-21de-4409-9dfb-f0e2d31a71cf" />

<img width="975" height="512" alt="image" src="https://github.com/user-attachments/assets/f5e75840-9b95-415e-b7ee-61df4f2847e9" />
<img width="975" height="512" alt="image" src="https://github.com/user-attachments/assets/b9b71a9a-1277-48ad-9021-91bfa0c1b46a" />
<img width="975" height="512" alt="image" src="https://github.com/user-attachments/assets/d309a2d7-c621-421b-93ee-4a1e30567a66" />
<img width="939" height="494" alt="image" src="https://github.com/user-attachments/assets/a9814bc4-f77a-4d25-8755-d5f17423bc90" />
<img width="975" height="512" alt="image" src="https://github.com/user-attachments/assets/7486c1ae-22b4-4359-98f8-8e24cc12a41d" />

## 🚀 Features

- ✅ Secure user authentication with role-based access (Admin, Organizer, Attendee)
- 📅 Event creation, update, and deletion by organizers
- 📍 Real-time booking and cancellation notifications using **SignalR**
- 📌 Interactive Google Maps API integration for event venue locations
- 🛡️ Input validation using FluentValidation and Data Annotations
- 🔍 Event search and filtering (by keyword, location, date)
- 🧪 Unit testing with xUnit and Moq
- 📱 Responsive design compatible with desktop and mobile browsers

---

## 🏗️ Technologies Used

| Layer          | Tools / Frameworks                                 |
|----------------|----------------------------------------------------|
| Frontend       | ASP.NET Core MVC, Razor Pages, JavaScript, jQuery, AJAX |
| Backend        | ASP.NET Core Web API, C#, SignalR, Identity         |
| Database       | SQL Server, Entity Framework Core                   |
| Testing        | xUnit, Moq                                          |
| External APIs  | Google Maps API                                     |

---

## 🧑‍💻 User Roles

- **Admin**: Full control over users and system monitoring
- **Organizer**: Create, edit, delete events and receive live booking updates
- **Attendee**: Browse events, make bookings, receive confirmations in real-time

---

## 📂 Project Structure (Important Folders)

/Controllers

EventsController.cs

CategoriesController.cs

AccountController.cs

/Models

EventsTable.cs

EventCategory.cs

/Views
/Events
/Categories
/Account
/Shared
_ViewImports.cshtml
_ViewStart.cshtml

/wwwroot

CSS, JS, Images

/Data

DB Context Files

/Hub

BookingHub.cs

---

## 🛠️ How to Run

1. **Clone the repository**
   ```bash
   git clone https://github.com/yug9190/EventManagementSystem.git

2. Set up the database

Open appsettings.json and update your SQL Server connection string.
Run migrations:
dotnet ef database update

3. Run the application
dotnet run

4. Access the system
Navigate to https://localhost:5001/ in your browser.

## Conclusion
The Event Management System provides a user-friendly interface for managing events and categories securely. With features such as authentication and CRUD operations, it offers a robust solution for event organization and management.

---
