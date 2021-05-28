CREATE TABLE [dbo].[Settings] (
    [ID]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [Value] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([ID] ASC)
);

