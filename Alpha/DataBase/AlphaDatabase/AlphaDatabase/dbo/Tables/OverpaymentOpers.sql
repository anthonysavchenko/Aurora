CREATE TABLE [dbo].[OverpaymentOpers] (
    [ID]                        INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime]          DATETIME2 (0)  NOT NULL,
    [PaymentPeriod]             DATE           NOT NULL,
    [Value]                     DECIMAL (9, 2) NOT NULL,
    [Customer]                  INT            NOT NULL,
    [OverpaymentCorrectionOper] INT            NULL,
    CONSTRAINT [PK_OverpaymentOper] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OverpaymentOpers_Customers] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID]),
    CONSTRAINT [FK_OverpaymentOpers_OverpaymentCorrectionOpers] FOREIGN KEY ([OverpaymentCorrectionOper]) REFERENCES [dbo].[OverpaymentCorrectionOpers] ([ID])
);