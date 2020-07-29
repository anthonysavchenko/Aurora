CREATE TABLE [dbo].[PrivateCounterValues] (
    [ID]                   INT           IDENTITY (1, 1) NOT NULL,
    [PrivateValuesFormPos] INT           NOT NULL,
    [PrivateCounter]       INT           NOT NULL,
    [Month]                DATETIME2 (0) NOT NULL,
    [ValueType]            TINYINT       NOT NULL,
    [Value]                INT           NULL,
    CONSTRAINT [PK_PrivateCounterValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrivateCounterValues_PrivateCounters] FOREIGN KEY ([PrivateCounter]) REFERENCES [dbo].[PrivateCounters] ([ID]),
    CONSTRAINT [FK_PrivateCounterValues_PrivateValuesFormPoses] FOREIGN KEY ([PrivateValuesFormPos]) REFERENCES [dbo].[PrivateValuesFormPoses] ([ID])
);

