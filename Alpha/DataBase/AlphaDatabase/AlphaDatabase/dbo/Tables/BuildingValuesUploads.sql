CREATE TABLE [dbo].[BuildingValuesUploads] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Created]          DATETIME2 (0)  NOT NULL,
    [Month]            DATETIME2 (0)  NOT NULL,
    [Author]           INT            NOT NULL,
    [FilePath]         NVARCHAR (400) NOT NULL,
    [Note]             NVARCHAR (250) NULL,
    [ErrorDescription] NVARCHAR (MAX) NULL,
    [ExceptionMessage] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_BuildingValuesUploads] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingValuesUploads_Users] FOREIGN KEY ([Author]) REFERENCES [dbo].[Users] ([ID])
);



