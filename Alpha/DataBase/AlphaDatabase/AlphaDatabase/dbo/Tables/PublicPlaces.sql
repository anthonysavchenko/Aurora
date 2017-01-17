CREATE TABLE [dbo].[PublicPlaces] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Area]       DECIMAL (9, 2) NOT NULL,
    [BuildingID] INT            NOT NULL,
    [ServiceID]  INT            NOT NULL,
    CONSTRAINT [PK_PublicPlaces] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicPlaces_Buildings] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Buildings] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_PublicPlaces_Services] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Services] ([ID])
);



