CREATE TABLE [dbo].[ChargeCorrectionOperPoses] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [Value]                DECIMAL (9, 2) NOT NULL,
    [Service]              INT            NOT NULL,
    [Contractor]           INT            NOT NULL,
    [ChargeCorrectionOper] INT            NOT NULL,
    CONSTRAINT [PK_ChargeCorrectionOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ChargeCorrectionOperPoses_ChargeOpers] FOREIGN KEY ([ChargeCorrectionOper]) REFERENCES [dbo].[ChargeCorrectionOpers] ([ID]),
    CONSTRAINT [FK_ChargeCorrectionOperPoses_Contractors] FOREIGN KEY ([Contractor]) REFERENCES [dbo].[Contractors] ([ID]),
    CONSTRAINT [FK_ChargeCorrectionOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);




GO
CREATE NONCLUSTERED INDEX [IX_ChargeCorrectionOper]
    ON [dbo].[ChargeCorrectionOperPoses]([ChargeCorrectionOper] ASC);

