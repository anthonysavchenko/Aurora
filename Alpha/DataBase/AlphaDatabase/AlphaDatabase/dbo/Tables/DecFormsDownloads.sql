CREATE TABLE [dbo].[DecFormsDownloads] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Created]          DATETIME2 (0)  NOT NULL,
    [Author]           INT            NOT NULL,
    [Directory]        NVARCHAR (200) NOT NULL,
    [Note]             NVARCHAR (250) NULL,
    [ErrorDescription] NVARCHAR (MAX) NULL,
    [ExceptionMessage] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_DecFormsDownloads] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DecFormsDownloads_Users] FOREIGN KEY ([Author]) REFERENCES [dbo].[Users] ([ID])
);

