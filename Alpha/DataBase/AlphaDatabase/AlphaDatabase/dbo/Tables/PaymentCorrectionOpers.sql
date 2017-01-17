CREATE TABLE [dbo].[PaymentCorrectionOpers] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime] DATETIME2 (0)  NOT NULL,
    [Period]           DATE           NOT NULL,
    [Value]            DECIMAL (9, 2) NOT NULL,
    [PaymentOper]      INT            NOT NULL,
    CONSTRAINT [PK_PaymentCorrectionOpers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PaymentCorrectionOpers_PaymentOpers] FOREIGN KEY ([PaymentOper]) REFERENCES [dbo].[PaymentOpers] ([ID])
);