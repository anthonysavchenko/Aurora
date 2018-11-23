CREATE TABLE [dbo].[ElectricitySharedCounterVolumes] (
    [ID]         INT             IDENTITY (1, 1) NOT NULL,
    [BuildingID] INT             NOT NULL,
    [Volume]     DECIMAL (10, 3) NOT NULL,
    [Period]     DATE            NOT NULL,
    CONSTRAINT [PK_ElectricitySharedCounterVolumes] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ElectricitySharedCounterVolumes_Buildings] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Buildings] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_BuildingID_Period]
    ON [dbo].[ElectricitySharedCounterVolumes]([BuildingID] ASC, [Period] ASC)
    INCLUDE([Volume]);

