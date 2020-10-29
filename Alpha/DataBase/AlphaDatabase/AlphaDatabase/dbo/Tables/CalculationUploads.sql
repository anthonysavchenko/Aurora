CREATE TABLE [dbo].[CalculationUploads] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [ProcessingResult] TINYINT        NOT NULL,
    [Created]          DATETIME2 (0)  NOT NULL,
    [Month]            DATETIME2 (0)  NOT NULL,
    [Author]           INT            NOT NULL,
    [DirectoryPath]    NVARCHAR (200) NOT NULL,
    [Note]             NVARCHAR (250) NULL,
    [ErrorDescription] NVARCHAR (MAX) NULL,
    [ExceptionMessage] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CalculationUploads] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CalculationUploads_Users] FOREIGN KEY ([Author]) REFERENCES [dbo].[Users] ([ID])
);

