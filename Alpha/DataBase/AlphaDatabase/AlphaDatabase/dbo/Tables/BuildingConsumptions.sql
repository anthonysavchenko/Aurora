CREATE TABLE [dbo].[BuildingConsumptions] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [BuildingID]            INT            NOT NULL,
    [Period]                DATE           NOT NULL,
    [ElectrVol]             NVARCHAR (100) NOT NULL,
    [ElectrOdnVol]          NVARCHAR (100) NOT NULL,
    [ElectrCounterValue]    NVARCHAR (100) NOT NULL,
    [HotWaterVol]           NVARCHAR (100) NOT NULL,
    [HotWaterOdnVol]        NVARCHAR (100) NOT NULL,
    [HotWaterCounterValue]  NVARCHAR (100) NOT NULL,
    [ColdWaterVol]          NVARCHAR (100) NOT NULL,
    [ColdWaterOdnVol]       NVARCHAR (100) NOT NULL,
    [ColdWaterCounterValue] NVARCHAR (100) NOT NULL,
    [WasteWaterVol]         NVARCHAR (100) NOT NULL,
    [WasteWaterOdnVol]      NVARCHAR (100) NOT NULL,
    [HeatingVol]            NVARCHAR (100) NOT NULL,
    [HeatingOdnVol]         NVARCHAR (100) NOT NULL,
    [HeatingCounterValue]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_BuildingConsumptions] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingConsumptions_Buildings] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Buildings] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_BuildingID_Period]
    ON [dbo].[BuildingConsumptions]([BuildingID] ASC, [Period] ASC);

