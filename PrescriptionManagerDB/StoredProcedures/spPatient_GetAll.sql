CREATE PROCEDURE [dbo].[spPatient_GetAll]
	
AS
BEGIN

SET NOCOUNT ON
	
	SELECT Id, FirstName, LastName, DOB, Email, PhoneNumber from dbo.Patient
	
END
