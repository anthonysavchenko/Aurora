CREATE TABLE [dbo].[BenefitTypes] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [Code]         NVARCHAR (50) NOT NULL,
    [BenefitRule]  TINYINT       NOT NULL,
    [FixedPercent] TINYINT       NULL,
    CONSTRAINT [PK_BenefitTypes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

