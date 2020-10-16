CREATE PROCEDURE [dbo].[spUser_Add]
	@id nvarchar(128),
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@email nvarchar(256)

AS
BEGIN

	SET NOCOUNT ON 

	INSERT INTO dbo.[User]
	(Id, FirstName, LastName, Email)
	VALUES (@id, @firstName, @lastName, @email)

END

