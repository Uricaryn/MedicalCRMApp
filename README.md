Clinic Connect - Medical CRM Application Overview
Clinic Connect is an advanced Medical CRM system that simplifies the management of clinics and hospitals by integrating various functionalities like patient management, appointment scheduling, payment tracking, and more. The application is under active development and aims to improve healthcare efficiency through a streamlined user interface and backend system.

Technologies Used
Backend: ASP.NET Core 6, Entity Framework Core, SQL Server, AutoMapper, Identity
Frontend: .NET MAUI (planned for cross-platform), XAML for UI design
API Documentation: Swagger
Logging: Serilog
Version Control: Git, GitHub
Other Tools: Docker (future support), NUnit, xUnit
Key Features
Patient Management: CRUD operations for patient records.
Procedure Management: Track medical procedures and associated product usage.
Appointment Scheduling: Integrated system with reminders.
User Authentication: Secure login with roles/permissions.
Payments: Manage patient payments and transactions.
Product Tracking: Manage stock levels and usage.
Cross-Platform: Accessible on desktop and mobile with .NET MAUI.
Getting Started
Prerequisites
.NET 6 SDK
Visual Studio 2022+
SQL Server or SQL Server Express
Node.js (for frontend)
Docker (optional)
Installation Steps
Clone the repository:

bash
Kodu kopyala
git clone https://github.com/MedicalCrmApp.git
Set up the database:

Update the connection string in appsettings.json.
Run migrations:
bash
Kodu kopyala
dotnet ef database update
Install dependencies:

Backend:
bash
Kodu kopyala
dotnet restore
Frontend (if applicable):
bash
Kodu kopyala
npm install
Run the Application:

Start API:
bash
Kodu kopyala
dotnet run --project Medical_CRM_API
Run the MAUI Application in Visual Studio.
API Documentation
Access via Swagger at https://localhost:7068/swagger.

Contributing
Fork the project.
Create your feature branch:
bash
Kodu kopyala
git checkout -b feature/new-feature
Commit your changes:
bash
Kodu kopyala
git commit -m 'Add some feature'
Push to the branch:
bash
Kodu kopyala
git push origin feature/new-feature
Open a pull request.
Roadmap
Complete payment module.
UI enhancements.
Add Docker support.
Multi-language support.
Integration with third-party services.
Contact
Developer: Onur Anatça
Email: onuranatca@hotmail.com.tr
