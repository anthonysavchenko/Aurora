CREATE TABLE [dbo].[PrivateValuesUploads] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Created]          DATETIME2 (0)  NOT NULL,
    [Month]            DATETIME2 (0)  NOT NULL,
    [Author]           INT            NOT NULL,
    [Directory]        NVARCHAR (200) NOT NULL,
    [Note]             NVARCHAR (250) NULL,
    [ErrorDescription] NVARCHAR (MAX) NULL,
    [ExceptionMessage] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_PrivateValuesUploads] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrivateValuesUploads_Users] FOREIGN KEY ([Author]) REFERENCES [dbo].[Users] ([ID])
);

