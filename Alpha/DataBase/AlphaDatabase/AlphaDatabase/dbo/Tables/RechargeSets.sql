CREATE TABLE [dbo].[RechargeSets] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [CreationDateTime] DATETIME2 (0)   NOT NULL,
    [Period]           DATE            NOT NULL,
    [Number]           INT             NOT NULL,
    [Quantity]         INT             NOT NULL,
    [ValueSum]         DECIMAL (18, 2) NOT NULL,
    [Comment]          VARCHAR (256)   NULL,
    [Author]           INT             NOT NULL,
    CONSTRAINT [PK_RechargeSets] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RechargeSets_Users] FOREIGN KEY ([Author]) REFERENCES [dbo].[Users] ([ID])
);

