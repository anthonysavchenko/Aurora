CREATE TABLE [dbo].[FillFormValues] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [FillFormPos]    INT           NOT NULL,
    [PrivateCounter] INT           NOT NULL,
    [Month]          DATETIME2 (0) NOT NULL,
    [ValueType]      TINYINT       NOT NULL,
    [Value]          INT           NULL,
    CONSTRAINT [PK_FillFormValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_FillFormValues_FillFormPoses] FOREIGN KEY ([FillFormPos]) REFERENCES [dbo].[FillFormPoses] ([ID]),
    CONSTRAINT [FK_FillFormValues_PrivateCounters] FOREIGN KEY ([PrivateCounter]) REFERENCES [dbo].[PrivateCounters] ([ID])
);

