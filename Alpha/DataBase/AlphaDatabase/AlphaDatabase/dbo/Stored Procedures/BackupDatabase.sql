CREATE PROCEDURE [dbo].[BackupDatabase]
	@name NVARCHAR(100), -- Имя БД
	@path NVARCHAR(MAX), -- Путь резервного копирования
	@step TINYINT = 5    -- С каким шагом выводить сообщения о прогрессе
AS
BEGIN
	BACKUP DATABASE @name
		TO DISK = @path
		WITH STATS = @step
END