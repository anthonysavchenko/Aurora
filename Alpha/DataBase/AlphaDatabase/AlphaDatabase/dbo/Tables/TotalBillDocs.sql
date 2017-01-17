CREATE TABLE [dbo].[TotalBillDocs] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Customer]         INT            NOT NULL,
    [CreationDateTime] DATETIME2 (0)  NOT NULL,
    [Account]          NVARCHAR (50)  NOT NULL,
    [Owner]            NVARCHAR (50)  NOT NULL,
    [Address]          NVARCHAR (50)  NOT NULL,
    [Square]           NVARCHAR (50)  NOT NULL,
    [ResidentsCount]   INT            NOT NULL,
    [Value]            DECIMAL (9, 2) NOT NULL,
    [BillSet]          INT            NOT NULL,
    [Period]           DATE           NOT NULL,
    [StartPeriod]      DATE           NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [TotalBillDocs_fk] FOREIGN KEY ([BillSet]) REFERENCES [dbo].[BillSets] ([ID]),
    CONSTRAINT [TotalBillDocs_fk2] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID])
);

