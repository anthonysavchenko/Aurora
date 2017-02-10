CREATE TABLE [dbo].[PrivateCounterValues] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [Period]         DATE           NOT NULL,
    [Value]          DECIMAL (9, 2) NOT NULL,
    [PrivateCounter] INT            NOT NULL,
    CONSTRAINT [PK_PrivateCounterValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrivateCounterValues_PrivateCounters] FOREIGN KEY ([PrivateCounter]) REFERENCES [dbo].[PrivateCounters] ([ID]) ON DELETE CASCADE
);



