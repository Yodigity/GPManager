CREATE TABLE [dbo].[User] (
    [Id]           NVARCHAR (128) NOT NULL,
    [FirstName]    VARCHAR (50)   NOT NULL,
    [LastName]     VARCHAR (50)   NOT NULL,
    [Email] NVARCHAR (256) NOT NULL,
    [CreatedDate]  DATETIME2 (7)  DEFAULT getutcdate() NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
