CREATE PROCEDURE [dbo].[spPatient_GetDetails]
	@id int

AS
BEGIN
	SET NOCOUNT ON

	SELECT Id, FirstName, LastName, DOB, Email, PhoneNumber

	FROM dbo.Patient
	WHERE  Id = @id;
	


END
