# Clinic Connect - Medical CRM Application

Clinic Connect is a comprehensive Medical Customer Relationship Management (CRM) system designed to streamline operations within medical facilities. The application manages patient information, procedures, appointments, payments, and more, integrating various functionalities to ensure efficient healthcare management.

> **Note**: This project is currently under active development. UI features will be fully implemented, and ongoing changes are expected.

## Table of Contents

- [About the Project](#about-the-project)
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [Known Issues](#known-issues)
- [Roadmap](#roadmap)
- [License](#license)
- [Contact](#contact)

## About the Project

Clinic Connect is designed to assist medical professionals in managing the day-to-day operations of various clinics and hospitals. It provides tools for tracking patient appointments, managing medical procedures, updating patient records, and handling payments. The goal is to improve patient care, reduce administrative workload, and enhance the overall efficiency of medical facilities.

## Technologies Used

- **Backend**: ASP.NET Core 6, Entity Framework Core, SQL Server, AutoMapper, Identity for user authentication and authorization
- **Frontend**: Planned: .NET MAUI for cross-platform mobile and desktop applications, XAML for UI design
- **API Documentation**: Swagger for API testing and documentation
- **Logging**: Serilog for structured logging
- **Version Control**: Git and GitHub for source code management
- **Other Tools**: Docker (for future containerization support), NUnit and xUnit for unit testing

## Features

- **Patient Management**: Create, read, update, and delete patient records.
- **Procedure Management**: Manage medical procedures and track the products used in each procedure.
- **Appointment Scheduling**: Schedule and manage appointments with integrated reminders.
- **User Authentication**: Secure login system with roles and permissions using ASP.NET Identity.
- **Payments**: Track patient payments and manage financial transactions.
- **Product Tracking**: Track product stocks and usage on procedures through StockTransaction and ProcedureProduct.
- **Cross-Platform Accessibility**: Accessible via desktop and mobile applications developed using .NET MAUI.

## Getting Started

### Prerequisites

- .NET 6 SDK or higher
- Visual Studio 2022 or later
- SQL Server or SQL Server Express
- Node.js (for potential frontend builds)
- Docker (optional, for running in a containerized environment)

### Installation

1. **Clone the repository**:
    ```bash
    git clone https://github.com/MedicalCrmApp.git
    ```

2. **Set up the database**:
    - Update the connection string in `appsettings.json` to point to your SQL Server instance.
    - Run the database migrations to set up the initial schema:
      ```bash
      dotnet ef database update
      ```

3. **Install dependencies**:
    - Backend dependencies:
      ```bash
      dotnet restore
      ```
    - Frontend dependencies (if applicable):
      ```bash
      npm install
      ```

### Running the Application

- **Start the API**:
    ```bash
    dotnet run --project Medical_CRM_API
    ```
- **Run the MAUI Application**:
    - Open the MAUI project in Visual Studio and set it as the startup project.
    - Select your desired platform (Android, iOS, Windows) and run.

## API Documentation

The API is documented using Swagger and can be accessed at [https://localhost:8080/swagger](https://localhost:8080/swagger) once the API is running. It provides interactive endpoints to test and explore the API functionality.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the project.
2. Create your feature branch (`git checkout -b feature/new-feature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/new-feature`).
5. Open a pull request.

## Roadmap

- Complete the implementation of the payment module.
- Enhance the UI for a better user experience.
- Implement Docker support for easy deployment.
- Add support for multi-language interfaces.
- Integration with third-party healthcare services (e.g., insurance providers).

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact

Developer: Onur Anatça  
Email: [onuranatca@hotmail.com.tr](mailto:onuranatca@hotmail.com.tr)


![api1](https://github.com/user-attachments/assets/7c0a16d2-38c0-48e1-a651-c3bc7cdfbedb) ![api2](https://github.com/user-attachments/assets/47a61e9e-d024-4db3-a6e0-54d4ce26b054)

![api3](https://github.com/user-attachments/assets/662f11a1-c54f-43d7-86f7-777cc1bf5d07) ![cc1](https://github.com/user-attachments/assets/42f8081f-dd7e-4cd3-8b89-c5fe6abc36c3)

![cc2](https://github.com/user-attachments/assets/3f20daf3-32f2-49e3-9c95-3c320dfb384b) ![cc2](https://github.com/user-attachments/assets/3f20daf3-32f2-49e3-9c95-3c320dfb384b)
  

