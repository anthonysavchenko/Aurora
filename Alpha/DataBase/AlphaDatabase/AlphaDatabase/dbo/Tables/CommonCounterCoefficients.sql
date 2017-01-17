CREATE TABLE [dbo].[CommonCounterCoefficients] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [Period]        DATE            NOT NULL,
    [Coefficient]   DECIMAL (18, 9) NOT NULL,
    [CommonCounter] INT             NOT NULL,
    CONSTRAINT [PK_CommonCounterCoefficients] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CommonCounterCoefficients_CommonCounters] FOREIGN KEY ([CommonCounter]) REFERENCES [dbo].[CommonCounters] ([ID])
);

