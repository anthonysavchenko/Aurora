CREATE TABLE [dbo].[Services] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Code]        NVARCHAR (50)  NOT NULL,
    [ServiceType] INT            NOT NULL,
    [ChargeRule]  TINYINT        NOT NULL,
    [Norm]        DECIMAL (9, 3) NULL,
    [Measure]     NVARCHAR (10)  NULL,
    CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Services_ServiceTypes] FOREIGN KEY ([ServiceType]) REFERENCES [dbo].[ServiceTypes] ([ID])
);







