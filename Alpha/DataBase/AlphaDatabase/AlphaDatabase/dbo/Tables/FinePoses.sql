CREATE TABLE [dbo].[FinePoses] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Value]      DECIMAL (9, 2) NOT NULL,
    [CustomerID] INT            NOT NULL,
    [DocID]      INT            NOT NULL,
    CONSTRAINT [PK_Fine] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_FinePoses_FineDocs] FOREIGN KEY ([DocID]) REFERENCES [dbo].[FineDocs] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Fines_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]) ON DELETE CASCADE
);

