CREATE TABLE [dbo].[CalculationCustomers] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [Account]       NVARCHAR (25)   NOT NULL,
    [Apartment]     NVARCHAR (10)   NULL,
    [Owner]         NVARCHAR (50)   NULL,
    [CounterType]   TINYINT         NOT NULL,
    [Volume]        DECIMAL (11, 3) NOT NULL,
    [Recalculation] DECIMAL (11, 3) NOT NULL,
    [Square]        DECIMAL (11, 3) NULL,
    [Residents]     TINYINT         NOT NULL,
    CONSTRAINT [PK_CalculationCustomers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

