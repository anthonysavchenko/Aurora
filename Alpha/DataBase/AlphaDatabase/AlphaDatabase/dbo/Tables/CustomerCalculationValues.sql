CREATE TABLE [dbo].[CustomerCalculationValues] (
    [ID]             INT             IDENTITY (1, 1) NOT NULL,
    [CalculationRow] INT             NOT NULL,
    [Customer]       INT             NOT NULL,
    [Month]          DATETIME2 (0)   NOT NULL,
    [ValueType]      TINYINT         NOT NULL,
    [Volume]         DECIMAL (11, 3) NOT NULL,
    [Recalculation]  DECIMAL (11, 3) NOT NULL,
    CONSTRAINT [PK_CustomerCalculationValues_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CustomerCalculationValues_CalculationRows] FOREIGN KEY ([CalculationRow]) REFERENCES [dbo].[CalculationRows] ([ID]),
    CONSTRAINT [FK_CustomerCalculationValues_Customers] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID])
);

