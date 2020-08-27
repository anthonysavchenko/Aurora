CREATE TABLE [dbo].[BuildingCounters] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [CounterNumber]  NVARCHAR (25) NOT NULL,
    [UtilityService] TINYINT       NOT NULL,
    [Coefficient]    TINYINT       NOT NULL,
    [Building]       INT           NOT NULL,
    [CheckedSince]   DATETIME2 (0) NULL,
    [CheckedTill]    DATETIME2 (0) NULL,
    CONSTRAINT [PK_BuildingCounters] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingCounters_Buildings] FOREIGN KEY ([Building]) REFERENCES [dbo].[Buildings] ([ID])
);

