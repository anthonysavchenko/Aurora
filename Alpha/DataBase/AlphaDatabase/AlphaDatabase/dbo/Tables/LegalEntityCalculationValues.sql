CREATE TABLE [dbo].[LegalEntityCalculationValues] (
    [ID]             INT             IDENTITY (1, 1) NOT NULL,
    [CalculationRow] INT             NOT NULL,
    [LegalEntity]    INT             NOT NULL,
    [Month]          DATETIME2 (0)   NOT NULL,
    [ChargedVolume]  DECIMAL (11, 3) NOT NULL,
    CONSTRAINT [PK_LegalEntityCalculationValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_LegalEntityCalculationValue_CalculationRows] FOREIGN KEY ([CalculationRow]) REFERENCES [dbo].[CalculationRows] ([ID]),
    CONSTRAINT [FK_LegalEntityCalculationValues_LegalEntities] FOREIGN KEY ([LegalEntity]) REFERENCES [dbo].[LegalEntities] ([ID])
);

