CREATE TABLE [dbo].[PrivateCounters] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Number]      NVARCHAR (50)  NOT NULL,
    [Rate]        DECIMAL (9, 2) NOT NULL,
    [CustomerPos] INT            NOT NULL,
    CONSTRAINT [PK_PrivateCounters] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrivateCounters_CustomerPoses] FOREIGN KEY ([CustomerPos]) REFERENCES [dbo].[CustomerPoses] ([ID])
);

