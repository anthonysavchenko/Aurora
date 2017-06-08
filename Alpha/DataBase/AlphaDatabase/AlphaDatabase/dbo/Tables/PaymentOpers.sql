CREATE TABLE [dbo].[PaymentOpers] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime]      DATETIME2 (0)  NOT NULL,
    [PaymentPeriod]         DATE           NOT NULL,
    [Value]                 DECIMAL (9, 2) NOT NULL,
    [Customer]              INT            NOT NULL,
    [PaymentSet]            INT            NOT NULL,
    [PaymentCorrectionOper] INT            NULL,
    CONSTRAINT [PK_PaymentOpers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PaymentOpers_Customers] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID]),
    CONSTRAINT [FK_PaymentOpers_PaymentCorrectionOpers] FOREIGN KEY ([PaymentCorrectionOper]) REFERENCES [dbo].[PaymentCorrectionOpers] ([ID]),
    CONSTRAINT [FK_PaymentOpers_PaymentSets] FOREIGN KEY ([PaymentSet]) REFERENCES [dbo].[PaymentSets] ([ID])
);






GO
CREATE NONCLUSTERED INDEX [IX_Customer]
    ON [dbo].[PaymentOpers]([Customer] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PaymentCorrectionOper]
    ON [dbo].[PaymentOpers]([PaymentCorrectionOper] ASC)
    INCLUDE([PaymentSet], [Value]);



