CREATE TABLE [dbo].[ChargeOperPoses] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Value]      DECIMAL (9, 2) NOT NULL,
    [Service]    INT            NOT NULL,
    [Contractor] INT            NOT NULL,
    [ChargeOper] INT            NOT NULL,
    CONSTRAINT [PK_ChargeOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ChargeOperPoses_ChargeOpers] FOREIGN KEY ([ChargeOper]) REFERENCES [dbo].[ChargeOpers] ([ID]),
    CONSTRAINT [FK_ChargeOperPoses_Contractors] FOREIGN KEY ([Contractor]) REFERENCES [dbo].[Contractors] ([ID]),
    CONSTRAINT [FK_ChargeOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);






GO
CREATE NONCLUSTERED INDEX [IX_ChargeOper]
    ON [dbo].[ChargeOperPoses]([ChargeOper] ASC)
    INCLUDE([Value], [Service]);


GO
CREATE NONCLUSTERED INDEX [IX_Service]
    ON [dbo].[ChargeOperPoses]([Service] ASC)
    INCLUDE([ChargeOper]);

