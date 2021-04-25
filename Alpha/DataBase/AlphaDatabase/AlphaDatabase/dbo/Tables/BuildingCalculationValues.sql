CREATE TABLE [dbo].[BuildingCalculationValues] (
    [ID]                    INT             IDENTITY (1, 1) NOT NULL,
    [CalculationRow]        INT             NOT NULL,
    [Building]              INT             NOT NULL,
    [Contract]              TINYINT         NOT NULL,
    [Month]                 DATETIME2 (0)   NOT NULL,
    [CalculationMethod]     TINYINT         NOT NULL,
    [Debt]                  DECIMAL (11, 3) NULL,
    [Volume]                DECIMAL (11, 3) NULL,
    [Norm]                  DECIMAL (11, 3) NULL,
    [CollectiveVolume]      DECIMAL (11, 3) NOT NULL,
    [CollectiveSquare]      DECIMAL (11, 3) NULL,
    [CollectiveVolumeDraft] DECIMAL (11, 3) NULL,
    CONSTRAINT [PK_BuildingCalculationValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingCalculationValues_Buildings] FOREIGN KEY ([Building]) REFERENCES [dbo].[Buildings] ([ID]),
    CONSTRAINT [FK_BuildingCalculationValues_CalculationRows] FOREIGN KEY ([CalculationRow]) REFERENCES [dbo].[CalculationRows] ([ID])
);





