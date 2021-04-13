﻿CREATE TABLE [dbo].[Buildings] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [Street]           NVARCHAR (50)   NOT NULL,
    [Number]           NVARCHAR (25)   NOT NULL,
    [BuildingContract] TINYINT         NOT NULL,
    [NormCoefficient]  DECIMAL (11, 3) NOT NULL,
    [CollectiveSquare] DECIMAL (11, 3) NOT NULL,
    [Note]             NVARCHAR (250)  NULL,
    CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED ([ID] ASC)
);




















GO


