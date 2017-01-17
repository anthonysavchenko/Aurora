CREATE TABLE [dbo].[DebtReceipts] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime] DATETIME2 (2)  NOT NULL,
    [Account]          NVARCHAR (50)  NOT NULL,
    [Address]          NVARCHAR (50)  NOT NULL,
    [Owner]            NVARCHAR (50)  NOT NULL,
    [Value]            DECIMAL (9, 2) NOT NULL,
    [ReceiptSet]       INT            NOT NULL,
    [Period]           DATETIME2 (2)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [DebtReceipts_fk] FOREIGN KEY ([ReceiptSet]) REFERENCES [dbo].[ReceiptSets] ([ID])
);

