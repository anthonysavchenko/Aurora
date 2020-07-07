CREATE TABLE [dbo].[PrivateCounters] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [Model]    NVARCHAR (50) NOT NULL,
    [Number]   NVARCHAR (25) NOT NULL,
    [Customer] INT           NOT NULL,
    CONSTRAINT [PK_PrivateCounters] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrivateCounters_Customers] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID])
);













