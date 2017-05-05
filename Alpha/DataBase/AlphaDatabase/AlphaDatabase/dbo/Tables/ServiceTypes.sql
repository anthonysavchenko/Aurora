CREATE TABLE [dbo].[ServiceTypes] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (150) NOT NULL,
    [Code] NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_ServiceTypes] PRIMARY KEY CLUSTERED ([ID] ASC)
);



