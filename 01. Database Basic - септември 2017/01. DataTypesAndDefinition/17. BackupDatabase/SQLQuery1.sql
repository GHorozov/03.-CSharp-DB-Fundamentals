USE SoftUni

GO
BACKUP DATABASE SoftUni
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\Backup\softuni-backup.bak'
		WITH FORMAT,
		MEDIANAME = 'Z_BACKUPSQLSOFTUNI_DATABASE',
		NAME = 'Full Backup of SOFTUNI';
GO