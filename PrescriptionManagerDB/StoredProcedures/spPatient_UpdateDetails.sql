CREATE PROCEDURE [dbo].[spPatient_UpdateDetails]
	@id int,
	@firstName nvarchar(Max),
	@lastName nvarchar(MAX),
	@email nvarchar(MAX),
	@phoneNumber nvarchar(MAX)
AS
BEGIN

	SET NOCOUNT ON

	UPDATE dbo.Patient SET FirstName = @firstName,LastName = @lastName, Email= @email, PhoneNumber = @phoneNumber
	WHERE Id = @id
END
