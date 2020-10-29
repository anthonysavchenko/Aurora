CREATE TABLE [dbo].[BuildingCounterCalculationValues] (
    [ID]              INT             IDENTITY (1, 1) NOT NULL,
    [CalculationRow]  INT             NOT NULL,
    [BuildingCounter] INT             NOT NULL,
    [Month]           DATETIME2 (0)   NOT NULL,
    [PrevValue]       DECIMAL (11, 3) NOT NULL,
    [CurrentValue]    DECIMAL (11, 3) NOT NULL,
    CONSTRAINT [PK_BuildingCounterCalculationValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingCounterCalculationValues_BuildingCounters] FOREIGN KEY ([BuildingCounter]) REFERENCES [dbo].[BuildingCounters] ([ID]),
    CONSTRAINT [FK_BuildingCounterCalculationValues_CalculationRows] FOREIGN KEY ([CalculationRow]) REFERENCES [dbo].[CalculationRows] ([ID])
);

