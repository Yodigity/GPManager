CREATE PROCEDURE [dbo].[spPrescription_GetPendingRenewals]
	@renewalDate datetime2,
	@today datetime2
AS
BEGIN
SET NOCOUNT ON
	SELECT dbo.Prescription.DrugName AS DrugName, dbo.Prescription.Id AS PrescriptionId, dbo.Prescription.RenewalDate, 
	dbo.Prescription.PatientId, dbo.Patient.FirstName, dbo.Patient.LastName, dbo.Patient.PhoneNumber FROM dbo.Prescription JOIN dbo.Patient ON dbo.Patient.Id = dbo.Prescription.PatientId  /*FULL OUTER JOIN dbo.Patient ON dbo.Prescription.PatientId = dbo.Patient.Id */
	WHERE dbo.Prescription.RenewalDate BETWEEN @today AND @renewalDate 
	
	
END

