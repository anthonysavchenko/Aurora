GO
PRINT N'Creating [dbo].[FineDocs]...';


GO
CREATE TABLE [dbo].[FineDocs] (
    [ID]     INT  IDENTITY (1, 1) NOT NULL,
    [Period] DATE NOT NULL,
    CONSTRAINT [PK_FineDocs] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[FinePoses]...';


GO
CREATE TABLE [dbo].[FinePoses] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Value]      DECIMAL (9, 2) NOT NULL,
    [CustomerID] INT            NOT NULL,
    [DocID]      INT            NOT NULL,
    CONSTRAINT [PK_Fine] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[ChargeOpers].[IX_ChargeCorrectionOper]...';


GO
CREATE NONCLUSTERED INDEX [IX_ChargeCorrectionOper]
    ON [dbo].[ChargeOpers]([ChargeCorrectionOper] ASC);


GO
PRINT N'Creating [dbo].[PaymentOperPoses].[IX_ServicePeriod]...';


GO
CREATE NONCLUSTERED INDEX [IX_ServicePeriod]
    ON [dbo].[PaymentOperPoses]([Service] ASC, [Period] ASC)
    INCLUDE([Value], [PaymentOper]);


GO
PRINT N'Creating [dbo].[RechargeOpers].[IX_ChargeCorrectinoOper]...';


GO
CREATE NONCLUSTERED INDEX [IX_ChargeCorrectinoOper]
    ON [dbo].[RechargeOpers]([ChargeCorrectionOper] ASC);


GO
PRINT N'Creating [dbo].[FK_FinePoses_FineDocs]...';


GO
ALTER TABLE [dbo].[FinePoses] WITH NOCHECK
    ADD CONSTRAINT [FK_FinePoses_FineDocs] FOREIGN KEY ([DocID]) REFERENCES [dbo].[FineDocs] ([ID]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Fines_Customers]...';


GO
ALTER TABLE [dbo].[FinePoses] WITH NOCHECK
    ADD CONSTRAINT [FK_Fines_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]) ON DELETE CASCADE;


GO
PRINT N'Checking existing data against newly created constraints';

GO
ALTER TABLE [dbo].[FinePoses] WITH CHECK CHECK CONSTRAINT [FK_FinePoses_FineDocs];

ALTER TABLE [dbo].[FinePoses] WITH CHECK CHECK CONSTRAINT [FK_Fines_Customers];


GO
PRINT N'Update complete.';


GO