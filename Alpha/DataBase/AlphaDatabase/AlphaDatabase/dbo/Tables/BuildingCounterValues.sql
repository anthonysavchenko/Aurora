CREATE TABLE [dbo].[BuildingCounterValues] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [BuildingValuesUploadPos] INT            NOT NULL,
    [BuildingCounter]         INT            NOT NULL,
    [Month]                   DATETIME2 (0)  NOT NULL,
    [PrevValue]               DECIMAL (8, 3) NULL,
    [CurrentValue]            DECIMAL (8, 3) NULL,
    [CurrentDate]             DATETIME2 (0)  NULL,
    CONSTRAINT [PK_BuildingCounterValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingCounterValues_BuildingCounters] FOREIGN KEY ([BuildingCounter]) REFERENCES [dbo].[BuildingCounters] ([ID]),
    CONSTRAINT [FK_BuildingCounterValues_BuildingValuesUploadPoses] FOREIGN KEY ([BuildingValuesUploadPos]) REFERENCES [dbo].[BuildingValuesUploadPoses] ([ID])
);



