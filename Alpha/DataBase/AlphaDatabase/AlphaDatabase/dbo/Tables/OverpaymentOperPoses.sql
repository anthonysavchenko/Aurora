CREATE TABLE [dbo].[OverpaymentOperPoses] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [Period]          DATE           NOT NULL,
    [Value]           DECIMAL (9, 2) NOT NULL,
    [Service]         INT            NOT NULL,
    [OverpaymentOper] INT            NOT NULL,
    CONSTRAINT [PK_OverpaymentOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OverpaymentOperPoses_OverpaymentOpers] FOREIGN KEY ([OverpaymentOper]) REFERENCES [dbo].[OverpaymentOpers] ([ID]),
    CONSTRAINT [FK_OverpaymentOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Service_Period]
    ON [dbo].[OverpaymentOperPoses]([Period] ASC, [Service] ASC)
    INCLUDE([Value], [OverpaymentOper]);

