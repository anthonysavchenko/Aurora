CREATE TABLE [dbo].[BankDetails] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (150) NOT NULL,
    [BIK]         CHAR (9)       NOT NULL,
    [KPP]         VARCHAR (9)    NULL,
    [CorrAccount] VARCHAR (20)   NULL,
    [Account]     CHAR (20)      NOT NULL,
    [INN]         VARCHAR (10)   NULL,
    CONSTRAINT [PK_Requisites] PRIMARY KEY CLUSTERED ([ID] ASC)
);







