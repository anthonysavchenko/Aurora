CREATE TABLE [dbo].[Emails] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [DecFormDownload]  INT            NOT NULL,
    [Subject]          NVARCHAR (100) NULL,
    [FromAddress]      NVARCHAR (100) NULL,
    [Received]         DATETIME2 (0)  NULL,
    [ErrorDescription] NVARCHAR (MAX) NULL,
    [ExceptionMessage] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Emails_DecFormsDownloads] FOREIGN KEY ([DecFormDownload]) REFERENCES [dbo].[DecFormsDownloads] ([ID])
);

