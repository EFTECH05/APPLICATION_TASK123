
<img width="1363" height="707" alt="merr" src="https://github.com/user-attachments/assets/b4e4fb61-84e5-4c3f-95f4-0c595d1baa66" />

Youtube web  demo : https://www.youtube.com/watch?v=0w5iqUrRZCw&t=656s
APPLICATION DEVELOPMENT SECURITY PROJECT
APDS International Payments Portal
Project Overview

This is a school-based software engineering project developed as part of a group assignment focused on designing and implementing a secure international banking and payments system.

The project simulates a real-world fintech environment where users can securely authenticate, manage accounts, and process international payments through a controlled, approval-based workflow.

The system consists of two main portals:

Customer Portal – Allows registered customers to securely log in, complete OTP verification via Gmail, enter international payment details, and submit transactions.
Employee Portal – Allows bank employees to securely log in, review and verify customer payments, validate SWIFT codes, and approve transactions for processing via SWIFT.

Security is a core focus of the system, with multiple layers of protection implemented to safeguard sensitive financial data.

Team Members & Roles
Ishka Sewshanker – Frontend Development (Customer Portal UI: HTML, CSS, JavaScript)
Xolisile Princess Mnyandu – Frontend Development (Employee Portal UI: HTML, CSS, JavaScript)
Franklin Ngangu – Backend Development (ASP.NET Core MVC, Database Integration, Security Implementation)
David Oyowa – Security Implementation (Input Validation, Password Hashing, SSL Configuration, Secure Coding Practices)
Chantel Mafunise – Testing & Security Tools (MobSF, ScoutSuite, SonarQube, CI/CD with CircleCI)
Mororiseng Jessica Legodi – Documentation, Video Demonstration, README, Submission Preparation
Features
Customer Portal
Secure user registration (full name, ID number, account number, password)
Secure login system
OTP verification sent via Gmail (Email-based Multi-Factor Authentication)
Password hashing and salting using ASP.NET Core Identity
Input validation using RegEx whitelisting
Secure HTTPS/SSL communication
Protection against:
Session hijacking
Clickjacking
SQL Injection
Cross-Site Scripting (XSS)
Man-in-the-Middle (MITM) attacks
DDoS attacks
Employee Portal
Pre-registered employee-only access (no registration system)
Secure login with hashed and salted passwords
Input validation using RegEx whitelisting
Review, verify, and process customer transactions
Validate SWIFT codes and approve payments
Same security protections as the Customer Portal
Security Implementation

The system applies secure software engineering principles, including:

Password hashing and salting (ASP.NET Core Identity)
OTP-based multi-factor authentication (Gmail email OTP)
Role-Based Access Control (RBAC)
Parameterized queries (SQL Injection prevention)
Input validation using RegEx whitelisting
Secure HTTPS/SSL communication
Session security mechanisms
Threat Protection Includes:
SQL Injection prevention
Cross-Site Scripting (XSS) protection
Session hijacking prevention
Clickjacking protection
Man-in-the-Middle (MITM) attack mitigation
DDoS attack awareness and mitigation strategies
Tech Stack
Frontend: HTML, CSS, JavaScript (Razor Views)
Backend: ASP.NET Core MVC
Database: Microsoft SQL Server (MSSQL)
Authentication: ASP.NET Core Identity (OTP + RBAC)
Security: Parameterized Queries + Secure Coding Practices
Deployment: SSL-enabled environment (AWS-tested)
Security Tools Used
MobSF – Static and dynamic application security testing
ScoutSuite – AWS cloud security auditing
SonarQube – Code quality and vulnerability detection
CircleCI – CI/CD pipeline automation
Project Summary

This project demonstrates the application of secure software engineering principles in a simulated banking system.

It highlights:

Full-stack development using ASP.NET Core MVC
Secure authentication with OTP (Gmail-based MFA)
Protection against common web vulnerabilities
Use of parameterized queries for database security
Role-based access control and system design
Team collaboration in a structured software engineering project
