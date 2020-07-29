CREATE TABLE [dbo].[PrivateValuesFormPoses] (
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [PrivateValuesForm] INT           NOT NULL,
    [Apartment]         NVARCHAR (10) NOT NULL,
    [CounterType]       TINYINT       NOT NULL,
    [CounterNumber]     NVARCHAR (25) NULL,
    [CurrentDate]       DATETIME2 (0) NULL,
    [CurrentValue]      INT           NULL,
    [CurrentDayValue]   INT           NULL,
    [CurrentNightValue] INT           NULL,
    CONSTRAINT [PK_PrivateValuesFormPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrivateValuesFormPoses_PrivateValuesForms] FOREIGN KEY ([PrivateValuesForm]) REFERENCES [dbo].[PrivateValuesForms] ([ID])
);

