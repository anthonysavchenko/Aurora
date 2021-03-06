PRINT N'Creating [dbo].[RechargePercentCorrections]...';


GO
CREATE TABLE [dbo].[RechargePercentCorrections] (
    [ID]            INT  IDENTITY (1, 1) NOT NULL,
    [Period]        DATE NOT NULL,
    [Days]          INT  NOT NULL,
    [Percent]       INT  NOT NULL,
    [CustomerPosID] INT  NOT NULL,
    CONSTRAINT [PK_RechargePercentCorrections] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[RechargePercentCorrections].[IX_CustomerPos_Period]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CustomerPos_Period]
    ON [dbo].[RechargePercentCorrections]([CustomerPosID] ASC, [Period] ASC)
    INCLUDE([ID], [Days], [Percent]);


GO
PRINT N'Creating [dbo].[FK_RechargePercentCorrections_CustomerPoses]...';


GO
ALTER TABLE [dbo].[RechargePercentCorrections] WITH NOCHECK
    ADD CONSTRAINT [FK_RechargePercentCorrections_CustomerPoses] FOREIGN KEY ([CustomerPosID]) REFERENCES [dbo].[CustomerPoses] ([ID]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [dbo].[RechargePercentCorrections] WITH CHECK CHECK CONSTRAINT [FK_RechargePercentCorrections_CustomerPoses];


GO
PRINT N'Update complete.';


GO
