CREATE TABLE [dbo].[OverpaymentCorrectionOpers] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Value]      DECIMAL (9, 2) NOT NULL,
    [ChargeOper] INT            NOT NULL,
    [Period]     DATE           NOT NULL,
    CONSTRAINT [PK_OverpaymentCorrectionOpers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OverpaymentCorrectionOpers_ChargeOpers] FOREIGN KEY ([ChargeOper]) REFERENCES [dbo].[ChargeOpers] ([ID])
);

