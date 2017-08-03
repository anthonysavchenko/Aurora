USE [AlphaDataBase]
GO

ALTER TABLE [dbo].[PrivateCounterValues] DROP CONSTRAINT [FK_PrivateCounterValues_PrivateCounters];


GO
PRINT N'Dropping [dbo].[FK_PrivateCounters_CustomerPoses]...';


GO
ALTER TABLE [dbo].[PrivateCounters] DROP CONSTRAINT [FK_PrivateCounters_CustomerPoses];


GO
PRINT N'Altering [dbo].[CommonCounterValues]...';


GO
ALTER TABLE [dbo].[CommonCounterValues] ALTER COLUMN [Value] DECIMAL (10, 3) NOT NULL;


GO
PRINT N'Altering [dbo].[CustomerPoses]...';


GO
ALTER TABLE [dbo].[CustomerPoses]
    ADD [PrivateCounterID] INT NULL;


GO
PRINT N'Altering [dbo].[PrivateCounters]...';


GO
ALTER TABLE [dbo].[PrivateCounters] DROP COLUMN [CustomerPos], COLUMN [Rate];


GO
ALTER TABLE [dbo].[PrivateCounters]
    ADD [CustomerID] INT NULL,
        [ServiceID]  INT NULL;


GO
PRINT N'Altering [dbo].[PrivateCounterValues]...';


GO
ALTER TABLE [dbo].[PrivateCounterValues] ALTER COLUMN [Value] DECIMAL (10, 3) NOT NULL;


GO
PRINT N'Altering [dbo].[RegularBillDocCounterPoses]...';


GO
ALTER TABLE [dbo].[RegularBillDocCounterPoses]
    ADD [ServiceName] NVARCHAR (50) CONSTRAINT [DF_RegularBillDocCounterPoses_ServiceName] DEFAULT ('') NOT NULL,
        [Measure]     NVARCHAR (10) CONSTRAINT [DF_RegularBillDocCounterPoses_Measure] DEFAULT ('') NOT NULL;


GO
PRINT N'Creating [dbo].[FK_PrivateCounterValues_PrivateCounters]...';


GO
ALTER TABLE [dbo].[PrivateCounterValues] WITH NOCHECK
    ADD CONSTRAINT [FK_PrivateCounterValues_PrivateCounters] FOREIGN KEY ([PrivateCounter]) REFERENCES [dbo].[PrivateCounters] ([ID]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_CustomerPoses_PrivateCounters]...';


GO
ALTER TABLE [dbo].[CustomerPoses] WITH NOCHECK
    ADD CONSTRAINT [FK_CustomerPoses_PrivateCounters] FOREIGN KEY ([PrivateCounterID]) REFERENCES [dbo].[PrivateCounters] ([ID]);


GO
PRINT N'Creating [dbo].[FK_PrivateCounters_Customers]...';


GO
ALTER TABLE [dbo].[PrivateCounters] WITH NOCHECK
    ADD CONSTRAINT [FK_PrivateCounters_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]);


GO
PRINT N'Creating [dbo].[FK_PrivateCounters_Services]...';


GO
ALTER TABLE [dbo].[PrivateCounters] WITH NOCHECK
    ADD CONSTRAINT [FK_PrivateCounters_Services] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Services] ([ID]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [dbo].[PrivateCounterValues] WITH CHECK CHECK CONSTRAINT [FK_PrivateCounterValues_PrivateCounters];

ALTER TABLE [dbo].[CustomerPoses] WITH CHECK CHECK CONSTRAINT [FK_CustomerPoses_PrivateCounters];

ALTER TABLE [dbo].[PrivateCounters] WITH CHECK CHECK CONSTRAINT [FK_PrivateCounters_Customers];

ALTER TABLE [dbo].[PrivateCounters] WITH CHECK CHECK CONSTRAINT [FK_PrivateCounters_Services];


GO
PRINT N'Update complete.';

GO