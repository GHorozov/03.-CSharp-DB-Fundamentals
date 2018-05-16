CREATE DATABASE CarRental

USE CarRental

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	CategoryName NVARCHAR(50) NOT NULL,
	DailyRate INT,
	WeeklyRate INT,
	MonthlyRate INT,
	WeekendRate INT
)

CREATE TABLE Cars(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PlateNumber INT NOT NULL,
	Manufacturer NVARCHAR(50) NOT NULL,
	Model NVARCHAR(50),
	CarYear INT,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Doors INT,
	Picture VARBINARY(1000),
	Condition NVARCHAR(50),
	Available BIT
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Title NVARCHAR(50),
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	DriverLicenceNumber INT NOT NULL,
	FullName NVARCHAR(50) NOT NULL,
	Address NVARCHAR(50) NOT NULL,
	City NVARCHAR(50) NOT NULL,
	ZIPCode INT NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RentalOrders(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
	CarId INT FOREIGN KEY REFERENCES Cars(Id),
	TankLevel DECIMAL(10,2) NOT NULL,
	KilometrageStart BIGINT NOT NULL,
	KilometrageEnd BIGINT NOT NULL,
	TotalKilometrage INT NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	TotalDays DECIMAL(10,2) NOT NULL,
	RateApplied INT,
	TaxRate INT,
	OrderStatus NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate) VALUES
('RACE', 1, 2,3,4  ),
('SPORT', 2, 3,4,10 ),
('SHOPPING', 10, 20,33, 100 )

INSERT INTO  Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available) VALUES
(1234 , 'MERCEDES', 'CLK', 2016, 1,4,NULL,'NEW', 1 ),
(1532, 'HONDA', 'CIVIC', 2015, 2,4,NULL,'NEW', 1 ),
(3456 , 'AUDI', 'R6', 2015, 3,4,NULL,'NEW', 1 )

INSERT INTO Employees (FirstName, LastName, Title, Notes) VALUES
('IVAN', 'IVANOV', 'MANAGER', 'MANAGER WORK WITH ...'),
('GEORGI', 'PETROV', 'MANAGER', 'PRODUCT MANAGER WORK WITH ...'),
('GAN4O', 'GAN4EV', 'MANAGER', ' CARS MANAGER TEAM WITH ...')

INSERT INTO Customers (DriverLicenceNumber, FullName, Address, City, ZIPCode, Notes) VALUES
(123434234, 'IVAN IVANOV IVANOV', 'BLD 34', 'SOFIA', 5053, 'JUST ...'),
(123434234, 'GEORGI GEORGIEV FEORGIEV', 'BRTD 12', 'STARA ZAGORA', 6000, 'SOME CARS ...'),
(123434234, 'PETAR PETROV PETROV', 'BFGD 3', 'VARNA', 1000, 'ELSE AND ...')

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes) VALUES
(1, 2, 2, 10, 12313312, 12313330, 30, GETDATE(), GETDATE() + 1, 1, 12,22, 'YES', 'ORDER THIS CAR ...' ),
(2, 1, 3, 30, 12313444, 12313545, 44, GETDATE(), GETDATE() + 2, 2, 13,25, 'YES', 'ORDER THIS CAR ...' ),
(3, 3, 2, 40, 12313555, 12313656, 55, GETDATE(), GETDATE() + 3, 3, 14,26, 'YES', 'ORDER THIS CAR ...' )