CREATE TABLE [dbo].[Intermediaries] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50)  NOT NULL,
    [Code] NVARCHAR (50)  NOT NULL,
    [Rate] DECIMAL (5, 2) NOT NULL,
    CONSTRAINT [PK_Intermediaries] PRIMARY KEY CLUSTERED ([ID] ASC)
);

