CREATE TABLE [dbo].[CalculationLegalEntities] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [Contract]      NVARCHAR (25)   NOT NULL,
    [EntityName]    NVARCHAR (200)  NULL,
    [ChargedVolume] DECIMAL (11, 3) NOT NULL,
    [Square]        DECIMAL (11, 3) NULL,
    CONSTRAINT [PK_CalculationLegalEntities] PRIMARY KEY CLUSTERED ([ID] ASC)
);

