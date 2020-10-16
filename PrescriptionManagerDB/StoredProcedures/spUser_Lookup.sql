CREATE PROCEDURE [dbo].[spUser_Lookup]
	@id nvarchar(128)
AS
	SELECT Id, FirstName, LastName, Email, CreatedDate 
	FROM [dbo].[User]
	WHERE Id = @id
RETURN 0
