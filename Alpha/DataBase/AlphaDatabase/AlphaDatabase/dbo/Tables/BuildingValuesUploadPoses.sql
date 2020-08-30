CREATE TABLE [dbo].[BuildingValuesUploadPoses] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [BuildingValuesUpload] INT            NOT NULL,
    [Street]               NVARCHAR (50)  NULL,
    [Building]             NVARCHAR (25)  NULL,
    [CounterNumber]        NVARCHAR (25)  NULL,
    [Coefficient]          TINYINT        NULL,
    [CurrentValue]         DECIMAL (8, 3) NULL,
    [PrevValue]            DECIMAL (8, 3) NULL,
    [CurrentDate]          DATETIME2 (0)  NULL,
    [ErrorDescription]     NVARCHAR (MAX) NULL,
    [ExceptionMessage]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_BuildingValuesUploadPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingValuesUploadPoses_BuildingValuesUploads] FOREIGN KEY ([BuildingValuesUpload]) REFERENCES [dbo].[BuildingValuesUploads] ([ID])
);



