CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name NVARCHAR(50) NOT NULL
)

CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	AddressText NVARCHAR(50) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(Id),
)

CREATE TABLE Departments(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name NVARCHAR(50) NOT NULL,
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	JobTitle NVARCHAR(50) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
	HireDate NVARCHAR(50),
	Salary DECIMAL(15,2) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)


--•	Towns (Id, Name)
--•	Addresses (Id, AddressText, TownId)
--•	Departments (Id, Name)
--•	Employees (Id, FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary, AddressId)
