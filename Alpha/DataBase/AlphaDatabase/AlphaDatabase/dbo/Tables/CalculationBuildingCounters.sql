CREATE TABLE [dbo].[CalculationBuildingCounters] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [CounterNumber] NVARCHAR (25)   NOT NULL,
    [Model]         NVARCHAR (50)   NULL,
    [Coefficient]   TINYINT         NOT NULL,
    [CurrentValue]  DECIMAL (11, 3) NOT NULL,
    [PrevValue]     DECIMAL (11, 3) NOT NULL,
    CONSTRAINT [PK_CalculationBuildingCounters] PRIMARY KEY CLUSTERED ([ID] ASC)
);

