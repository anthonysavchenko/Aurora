CREATE TABLE [dbo].[ChargeCorrectionOpers] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime] DATETIME2 (0)  NOT NULL,
    [Period]           DATE           NOT NULL,
    [Value]            DECIMAL (9, 2) NOT NULL,
    [RechargeOper]     INT            NULL,
    CONSTRAINT [PK_ChargeCorrectionOpers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ChargeCorrectionOpers_RechargeOpers] FOREIGN KEY ([RechargeOper]) REFERENCES [dbo].[RechargeOpers] ([ID])
);








GO
CREATE NONCLUSTERED INDEX [IX_RechargeOper]
    ON [dbo].[ChargeCorrectionOpers]([RechargeOper] ASC);

