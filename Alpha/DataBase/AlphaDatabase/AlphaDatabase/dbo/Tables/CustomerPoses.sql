CREATE TABLE [dbo].[CustomerPoses] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Service]          INT            NOT NULL,
    [Contractor]       INT            NOT NULL,
    [Rate]             DECIMAL (9, 2) NOT NULL,
    [Customer]         INT            NOT NULL,
    [Since]            DATETIME2 (0)  NOT NULL,
    [Till]             DATETIME2 (0)  NOT NULL,
    [PrivateCounterID] INT            NULL,
    CONSTRAINT [PK_CustomerPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CustomerPoses_Contractors] FOREIGN KEY ([Contractor]) REFERENCES [dbo].[Contractors] ([ID]),
    CONSTRAINT [FK_CustomerPoses_CustomerPoses] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerPoses_PrivateCounters] FOREIGN KEY ([PrivateCounterID]) REFERENCES [dbo].[PrivateCounters] ([ID]),
    CONSTRAINT [FK_CustomerPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);














GO
CREATE NONCLUSTERED INDEX [IX_Since_Till]
    ON [dbo].[CustomerPoses]([Since] ASC, [Till] ASC)
    INCLUDE([Service], [Contractor], [Rate], [Customer]);


GO
CREATE NONCLUSTERED INDEX [IX_Service_Since_Till]
    ON [dbo].[CustomerPoses]([Service] ASC, [Since] ASC, [Till] ASC)
    INCLUDE([Rate], [Customer]);


GO
CREATE NONCLUSTERED INDEX [IX_Customer]
    ON [dbo].[CustomerPoses]([Customer] ASC);

