CREATE TABLE [dbo].[Contractors] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    [Code] NVARCHAR (50) NOT NULL,
    [ContactInfo] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Contractors] PRIMARY KEY CLUSTERED ([ID] ASC)
);

