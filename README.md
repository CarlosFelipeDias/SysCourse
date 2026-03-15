# SysCourse

Study project created while learning ASP.NET Core development using Visual Studio Code and .NET CLI.

This repository is used to practice backend development concepts, project organization, and modern .NET workflows.

The application being developed in this repository is a contact book for registering and managing contacts.

---

## Learning Goals

This project is part of a personal study path focused on:

* Understanding ASP.NET Core MVC
* Practicing .NET CLI development workflow
* Learning Git and GitHub integration
* Implementing Dapper with MySQL
* Improving project architecture and organization

---

## Project Structure

```text
SysCourse
|
|-- src/              # Application projects
|   |-- WebUI         # ASP.NET Core MVC application
|
|-- test/             # Future unit tests
|
|-- SysCourse.sln     # Solution file
|-- global.json       # .NET SDK version
`-- README.md
```

---

## Technologies

* .NET 10
* ASP.NET Core MVC
* Dapper (planned)
* MySQL (planned)
* Visual Studio Code
* Git / GitHub

---

## Project Status

This project is currently under development as part of a learning process.

New features, improvements, and documentation will be added gradually.

---

## Development Environment

* Visual Studio Code
* .NET CLI
* Git

---

## Environment Variables

This project does not store the MySQL connection string in source code or versioned configuration files.

To run the project locally, create the following user environment variable in Windows:

```text
ConnectionStrings__DefaultConnection
```

Example value:

```text
Server=localhost;Port=3306;Uid=root;Pwd=your_password;
```

### How to Create It on Windows

1. Open the Start menu and search for `environment variables`.
2. Click `Edit the system environment variables`.
3. Click `Environment Variables...`.
4. Under `User variables`, click `New...`.
5. Set the variable name to `ConnectionStrings__DefaultConnection`.
6. Set the variable value to your MySQL connection string.
7. Close and reopen Visual Studio Code after saving the variable.

### PowerShell Examples

Temporary, for the current terminal session only:

```powershell
$env:ConnectionStrings__DefaultConnection="Server=localhost;Port=3306;Uid=root;Pwd=your_password;"
```

Persistent for the current Windows user:

```powershell
[System.Environment]::SetEnvironmentVariable(
  "ConnectionStrings__DefaultConnection",
  "Server=localhost;Port=3306;Uid=root;Pwd=your_password;",
  "User"
)
```

To verify it:

```powershell
[System.Environment]::GetEnvironmentVariable("ConnectionStrings__DefaultConnection","User")
```

---

## Future Improvements

Some features planned for the project:

* Database integration with MySQL
* Data access using Dapper
* Form validation
* Unit testing
* Improved project architecture

---

## Author

Carlos Dias

---

## License

This project is for educational purposes.
