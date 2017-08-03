CREATE TABLE [dbo].[CommonCounterValues] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [Period]        DATE            NOT NULL,
    [Value]         DECIMAL (10, 3) NOT NULL,
    [CommonCounter] INT             NOT NULL,
    CONSTRAINT [PK_CommonCounterValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CommonCounterValues_CommonCounters] FOREIGN KEY ([CommonCounter]) REFERENCES [dbo].[CommonCounters] ([ID])
);



