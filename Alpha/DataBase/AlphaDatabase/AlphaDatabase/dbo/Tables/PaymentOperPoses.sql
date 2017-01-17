CREATE TABLE [dbo].[PaymentOperPoses] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Period]      DATE           NOT NULL,
    [Value]       DECIMAL (9, 2) NOT NULL,
    [Service]     INT            NOT NULL,
    [PaymentOper] INT            NOT NULL,
    CONSTRAINT [PK_PaymentOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PaymentOperPoses_PaymentOpers] FOREIGN KEY ([PaymentOper]) REFERENCES [dbo].[PaymentOpers] ([ID]),
    CONSTRAINT [FK_PaymentOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);




GO
CREATE NONCLUSTERED INDEX [IX_PaymentOper]
    ON [dbo].[PaymentOperPoses]([PaymentOper] ASC)
    INCLUDE([Period], [Value], [Service]);


GO
CREATE NONCLUSTERED INDEX [IX_Period]
    ON [dbo].[PaymentOperPoses]([Period] ASC)
    INCLUDE([Value], [Service], [PaymentOper]);

