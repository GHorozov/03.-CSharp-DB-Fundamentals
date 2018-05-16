USE SOFTUNI

INSERT INTO Towns (Name) VALUES
('Sofia'),
('Plovdiv'),
('Varna'),
('Burgas')

INSERT INTO Addresses (AddressText, TownId) VALUES
('one street 24', 1),
('two street 21', 2),
('three street 20', 3)

INSERT INTO Departments (Name) VALUES
('Engineering'),
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance')

INSERT INTO Employees (FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary, AddressId) VALUES
('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, 01/02/2013, 3500.00, 1),
('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, 02/03/2004, 4000.00,2),
('Maria', 'Petrova', 'Ivanova', 'Intern', 5, 28/08/2016, 4000.00,3),
('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, 09/12/2007, 3000.00,1),
('Peter', 'Pan', 'Pan', 'Intern', 3, 28/08/2016, 599.88,1)


--•	Towns: Sofia, Plovdiv, Varna, Burgas
--•	Departments: Engineering, Sales, Marketing, Software Development, Quality Assurance
