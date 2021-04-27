CREATE TABLE [dbo].[BuildingValuesRows] (
    [ID]                 INT             IDENTITY (1, 1) NOT NULL,
    [BuildingValuesForm] INT             NOT NULL,
    [ProcessingResult]   TINYINT         NOT NULL,
    [Street]             NVARCHAR (50)   NULL,
    [Building]           NVARCHAR (25)   NULL,
    [CounterNumber]      NVARCHAR (25)   NULL,
    [Coefficient]        TINYINT         NULL,
    [CurrentValue]       DECIMAL (11, 3) NULL,
    [PrevValue]          DECIMAL (11, 3) NULL,
    [ErrorDescription]   NVARCHAR (MAX)  NULL,
    [ExceptionMessage]   NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_BuildingValuesRows] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingValuesRows_BuildingValuesForms] FOREIGN KEY ([BuildingValuesForm]) REFERENCES [dbo].[BuildingValuesForms] ([ID])
);

