CREATE TABLE [dbo].[LegalEntities] (
    [ID]       INT          IDENTITY (1, 1) NOT NULL,
    [Building] INT          NOT NULL,
    [Contract] VARCHAR (25) NOT NULL,
    CONSTRAINT [PK_LegalEntities] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_LegalEntities_Buildings] FOREIGN KEY ([Building]) REFERENCES [dbo].[Buildings] ([ID])
);

