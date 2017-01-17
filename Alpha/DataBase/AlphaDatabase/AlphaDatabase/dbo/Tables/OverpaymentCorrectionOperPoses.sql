CREATE TABLE [dbo].[OverpaymentCorrectionOperPoses] (
    [ID]                        INT            IDENTITY (1, 1) NOT NULL,
    [Value]                     DECIMAL (9, 2) NOT NULL,
    [Service]                   INT            NOT NULL,
    [OverpaymentCorrectionOper] INT            NOT NULL,
    CONSTRAINT [PK_OverpaymentCorrectionOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OverpaymentCorrectionOperPoses_OverpaymentCorrectionOpers] FOREIGN KEY ([OverpaymentCorrectionOper]) REFERENCES [dbo].[OverpaymentCorrectionOpers] ([ID]),
    CONSTRAINT [FK_OverpaymentCorrectionOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);




GO
CREATE NONCLUSTERED INDEX [IX_Service]
    ON [dbo].[OverpaymentCorrectionOperPoses]([Service] ASC)
    INCLUDE([Value], [OverpaymentCorrectionOper]);

