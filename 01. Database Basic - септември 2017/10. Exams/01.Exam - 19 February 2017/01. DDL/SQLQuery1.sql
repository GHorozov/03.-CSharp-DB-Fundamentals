CREATE TABLE Countries
(	
	Id INT PRIMARY KEY IDENTITY, 
	Name NVARCHAR(50)  UNIQUE 
)

CREATE TABLE Customers
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(25),
	LastName NVARCHAR(25),
	Age  INT,
	Gender CHAR(1) CHECK (Gender = 'M' OR Gender = 'F') NOT NULL,
	PhoneNumber CHAR(10),
	CountryId INT FOREIGN KEY REFERENCES Countries(Id)
)

CREATE TABLE Distributors
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(25)  UNIQUE,
	CountryId INT FOREIGN KEY REFERENCES Countries(Id),
	AddressText NVARCHAR(30),
	Summary NVARCHAR(200)	
)

CREATE TABLE Products
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(25) UNIQUE,
	Description NVARCHAR(250),
	Recipe NVARCHAR(MAX),
	Price MONEY CHECK (Price >= 0)
)

CREATE TABLE Feedbacks
(
	Id INT PRIMARY KEY IDENTITY,
	ProductId INT FOREIGN KEY REFERENCES Products(Id),
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
	Rate DECIMAL(10,2) CHECK (Rate BETWEEN 0 AND 10),
	Description NVARCHAR(255)
	
) 

CREATE TABLE Ingredients
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(30),
	OriginCountryId INT FOREIGN KEY REFERENCES Countries(Id),
	Description NVARCHAR(200),
	DistributorId INT FOREIGN KEY REFERENCES Distributors(Id)
)

CREATE TABLE ProductsIngredients
(
	ProductId INT FOREIGN KEY REFERENCES Products(Id),
	IngredientId INT FOREIGN KEY REFERENCES Ingredients(Id),
	CONSTRAINT pk_ProductsIngredients PRIMARY KEY (ProductId,IngredientId) 
)
