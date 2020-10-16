CREATE TABLE [dbo].[Prescription]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[PrescriberId] NVARCHAR(128) NOT NULL,
	[PatientId] NVARCHAR(128) NOT NULL,
	[DrugName] NVARCHAR(100) NOT NULL,
	[PrescriptionDate] datetime2(7) NOT NULL DEFAULT getutcdate(),
	[Dosage] decimal NOT NULL,
	[RenewalDate] datetime2(7) NOT NULL, 
    CONSTRAINT [FK_Prescription_User] FOREIGN KEY ([PrescriberId]) REFERENCES [User](Id), 

    CONSTRAINT [FK_Prescription_Patient] FOREIGN KEY ([PatientId]) REFERENCES [Patient](Id)

)
