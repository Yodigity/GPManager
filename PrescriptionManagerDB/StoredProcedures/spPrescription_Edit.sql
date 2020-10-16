CREATE PROCEDURE [dbo].[spPrescription_Edit]
	@PrescriptionId int,
	@Dosage decimal(18,0),
	@PrescriberId nvarchar(128),
	@RenewalDate datetime2

AS
BEGIN

SET NOCOUNT ON

	UPDATE dbo.Prescription
	SET Dosage = @Dosage, PrescriberId = @PrescriberId, RenewalDate = @RenewalDate
	WHERE Id = @PrescriptionId

END

