CREATE PROCEDURE [dbo].[spPrescription_Add]
	@PatientId nvarchar(128), @DrugName nvarchar(100), @Dosage decimal(18, 0), @PrescriberId nvarchar(128), @RenewalDate datetime2

AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.Prescription 
	(PatientId, DrugName, Dosage, PrescriberId, RenewalDate)
	VALUES
	(@PatientId, @DrugName, @Dosage, @PrescriberId, @RenewalDate)


END
