CREATE TABLE [dbo].[RechargePercentCorrections] (
    [ID]            INT  IDENTITY (1, 1) NOT NULL,
    [Period]        DATE NOT NULL,
    [Days]          INT  NOT NULL,
    [Percent]       INT  NOT NULL,
    [CustomerPosID] INT  NOT NULL,
    CONSTRAINT [PK_RechargePercentCorrections] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RechargePercentCorrections_CustomerPoses] FOREIGN KEY ([CustomerPosID]) REFERENCES [dbo].[CustomerPoses] ([ID]) ON DELETE CASCADE
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CustomerPos_Period]
    ON [dbo].[RechargePercentCorrections]([CustomerPosID] ASC, [Period] ASC)
    INCLUDE([ID], [Days], [Percent]);

