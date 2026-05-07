# Car Rental Management System

A desktop-based Car Rental Management System developed using **C#**, **Windows Forms**, and **SQL Server** for managing vehicle rentals, bookings, payments, offers, and user management efficiently.

---

## Introduction

In the fast-paced world of urban transportation, finding a reliable car quickly and managing vehicle fleets efficiently are essential for customer satisfaction and business success.

The **Car Rental Management System** is designed to provide a complete desktop-based solution where:
- Car owners can list and manage vehicles
- Customers can rent cars easily
- Admins can monitor and control the platform
- Super Admins can manage the entire system

The project demonstrates the practical implementation of:
- Object-Oriented Programming (OOP)
- Graphical User Interface (GUI)
- Database Integration
- Desktop Application Development
- Real-life Business Logic

---

## Objectives

- Develop a real-life desktop-based application
- Implement Object-Oriented Programming concepts
- Create a user-friendly GUI
- Integrate SQL Server database
- Manage rentals, payments, and reviews efficiently
- Ensure proper validation and verification

---

## Technologies Used

### Programming Language
- C#

### Framework
- Windows Forms (.NET)

### Database
- Microsoft SQL Server

### Development Tools
- Visual Studio 2026
- SQL Server Management Studio (SSMS)

---

## User Roles

### Customer
- Register/Login
- Browse available cars
- Rent vehicles
- Apply offers
- Make payments
- Return rented cars
- Submit reviews

### Owner
- Add and manage cars
- Monitor bookings
- Manage offers
- Track earnings
- View inventory status

### Admin
- Manage users
- Manage bookings
- Monitor reports
- Manage cars and offers

### Super Admin
- Manage admins
- Monitor the entire system
- Control platform activities

---

## Features

- User Authentication System
- Role-Based Access Control
- Car Listing & Management
- Car Booking System
- Offer & Discount System
- Payment Management
- Late Fine Calculation
- Review & Rating System
- Inventory Dashboard
- Admin Control Panel
- Database Integration
- Input Validation & Verification

---

## Case Study

The system allows car owners to list vehicles for rent while customers can search and book cars according to their needs.

Customers can:
- Filter cars by location and seats
- Apply discount offers
- Make payments using digital payment methods
- Return vehicles
- Submit reviews after rental completion

Owners can:
- Track bookings and earnings
- Manage vehicle availability
- Create promotional offers

Admins and Super Admins oversee the entire platform to ensure smooth operations.

---

## Database Schema

### Main Tables
- Users
- Cars
- Bookings
- Payments
- Offers
- Reviews
- Fines

### Database Features
- Primary Keys
- Foreign Keys
- Constraints
- Data Validation
- Normalized Database Structure

---

## Project Structure

```bash
Car-Rental-Management-System/
│
├── README.md
│
├── SourceCode/
│   ├── Forms/
│   ├── Classes/
│   ├── Database/
│   └── Assets/
│
├── Database/
│   ├── SQL_Queries.sql
│   └── Database_Schema.png
│
├── Screenshots/
│   ├── Login_Page.png
│   ├── Dashboard.png
│   ├── Payment_System.png
│   └── Admin_Panel.png
│
├── Report/
│   └── Project_Report.pdf
│
└── Presentation/
    └── Project_Slides.pptx
```

---

## Database Tables

### Users Table
Stores:
- Customer Information
- Owner Information
- Admin Accounts
- Authentication Data

### Cars Table
Stores:
- Vehicle Details
- Pricing Information
- Availability Status
- Owner Information

### Bookings Table
Stores:
- Rental Information
- Return Dates
- Payment Status
- Booking Status

### Payments Table
Stores:
- Transaction Information
- Payment Methods
- Payment Verification

### Reviews Table
Stores:
- Ratings
- Customer Feedback

---

## Payment System

Supported payment methods:
- bKash
- Nagad
- Rocket
- Card
- Cash

The system automatically:
- Generates transaction IDs
- Updates payment status
- Calculates payable amount
- Applies discounts

---

## Fine Management

Late return fines are automatically calculated based on:
- Actual return date
- Expected return date
- Daily fine amount

---

## UI Screenshots

### Authentication
- Login Page
- Registration Page

### Customer Interfaces
- Customer Dashboard
- Car Rent Page
- Payment & Return Page
- Review Section
- Booking History

### Owner Interfaces
- Owner Dashboard
- Manage Cars
- Earnings Dashboard
- Inventory Management

### Admin Interfaces
- Admin Dashboard
- User Management
- Booking Management
- Reports

### Super Admin Interfaces
- Super Admin Dashboard
- Manage Users
- Manage Reviews
- Manage Cars

---

##  Setup Instructions

### Requirements
- Visual Studio
- SQL Server
- .NET Framework
- Windows OS

### Installation Steps

1. Clone the repository

```bash
git clone https://github.com/your-username/car-rental-management-system.git
```

2. Open the project in Visual Studio

3. Import the SQL database

4. Configure the database connection string

5. Run the application

---

##  Validation & Security

- Input validation implemented
- Role-based authorization
- Unique email and username constraints
- Secure transaction handling
- Database integrity maintained through constraints

---

##  Advantages

- User-friendly interface
- Efficient rental management
- Organized database system
- Secure payment handling
- Real-life business implementation
- Easy system monitoring

---

##  Limitations

- Desktop-only system
- Requires manual database setup
- Internet-based payment API not integrated
- No real-time GPS tracking

---

##  Future Improvements

- Mobile application support
- Online payment gateway integration
- GPS vehicle tracking
- AI-based recommendations
- Cloud database integration
- Online booking notifications

---

##  Academic Information

### Institution
AMERICAN INTERNATIONAL UNIVERSITY–BANGLADESH (AIUB)

### Department
Department of Computer Science

### Course
CSC2210: Object Oriented Programming 2

### Semester
Spring 2025-2026

### Supervisor
Dr. Iftekharul Mobin

---

##  Team Members

| Name | ID |
|---|---|
| Tabassum Anshera Ridika | 24-58260-2 |
| Abdullah Yusuf | 24-58269-2 |

---

##  Conclusion

The Car Rental Management System successfully demonstrates the implementation of Object-Oriented Programming concepts, GUI development, and database integration in a real-life desktop application.

The project provides an efficient solution for managing car rentals, users, payments, and administrative activities while maintaining a user-friendly and organized system architecture.

---

##  License

This project is developed for academic and educational purposes.

---

##  Contribution

Contributions, suggestions, and improvements are welcome.

---

##  Acknowledgements

Special thanks to:
- Our course instructor
- Project supervisor
- Team members
- American International University–Bangladesh (AIUB)
