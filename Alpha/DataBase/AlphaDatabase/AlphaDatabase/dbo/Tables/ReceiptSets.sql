CREATE TABLE [dbo].[ReceiptSets] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime] DATETIME2 (2)  NOT NULL,
    [Number]           INT            NOT NULL,
    [IsDebt]           BIT            CONSTRAINT [DF__ReceiptSet__Type__14270015] DEFAULT ((0)) NOT NULL,
    [Quantity]         SMALLINT       DEFAULT ((0)) NOT NULL,
    [ValueSum]         DECIMAL (9, 2) CONSTRAINT [DF__ReceiptSe__Value__1BC821DD] DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

