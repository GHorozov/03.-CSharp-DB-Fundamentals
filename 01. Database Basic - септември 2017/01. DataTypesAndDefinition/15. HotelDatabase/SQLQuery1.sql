CREATE DATABASE Hotel

USE Hotel

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	LastName  NVARCHAR(50) NOT NULL,
	Title NVARCHAR(50),
	Notes NVARCHAR(max)
)

CREATE TABLE Customers(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	AccountNumber BIGINT NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	LastName  NVARCHAR(50) NOT NULL,
	PhoneNumber NVARCHAR(50) NOT NULL,
	EmergencyName NVARCHAR(50) NOT NULL,
	EmergencyNumber NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(max)
)

CREATE TABLE RoomStatus(
	RoomStatus NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(max)
)

CREATE TABLE RoomTypes(
	RoomType NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(max)
)

CREATE TABLE BedTypes(
	BedType NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(max)
)

CREATE TABLE Rooms(
	RoomNumber INT NOT NULL,
	RoomType  NVARCHAR(50) NOT NULL,
	BedType NVARCHAR(50) NOT NULL,
	Rate INT,
	RoomStatus NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(max)
)

CREATE TABLE Payments(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	PaymentDate DATE,
	AccountNumber INT,
	FirstDateOccupied DATE,
	LastDateOccupied DATE,
	TotalDays INT,
	AmountCharged DECIMAL(15,2) NOT NULL,
	TaxRate INT,
	TaxAmount DECIMAL(15,2) NOT NULL,
	PaymentTotal DECIMAL(15,2) NOT NULL,
	Notes NVARCHAR(max)
)

CREATE TABLE Occupancies(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	DateOccupied DATE,
	AccountNumber BIGINT NOT NULL,
	RoomNumber INT,
	RateApplied INT,
	PhoneCharge DECIMAL(10, 2) NOT NULL,
	Notes NVARCHAR(max)
)

INSERT INTO Employees (FirstName, LastName, Title, Notes) VALUES
('IVAN', 'IVANOV', 'MANAGER', 'MAN ...'),
('GEORGI', 'PETROV', 'WORKER', 'WORK WORK AND WORK ...'),
('STAMAT', 'STAMATOV', 'WORKER', 'WORK HARD WORK ...')

INSERT INTO Customers (AccountNumber, FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber, Notes) VALUES
(123131, 'IVAN', 'IVANOV', '012353', 'PETQ', '1313413', 'SOMETHING'),
(123355, 'GEORGI', 'ROTKOV', '012365', 'ZDRAVKO', '131433', 'SOMETHING ELSE ...'),
(123131, 'MARIA', 'IVANOVA', '012546', 'SPAS', '1578413', 'SOMETHING ELSE ELSE ...')

INSERT INTO RoomStatus (RoomStatus, Notes) VALUES
('OK', 'ROOM 1'),
('NICE', 'ROOM 2'),
('NOT BAD', 'ROOM 3')

INSERT INTO RoomTypes (RoomType, Notes) VALUES
('SINGLE', NULL),
('DOUBLE', NULL),
('LARGE', NULL)

INSERT INTO BedTypes (BedType, Notes) VALUES
('SMALL', NULL),
('BIG', NULL),
('LARGE', 'large')

INSERT INTO Rooms (RoomNumber, RoomType, BedType, Rate, RoomStatus, Notes) VALUES
(131, 'SINGLE', 'SMALL', 1, 'OK', NULL),
(132, 'LARGE', 'DOUBLE', 3, 'NICE', NULL),
(133, 'BIG', 'LARGE', 2, 'NOT BAD', NULL)


INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, AmountCharged, TaxRate, TaxAmount, PaymentTotal, Notes) VALUES
(1, GETDATE(), 12331, GETDATE()+1, GETDATE() + 3, 3, 222,20, 2.22, 1233 , 'SOMETHING'),
(2, GETDATE(), 125, GETDATE()+2, GETDATE() + 5, 10, 324,22, 3.1, 1333 , 'SOMETHING ELSE'),
(3, GETDATE(), 126, GETDATE()+3, GETDATE() + 6, 20, 325,23, 2.5, 1444 , 'SOMETHING ELSE ELSE ')


INSERT INTO Occupancies (EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge, Notes) VALUES
(1, GETDATE(), 1234, 2,32, 123,'SOME NOTE'),
(3, GETDATE()+1, 124, 3,52, 156,'SOME NOTE 2'),
(2, GETDATE()+2, 1256, 42,44, 175,'SOME NOTE 3')
