<br/>
<p align="center">
  <a href="https://github.com/shenoyranjith/ASPNetBoilerplate">
    <img src="images/Microsoft_.NET_logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">ASPNETBoilerplate</h3>

![Contributors](https://img.shields.io/github/contributors/shenoyranjith/ASPNetBoilerplate?color=dark-green) ![Forks](https://img.shields.io/github/forks/shenoyranjith/ASPNetBoilerplate?style=social) ![Stargazers](https://img.shields.io/github/stars/shenoyranjith/ASPNetBoilerplate?style=social) ![Issues](https://img.shields.io/github/issues/shenoyranjith/ASPNetBoilerplate) ![License](https://img.shields.io/github/license/shenoyranjith/ASPNetBoilerplate) 

## Table Of Contents

- [Table Of Contents](#table-of-contents)
- [About The Project](#about-the-project)
- [Built With](#built-with)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
- [Usage](#usage)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
  - [Creating A Pull Request](#creating-a-pull-request)
- [License](#license)
- [Authors](#authors)
- [Acknowledgements](#acknowledgements)

## About The Project

This boilerplate was built to jump start development with minimal setup and save hours configuring and setting up common functionalities in a REST API project. The project followes the N Tier Architecture.
 
The solution has four projects that are included in it.
1. Web - This is the main ASP .NET Core project.
2. Domain - This is a Class Library that has all your business domain entities.
3. Repository - This is a Class Library and implements the Repository pattern.
4. Migrator - This is a Console Application that runs your migrations.

## Built With

This project is configured with the following

* [Microsoft .NET 7](https://dotnet.microsoft.com/en-us/)
* [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0)
* [AutoMapper](https://automapper.org/)
* [Dapper](https://github.com/DapperLib/Dapper)
* [Dapper Extensions](https://github.com/tmsmith/Dapper-Extensions)
* [Newtonsoft.Json](https://www.newtonsoft.com/json)
* [Swashbuckle.AspNetCore](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio)
* [Fluent Migrator](https://fluentmigrator.github.io/)

## Getting Started

All you have to do to use this boilerplate is to fork this repository and then clone the forked repository! It's that simple!

### Prerequisites

- Ensure that you have .NET 7 SDK installed.
- This project is makes use of SQLServer as the Database.

## Usage

1. Run the rename.ps1 file to rename all the files and update the namespaces with your project name.
```
.\rename.ps1 <YOUR_PROJECT_NAME>
```
2. Update the connection strings in the Web and Migrator project to that of your database.
3. Follow the instructions in the [README](https://github.com/shenoyranjith/ASPNetBoilerplate/blob/master/src/ASPNetBoilerplate.Migrator/README.md) in the Migrator to run the migrations.

## Roadmap

See the [open issues](https://github.com/shenoyranjith/ASPNetBoilerplate/issues) for a list of proposed features (and known issues).

## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.
* If you have suggestions for adding or removing projects, feel free to [open an issue](https://github.com/shenoyranjith/ASPNetBoilerplate/issues/new) to discuss it, or directly create a pull request.
* Please make sure you check your spelling and grammar.
* Create individual PR for each suggestion.

### Creating A Pull Request

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

Distributed under the MIT License. See [LICENSE](https://github.com/shenoyranjith/ASPNetBoilerplate/blob/main/LICENSE.md) for more information.

## Authors

* **Ranjith Shenoy** - *An enthusiastic software developer * - [Ranjith Shenoy](https://github.com/shenoyranjith) - **

## Acknowledgements
