# Car Rental Management System
This repository contains a .NET application for managing a car rental system. The application allows users to manage customers, vehicles, employees, and generate detailed reports on rental statuses, returns, and vehicle maintenance.

## Requirements
Visual Studio (version 2015 or higher).
.NET Framework (version 4.5).
SQL Server (version 2016 or higher)

### Key Features
- Customer Management: Module for registering and updating customer information.
- Rentals and Returns Tracking: Register rentals and track vehicle returns.
- Vehicle Maintenance: Manage scheduled maintenance activities for vehicles.
- Report Generation: View detailed RDLC reports on rental statuses, returns, and vehicle maintenance.

### Project Structure
- Reports Folder: Contains .xsd (XML schemas) and .rdlc (report definition) files, structuring and visualizing application data.
- .cs Files: C# source code that implements business logic and handles CRUD operations for each module (rentals, customers, vehicles, etc.).

### Dependencies and References
- The project uses the following key references:
- Microsoft.CSharp: Provides support for C# programming.
- Microsoft.ReportViewer: Components for displaying RDLC reports in Windows Forms.
- System.Data and System.Data.DataSetExtensions: For data management and DataSet operations.
- System.Windows.Forms: Supports building Windows Forms user interfaces.
- System.Xml and System.Xml.Linq: For XML data handling and manipulation.

## Installation and Usage
1. Clone this repository:
```bash
git clone https://github.com/mgmarce/alquilerCarros.git
```
2. Open the project in Visual Studio.
3. Confirm that you have downloaded and executed the database. _[(download database)](https://github.com/mgmarce/alquilerCarros/blob/main/db_alquilerCarros.sql)_
4. Make sure to restore required references and settings.
5. Run the application from Visual Studio.

#### Contributions
_Contributions are welcome! Please open an issue or pull request for improvements, bug fixes, or new features.✨_

## Authors
[Marcela Menjivar](https://www.github.com/mgmarce)
