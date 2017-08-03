CREATE TABLE [dbo].[PrivateCounters] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Number]     NVARCHAR (50) NOT NULL,
    [CustomerID] INT           NULL,
    [ServiceID]  INT           NULL,
    CONSTRAINT [PK_PrivateCounters] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrivateCounters_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]),
    CONSTRAINT [FK_PrivateCounters_Services] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Services] ([ID])
);