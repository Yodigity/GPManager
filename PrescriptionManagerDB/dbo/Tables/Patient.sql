CREATE TABLE [dbo].[Patient]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY,
    [FirstName]   NVARCHAR (MAX) NOT NULL,
    [LastName]    NVARCHAR (MAX) NOT NULL,
    [DOB]         DATETIME2      NOT NULL DEFAULT getutcdate(),
    [Email]       NVARCHAR (MAX) NULL,
    [PhoneNumber] NVARCHAR (MAX) NULL,
    
)
