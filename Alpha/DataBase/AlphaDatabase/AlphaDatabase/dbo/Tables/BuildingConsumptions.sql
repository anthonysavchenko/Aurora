CREATE TABLE [dbo].[BuildingConsumptions] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [BuildingID]            INT            NOT NULL,
    [Period]                DATE           NOT NULL,
    [ElectrVol]             NVARCHAR (255) NOT NULL,
    [ElectrOdnVol]          NVARCHAR (255) NOT NULL,
    [ElectrCounterValue]    NVARCHAR (255) NOT NULL,
    [HotWaterVol]           NVARCHAR (255) NOT NULL,
    [HotWaterOdnVol]        NVARCHAR (255) NOT NULL,
    [HotWaterCounterValue]  NVARCHAR (255) NOT NULL,
    [ColdWaterVol]          NVARCHAR (255) NOT NULL,
    [ColdWaterOdnVol]       NVARCHAR (255) NOT NULL,
    [ColdWaterCounterValue] NVARCHAR (255) NOT NULL,
    [WasteWaterVol]         NVARCHAR (255) NOT NULL,
    [WasteWaterOdnVol]      NVARCHAR (255) NOT NULL,
    [HeatingVol]            NVARCHAR (255) NOT NULL,
    [HeatingOdnVol]         NVARCHAR (255) NOT NULL,
    [HeatingCounterValue]   NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_BuildingConsumptions] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingConsumptions_Buildings] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Buildings] ([ID])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_BuildingID_Period]
    ON [dbo].[BuildingConsumptions]([BuildingID] ASC, [Period] ASC);

