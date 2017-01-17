CREATE TABLE [dbo].[ChargeSets] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [CreationDateTime] DATETIME2 (0)   NOT NULL,
    [Period]           DATE            NOT NULL,
    [Number]           INT             NOT NULL,
    [Quantity]         INT             NOT NULL,
    [ValueSum]         DECIMAL (18, 2) NOT NULL,
    [Author]           INT             NOT NULL,
    CONSTRAINT [PK_ChargeSet] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ChargeSets_Users] FOREIGN KEY ([Author]) REFERENCES [dbo].[Users] ([ID])
);



