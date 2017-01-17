CREATE TABLE [dbo].[BillSets] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [CreationDateTime] DATETIME2 (0)   NOT NULL,
    [Number]           INT             NOT NULL,
    [BillType]         TINYINT         CONSTRAINT [DF__ReceiptSet__Type__14270015] DEFAULT ((0)) NOT NULL,
    [Quantity]         SMALLINT        DEFAULT ((0)) NOT NULL,
    [ValueSum]         DECIMAL (19, 2) CONSTRAINT [DF__ReceiptSe__Value__1BC821DD] DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [ReceiptSets_uq] UNIQUE NONCLUSTERED ([Number] ASC)
);