CREATE TABLE [dbo].[BenefitOpers] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [Value]                 DECIMAL (9, 2) NOT NULL,
    [ChargeOper]            INT            NOT NULL,
    [BenefitCorrectionOper] INT            NULL,
    CONSTRAINT [PK_BenefitOpers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [BenefitOpers_fk] FOREIGN KEY ([BenefitCorrectionOper]) REFERENCES [dbo].[BenefitCorrectionOpers] ([ID]),
    CONSTRAINT [FK_BenefitOpers_ChargeOpers] FOREIGN KEY ([ChargeOper]) REFERENCES [dbo].[ChargeOpers] ([ID])
);







