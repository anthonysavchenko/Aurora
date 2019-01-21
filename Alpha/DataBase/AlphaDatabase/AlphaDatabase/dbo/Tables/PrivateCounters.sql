CREATE TABLE [dbo].[PrivateCounters] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Number]     NVARCHAR (50) NOT NULL,
    [CustomerID] INT           NULL,
    [ServiceID]  INT           NULL,
    [Model]      NVARCHAR (50) CONSTRAINT [DF_PrivateCounters_Model] DEFAULT ('') NOT NULL,
    [Archived]   BIT           CONSTRAINT [DF_PrivateCounters_Archived] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PrivateCounters] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrivateCounters_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]),
    CONSTRAINT [FK_PrivateCounters_Services] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Services] ([ID])
);











