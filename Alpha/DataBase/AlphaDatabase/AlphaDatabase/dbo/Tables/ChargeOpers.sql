CREATE TABLE [dbo].[ChargeOpers] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime]     DATETIME2 (0)  NOT NULL,
    [Value]                DECIMAL (9, 2) NOT NULL,
    [Customer]             INT            NOT NULL,
    [ChargeSet]            INT            NOT NULL,
    [ChargeCorrectionOper] INT            NULL,
    [RegularBillDoc]       INT            NULL,
    CONSTRAINT [PK_ChargeOper] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [ChargeOpers_fk] FOREIGN KEY ([ChargeCorrectionOper]) REFERENCES [dbo].[ChargeCorrectionOpers] ([ID]),
    CONSTRAINT [ChargeOpers_fk2] FOREIGN KEY ([RegularBillDoc]) REFERENCES [dbo].[RegularBillDocs] ([ID]),
    CONSTRAINT [FK_ChargeOpers_ChargeSets] FOREIGN KEY ([ChargeSet]) REFERENCES [dbo].[ChargeSets] ([ID]),
    CONSTRAINT [FK_ChargeOpers_Customers] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID])
);








GO
CREATE NONCLUSTERED INDEX [IX_Customer]
    ON [dbo].[ChargeOpers]([Customer] ASC)
    INCLUDE([Value], [ChargeSet]);


GO
CREATE NONCLUSTERED INDEX [IX_ChargeCorrectionOper_Customer]
    ON [dbo].[ChargeOpers]([Customer] ASC, [ChargeCorrectionOper] ASC)
    INCLUDE([ID]);


GO
CREATE NONCLUSTERED INDEX [IX_ChargeSet]
    ON [dbo].[ChargeOpers]([ChargeSet] ASC)
    INCLUDE([ID], [Value], [Customer], [ChargeCorrectionOper]);


GO
CREATE NONCLUSTERED INDEX [IX_ChargeCorrectionOper]
    ON [dbo].[ChargeOpers]([ChargeCorrectionOper] ASC);

