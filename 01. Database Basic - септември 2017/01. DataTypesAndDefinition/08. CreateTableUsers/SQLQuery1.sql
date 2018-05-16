CREATE TABLE Users(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Username VARCHAR(30) NOT NULL,
	Password VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(1000),
	LastLoginTime DATE,
	IsDeleted BIT
)

INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, IsDeleted) VALUES
('Gosho', 'zzzzzz', null, 2011-01-04, 0),
('Marisa', 'kkkak', null, 2010-04-05, 0),
('Ivan', 'dnasd', null, 1999-03-06, 0),
('Mira', '123rrrr', null, 1943-06-07, 0),
('Nasko', 'passsss', null, 1987-03-03, 0)
