CREATE PROCEDURE [dbo].[Patient_GetPrescriptionDetails]
	@id int
AS
BEGIN
	SELECT * FROM dbo.Prescription
	WHERE  Prescription.PatientId = @id
END
