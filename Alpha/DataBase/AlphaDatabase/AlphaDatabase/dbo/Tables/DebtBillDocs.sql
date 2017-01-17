CREATE TABLE [dbo].[DebtBillDocs] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime] DATETIME2 (0)  NOT NULL,
    [Account]          NVARCHAR (50)  NOT NULL,
    [Address]          NVARCHAR (50)  NOT NULL,
    [Owner]            NVARCHAR (50)  NOT NULL,
    [Value]            DECIMAL (9, 2) NOT NULL,
    [BillSet]          INT            NOT NULL,
    [Period]           DATE           NOT NULL,
    [CustomerID]       INT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [DebtBillDocs_fk] FOREIGN KEY ([BillSet]) REFERENCES [dbo].[BillSets] ([ID]),
    CONSTRAINT [FK_DebtBillDocs_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID])
);
