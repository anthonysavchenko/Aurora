CREATE TABLE [dbo].[BenefitOperPoses] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [BenefitRule] TINYINT        NOT NULL,
    [Value]       DECIMAL (9, 2) NOT NULL,
    [Service]     INT            NOT NULL,
    [BenefitOper] INT            NOT NULL,
    [Contractor]  INT            NOT NULL,
    CONSTRAINT [PK_BenefitOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BenefitOperPoses_BenefitOpers] FOREIGN KEY ([BenefitOper]) REFERENCES [dbo].[BenefitOpers] ([ID]),
    CONSTRAINT [FK_BenefitOperPoses_Contractors] FOREIGN KEY ([Contractor]) REFERENCES [dbo].[Contractors] ([ID]),
    CONSTRAINT [FK_BenefitOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);






GO
CREATE NONCLUSTERED INDEX [IX_Service]
    ON [dbo].[BenefitOperPoses]([Service] ASC)
    INCLUDE([Value], [BenefitOper]);

