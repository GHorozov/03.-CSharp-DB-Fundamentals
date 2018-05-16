CREATE TABLE People(
	Id INT	PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name VARCHAR(50) NOT NULL,
	Picture VARBINARY,
	Height DECIMAL(10,2),
	Weight DECIMAL(10,2),
	Gender VARCHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography VARCHAR(MAX)
)

INSERT INTO People ( Name, Picture, Height, Weight, Gender, Birthdate, Biography) VALUES
('Gosho', NUll, 1.78, 85, 'm', GETDATE(), 'I studied in ...' ),
('Ivan' , NUll, 1.72, 80, 'm', GETDATE(), 'I have ...'),
('Maria', NULL, 1.75, 55, 'f', GETDATE(), 'Education in ...'),
('Grigor', NULL, 1.95, 90, 'm', GETDATE(), 'Student in...'),
('Svetla', NULL, 1.69, 52, 'f', GETDATE(), 'Education in school ...')
