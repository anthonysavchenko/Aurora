CREATE TABLE [dbo].[BuildingValuesForms] (
    [ID]                 INT IDENTITY (1, 1) NOT NULL,
    [BuildingValuesFile] INT NOT NULL,
    CONSTRAINT [PK_BuildingValuesForms] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BuildingValuesForms_BuildingValuesFiles] FOREIGN KEY ([BuildingValuesFile]) REFERENCES [dbo].[BuildingValuesFiles] ([ID])
);

