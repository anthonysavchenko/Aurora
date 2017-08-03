CREATE TABLE [dbo].[Residents] (
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]         NVARCHAR (50) NOT NULL,
    [Patronymic]        NVARCHAR (50) NOT NULL,
    [Surname]           NVARCHAR (50) NOT NULL,
    [ResidentDocument]  NVARCHAR (50) NULL,
    [OwnerRelationship] TINYINT       NOT NULL,
    [BenefitType]       INT           NULL,
    [Customer]          INT           NOT NULL,
    CONSTRAINT [PK_Residents] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Residents_BenefitTypes] FOREIGN KEY ([BenefitType]) REFERENCES [dbo].[BenefitTypes] ([ID]),
    CONSTRAINT [FK_Residents_Customers] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID])
);






GO
CREATE NONCLUSTERED INDEX [IX_Customer]
    ON [dbo].[Residents]([Customer] ASC);

