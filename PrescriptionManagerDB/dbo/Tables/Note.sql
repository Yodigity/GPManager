CREATE TABLE [dbo].[Note]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PatientId] NVARCHAR(128) NOT NULL,
    [Title] NVARCHAR(50) NOT NULL, 
    [Text] TEXT NOT NULL, 
    [AuthorId] NVARCHAR(128) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate() 
)
