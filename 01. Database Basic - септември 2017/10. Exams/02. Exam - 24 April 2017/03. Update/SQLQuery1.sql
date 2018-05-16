UPDATE Jobs
SET Status = 'In Progress', MechanicId = (SELECT MechanicId FROM Mechanics WHERE FirstName = 'Ryan' and LastName = 'Harnos')
WHERE Status = 'Pending' 

