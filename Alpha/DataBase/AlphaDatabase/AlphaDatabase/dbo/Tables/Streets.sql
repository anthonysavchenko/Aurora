CREATE TABLE [dbo].[Streets] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Streets] PRIMARY KEY CLUSTERED ([ID] ASC)
);

