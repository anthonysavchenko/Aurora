CREATE TABLE [dbo].[RechargeOpers] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime]     DATETIME2 (0)  NOT NULL,
    [Value]                DECIMAL (9, 2) NOT NULL,
    [Customer]             INT            NOT NULL,
    [RechargeSet]          INT            NOT NULL,
    [ChargeOper]           INT            NULL,
    [ChargeCorrectionOper] INT            NULL,
    CONSTRAINT [PK_RechargeOper] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RechargeOpers_ChargeCorrectionOpers] FOREIGN KEY ([ChargeCorrectionOper]) REFERENCES [dbo].[ChargeCorrectionOpers] ([ID]),
    CONSTRAINT [FK_RechargeOpers_ChargeOpers] FOREIGN KEY ([ChargeOper]) REFERENCES [dbo].[ChargeOpers] ([ID]),
    CONSTRAINT [FK_RechargeOpers_Customers] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID]),
    CONSTRAINT [FK_RechargeOpers_RechargeSets] FOREIGN KEY ([RechargeSet]) REFERENCES [dbo].[RechargeSets] ([ID])
);








GO
CREATE NONCLUSTERED INDEX [IX_Customer]
    ON [dbo].[RechargeOpers]([Customer] ASC);

