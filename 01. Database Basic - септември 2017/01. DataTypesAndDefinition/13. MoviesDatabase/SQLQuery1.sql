CREATE DATABASE Movies

CREATE TABLE Directors(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Genres(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	GenreName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)


CREATE TABLE Movies(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Title NVARCHAR(50) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
	CopyrightYear INT,
	Length DECIMAL(10,2),
	GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Rating INT,
	Notes NVARCHAR(MAX)
)


INSERT INTO Directors (DirectorName,Notes) VALUES
('Ivan', 'offf ok'),
('Georgi', 'ok'),
('Stamat', 'stava'),
('Maria', 'asdadasd'),
('Desi', 'mmmmmmm')

INSERT INTO Genres (GenreName, Notes) VALUES
('Drama', 'drama queen'),
('Comedy', 'funny films'),
('Horor', 'stra6no'),
('Romantic', 'romati4ni ne6ta'),
('Erotic', 'erotic stuff')

INSERT INTO Categories (CategoryName, Notes) VALUES
('DramaCategory', 'Category drama queen'),
('ComedyCategory', 'Category funny films'),
('HororCategory', 'Category stra6no'),
('RomanticCategory', 'Category romati4ni ne6ta'),
('EroticCategory', 'Category erotic stuff')


INSERT INTO Movies (Title,DirectorId,CopyrightYear,Length,GenreId,CategoryId, Rating,Notes) VALUES
('Blue Ring', 1, 1999, 2.20, 3, 3, 9, 'good movie interesting ...'),
('Words', 2, 1999, 2.20, 3, 3, 9, 'not bad movie interesting ...'),
('Gnomes', 5, 1999, 2.20, 3, 3, 9, 'uuuuuuu interesting ...'),
('Man and woman', 4, 1999, 2.20, 3, 3, 9, 'ok for watching interesting ...'),
('Red Sonia', 3, 1999, 2.20, 3, 3, 9, 'ok movie interesting ...')