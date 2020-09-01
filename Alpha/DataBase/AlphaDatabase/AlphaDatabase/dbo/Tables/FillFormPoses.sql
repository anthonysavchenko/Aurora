CREATE TABLE [dbo].[FillFormPoses] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [FillForm]        INT           NOT NULL,
    [Apartment]       NVARCHAR (10) NOT NULL,
    [Account]         NVARCHAR (25) NOT NULL,
    [CounterType]     TINYINT       NOT NULL,
    [CounterModel]    NVARCHAR (50) NULL,
    [CounterNumber]   NVARCHAR (25) NULL,
    [PrevDate]        DATETIME2 (0) NULL,
    [PrevValue]       INT           NULL,
    [PrevDayValue]    INT           NULL,
    [PrevNightValue]  INT           NULL,
    [CounterCapacity] NVARCHAR (2)  NULL,
    CONSTRAINT [PK_FillFormPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_FillFormPoses_FillForms] FOREIGN KEY ([FillForm]) REFERENCES [dbo].[FillForms] ([ID])
);



