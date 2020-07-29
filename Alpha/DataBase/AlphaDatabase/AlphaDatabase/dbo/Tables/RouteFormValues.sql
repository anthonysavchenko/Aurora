CREATE TABLE [dbo].[RouteFormValues] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [RouteFormPos]   INT           NOT NULL,
    [PrivateCounter] INT           NOT NULL,
    [Month]          DATETIME2 (0) NOT NULL,
    [ValueType]      TINYINT       NOT NULL,
    [Value]          INT           NULL,
    CONSTRAINT [PK_RouteFormValues] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RouteFormValues_PrivateCounters] FOREIGN KEY ([PrivateCounter]) REFERENCES [dbo].[PrivateCounters] ([ID]),
    CONSTRAINT [FK_RouteFormValues_RouteFormPoses] FOREIGN KEY ([RouteFormPos]) REFERENCES [dbo].[RouteFormPoses] ([ID])
);

