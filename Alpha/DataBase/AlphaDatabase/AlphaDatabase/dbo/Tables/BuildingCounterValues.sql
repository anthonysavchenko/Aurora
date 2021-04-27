CREATE TABLE [dbo].[BuildingCounterValues] (
    [ID]                INT             IDENTITY (1, 1) NOT NULL,
    [BuildingValuesRow] INT             NOT NULL,
    [BuildingCounter]   INT             NOT NULL,
    [Month]             DATETIME2 (0)   NOT NULL,
    [PrevValue]         DECIMAL (11, 3) NULL,
    [CurrentValue]      DECIMAL (11, 3) NULL,
    CONSTRAINT [PK_BuildingCounterValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingCounterValues_BuildingCounters] FOREIGN KEY ([BuildingCounter]) REFERENCES [dbo].[BuildingCounters] ([ID]),
    CONSTRAINT [FK_BuildingCounterValues_BuildingValuesRows] FOREIGN KEY ([BuildingValuesRow]) REFERENCES [dbo].[BuildingValuesRows] ([ID])
);







