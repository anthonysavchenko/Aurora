CREATE TABLE [dbo].[BenefitCorrectionOpers] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [Value]                DECIMAL (9, 2) NOT NULL,
    [ChargeCorrectionOper] INT            NOT NULL,
    CONSTRAINT [PK_BenefitCorrectionOpers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [BenefitCorrectionOpers_fk] FOREIGN KEY ([ChargeCorrectionOper]) REFERENCES [dbo].[ChargeCorrectionOpers] ([ID])
);

