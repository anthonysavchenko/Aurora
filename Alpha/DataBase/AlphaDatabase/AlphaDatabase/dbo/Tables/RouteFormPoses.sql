CREATE TABLE [dbo].[RouteFormPoses] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [RouteForm]       INT            NOT NULL,
    [Apartment]       NVARCHAR (10)  NOT NULL,
    [Account]         NVARCHAR (25)  NOT NULL,
    [CounterType]     TINYINT        NOT NULL,
    [CounterNumber]   NVARCHAR (25)  NULL,
    [PrevDate]        DATETIME2 (0)  NULL,
    [PrevValue]       DECIMAL (8, 3) NULL,
    [PrevDayValue]    DECIMAL (8, 3) NULL,
    [PrevNightValue]  DECIMAL (8, 3) NULL,
    [Owner]           NVARCHAR (50)  NULL,
    [CounterCapacity] NVARCHAR (2)   NULL,
    [Debt]            NVARCHAR (25)  NULL,
    [Payed]           NVARCHAR (25)  NULL,
    [Phone]           NVARCHAR (25)  NULL,
    [Note]            NVARCHAR (5)   NULL,
    CONSTRAINT [PK_RouteFormPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RouteFormPoses_RouteForms] FOREIGN KEY ([RouteForm]) REFERENCES [dbo].[RouteForms] ([ID])
);









