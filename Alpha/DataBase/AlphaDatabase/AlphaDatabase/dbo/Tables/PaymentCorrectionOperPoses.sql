CREATE TABLE [dbo].[PaymentCorrectionOperPoses] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [Value]                 DECIMAL (9, 2) NOT NULL,
    [Service]               INT            NOT NULL,
    [PaymentCorrectionOper] INT            NOT NULL,
    CONSTRAINT [PK_PaymentCorrectionOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PaymentCorrectionOperPoses_PaymentCorrectionOpers] FOREIGN KEY ([PaymentCorrectionOper]) REFERENCES [dbo].[PaymentCorrectionOpers] ([ID]),
    CONSTRAINT [FK_PaymentCorrectionOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);

