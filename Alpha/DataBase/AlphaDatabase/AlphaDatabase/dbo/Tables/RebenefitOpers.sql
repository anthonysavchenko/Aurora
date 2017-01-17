CREATE TABLE [dbo].[RebenefitOpers] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [Value]                 DECIMAL (9, 2) NOT NULL,
    [RechargeOper]          INT            NOT NULL,
    [BenefitCorrectionOper] INT            NULL,
    CONSTRAINT [PK_RebenefitOpers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RebenefitOpers_BenefitCorrectionOpers] FOREIGN KEY ([BenefitCorrectionOper]) REFERENCES [dbo].[BenefitCorrectionOpers] ([ID]),
    CONSTRAINT [FK_RebenefitOpers_RechargeOpers] FOREIGN KEY ([RechargeOper]) REFERENCES [dbo].[RechargeOpers] ([ID])
);





