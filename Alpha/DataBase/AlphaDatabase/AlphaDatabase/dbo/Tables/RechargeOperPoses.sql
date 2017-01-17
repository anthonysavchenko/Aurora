CREATE TABLE [dbo].[RechargeOperPoses] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Value]        DECIMAL (9, 2) NOT NULL,
    [Service]      INT            NOT NULL,
    [Contractor]   INT            NOT NULL,
    [RechargeOper] INT            NOT NULL,
    CONSTRAINT [PK_RechargeOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RechargeOperPoses_Contractors] FOREIGN KEY ([Contractor]) REFERENCES [dbo].[Contractors] ([ID]),
    CONSTRAINT [FK_RechargeOperPoses_RechargeOpers] FOREIGN KEY ([RechargeOper]) REFERENCES [dbo].[RechargeOpers] ([ID]),
    CONSTRAINT [FK_RechargeOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);






GO
CREATE NONCLUSTERED INDEX [IX_Service]
    ON [dbo].[RechargeOperPoses]([Service] ASC)
    INCLUDE([Value], [RechargeOper]);

