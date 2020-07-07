CREATE TABLE [dbo].[Buildings] (
    [ID]     INT           IDENTITY (1, 1) NOT NULL,
    [Street] NVARCHAR (50) NOT NULL,
    [Number] NVARCHAR (10) NOT NULL,
    CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED ([ID] ASC)
);














GO


