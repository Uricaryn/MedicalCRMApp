Clinic Connect - Medical CRM Application
clinic Connect is a comprehensive Medical Customer Relationship Management (CRM) system designed to streamline operations within medical facilities. The application manages patient information, procedures, appointments, payments, and more, integrating various functionalities to ensure efficient healthcare management.

Note: This project is currently under active development. UI features  will be fully implemented, and ongoing changes are expected.

Table of Contents
About the Project
Technologies Used
Features
Getting Started
Prerequisites
Installation
Running the Application
API Documentation
Contributing
Known Issues
Roadmap
License
Contact
About the Project
Clinic Connect is designed to assist medical professionals in managing the day-to-day operations of variations of clinics and hospitals. It provides tools for tracking patient appointments, managing medical procedures, updating patient records, and handling payments. The goal is to improve patient care, reduce administrative workload, and enhance the overall efficiency of medical facilities.

Technologies Used
Backend:
ASP.NET Core 6
Entity Framework Core
SQL Server
AutoMapper
Identity for user authentication and authorization
Frontend:
Planned : .NET MAUI for cross-platform mobile and desktop applications
XAML for UI design
API Documentation:
Swagger for API testing and documentation
Logging:
Serilog for structured logging
Version Control:
Git and GitHub for source code management
Other Tools:
Docker (for future containerization support)
NUnit and xUnit for unit testing
Features
Patient Management: Create, read, update, and delete patient records.
Procedure Management: Manage medical procedures and track the products used in each procedure.
Appointment Scheduling: Schedule and manage appointments with integrated reminders.
User Authentication: Secure login system with roles and permissions using ASP.NET Identity.
Payments: Track patient payments and manage financial transactions.
Product: Track product stocks and usage on procedures through StockTransaction and ProcedureProduct 
Cross-Platform Accessibility: Accessible via desktop and mobile applications developed using .NET MAUI.
Getting Started
Prerequisites
.NET 6 SDK or higher
Visual Studio 2022 or later
SQL Server or SQL Server Express
Node.js (for potential frontend builds)
Docker (optional, for running in a containerized environment)
Installation
Clone the repository:

bash
Kodu kopyala
git clone https://github.com/MedicalCrmApp.git

Set up the database:

Update the connection string in appsettings.json to point to your SQL Server instance.
Run the database migrations to set up the initial schema:
bash
Kodu kopyala
dotnet ef database update
Install dependencies:

Backend dependencies:
bash
Kodu kopyala
dotnet restore
Frontend dependencies (if applicable):
bash
Kodu kopyala
npm install
Running the Application
Start the API:
bash
Kodu kopyala
dotnet run --project Medical_CRM_API
Run the MAUI Application:
Open the MAUI project in Visual Studio and set it as the startup project.
Select your desired platform (Android, iOS, Windows) and run.
API Documentation
The API is documented using Swagger and can be accessed at https://localhost:7068/swagger once the API is running. It provides interactive endpoints to test and explore the API functionality.

Contributing
Contributions are welcome! Please follow these steps:

Fork the project.
Create your feature branch (git checkout -b feature/new-feature).
Commit your changes (git commit -m 'Add some feature').
Push to the branch (git push origin feature/new-feature).
Open a pull request.
Known Issues
Login Issues: Some login problems may occur due to misconfigured identity settings. Ensure that user credentials are correctly set.
CORS Errors: CORS settings must be correctly configured on the API for cross-origin requests.
Roadmap
 Complete the implementation of the payment module.
 Enhance the UI for a better user experience.
 Implement Docker support for easy deployment.
 Add support for multi-language interfaces.
 Integration with third-party healthcare services (e.g., insurance providers).
License
This project is licensed under the MIT License. See the LICENSE file for more details.

Contact
Developer: Onur Anatça
Email: onuranatca@hotmail.com.tr
