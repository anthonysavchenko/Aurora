CREATE TABLE [dbo].[PublicPlaceServiceVolumes] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Period]     DATE           NOT NULL,
    [Volume]     DECIMAL (9, 3) NOT NULL,
    [ServiceID]  INT            NOT NULL,
    [BuildingID] INT            NOT NULL,
    CONSTRAINT [PK_PublicPlaceServiceVolumes] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicPlaceServiceVolumes_Buildings] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Buildings] ([ID]),
    CONSTRAINT [FK_PublicPlaceServiceVolumes_Services] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Services] ([ID])
);

