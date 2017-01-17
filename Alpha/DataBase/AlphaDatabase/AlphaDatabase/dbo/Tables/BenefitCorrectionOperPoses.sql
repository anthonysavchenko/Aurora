CREATE TABLE [dbo].[BenefitCorrectionOperPoses] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [BenefitRule]           TINYINT        NOT NULL,
    [Value]                 DECIMAL (9, 2) NOT NULL,
    [Service]               INT            NOT NULL,
    [BenefitCorrectionOper] INT            NOT NULL,
    [Contractor]            INT            NOT NULL,
    CONSTRAINT [PK_BenefitCorrectionOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BenefitCorrectionOperPoses_BenefitOpers] FOREIGN KEY ([BenefitCorrectionOper]) REFERENCES [dbo].[BenefitCorrectionOpers] ([ID]),
    CONSTRAINT [FK_BenefitCorrectionOperPoses_Contractors] FOREIGN KEY ([Contractor]) REFERENCES [dbo].[Contractors] ([ID]),
    CONSTRAINT [FK_BenefitCorrectionOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);



