CREATE TABLE [dbo].[BuildingValuesFiles] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [BuildingValuesUpload] INT            NOT NULL,
    [ProcessingResult]     TINYINT        NOT NULL,
    [FileName]             NVARCHAR (100) NOT NULL,
    [ErrorDescription]     NVARCHAR (MAX) NULL,
    [ExceptionMessage]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_BuildingValuesFiles] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingValuesFiles_BuildingValuesUploads] FOREIGN KEY ([BuildingValuesUpload]) REFERENCES [dbo].[BuildingValuesUploads] ([ID])
);

