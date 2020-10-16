CREATE PROCEDURE [dbo].[spPatient_Add]
	@FirstName nvarchar(50), @LastName nvarchar(50), @DOB datetime2, @Email nvarchar(100), @PhoneNumber nvarchar(11)

	AS
	BEGIN

	SET NOCOUNT ON
	INSERT INTO dbo.Patient
	(FirstName, LastName, DOB, Email, PhoneNumber)
	VALUES (@FirstName, @LastName, @DOB, @Email, @PhoneNumber)

	End
