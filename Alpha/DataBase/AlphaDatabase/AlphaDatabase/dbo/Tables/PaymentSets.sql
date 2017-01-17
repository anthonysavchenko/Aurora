CREATE TABLE [dbo].[PaymentSets] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [CreationDateTime] DATETIME2 (0)  NOT NULL,
    [Number]           INT            NOT NULL,
    [IsFile]           BIT            NOT NULL,
    [Quantity]         SMALLINT       NOT NULL,
    [ValueSum]         DECIMAL (9, 2) NOT NULL,
    [Comment]          NVARCHAR (256) NULL,
    [Intermediary]     INT            NULL,
    [Author]           INT            NOT NULL,
    [PaymentDate]      DATE           NOT NULL,
    CONSTRAINT [PK_PaymentSets] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PaymentSets_Intermediaries] FOREIGN KEY ([Intermediary]) REFERENCES [dbo].[Intermediaries] ([ID]),
    CONSTRAINT [FK_PaymentSets_Users] FOREIGN KEY ([Author]) REFERENCES [dbo].[Users] ([ID])
);




