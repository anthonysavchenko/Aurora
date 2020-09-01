CREATE TABLE [dbo].[Customers] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Apartment] NVARCHAR (10) NOT NULL,
    [Account]   NVARCHAR (10) NOT NULL,
    [Building]  INT           NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Customers_Buildings] FOREIGN KEY ([Building]) REFERENCES [dbo].[Buildings] ([ID])
);






















GO



GO


