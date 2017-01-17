CREATE TABLE [dbo].[Users] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [Login]                   NVARCHAR (50)  NOT NULL,
    [Password]                NVARCHAR (MAX)  NOT NULL,
    [Aka]                     NVARCHAR (50)  NOT NULL,
	[SecurityStamp]           NVARCHAR(MAX)  NULL, 
    [LockoutEndDateUtc]       DATETIME2      NULL, 
    [LockoutEnabled]          BIT            NOT NULL DEFAULT 0, 
    [AccessFailedCount]       INT            NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);

