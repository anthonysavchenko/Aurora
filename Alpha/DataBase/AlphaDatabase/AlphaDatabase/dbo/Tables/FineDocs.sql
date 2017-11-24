CREATE TABLE [dbo].[FineDocs] (
    [ID]     INT  IDENTITY (1, 1) NOT NULL,
    [Period] DATE NOT NULL,
    CONSTRAINT [PK_FineDocs] PRIMARY KEY CLUSTERED ([ID] ASC)
);

