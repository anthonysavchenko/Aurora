CREATE TABLE [dbo].[RegularBillDocs] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [Customer]              INT            NOT NULL,
    [CreationDateTime]      DATETIME2 (0)  NOT NULL,
    [PayBeforeDateTime]     DATE           NOT NULL,
    [Account]               NVARCHAR (50)  NOT NULL,
    [Owner]                 NVARCHAR (50)  NOT NULL,
    [Address]               NVARCHAR (50)  NOT NULL,
    [Square]                NVARCHAR (50)  NOT NULL,
    [ResidentsCount]        INT            NOT NULL,
    [OverpaymentValue]      DECIMAL (9, 2) NOT NULL,
    [MonthChargeValue]      DECIMAL (9, 2) NOT NULL,
    [Value]                 DECIMAL (9, 2) NOT NULL,
    [BillSet]               INT            NOT NULL,
    [Period]                DATE           NOT NULL,
    [EmergencyPhoneNumber]  NCHAR (10)     NULL,
    [ContractorContactInfo] NVARCHAR (100) NULL,
    [BuildingArea]          DECIMAL (9, 2) CONSTRAINT [DF_RegularBillDocs_BuildingArea] DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [RegularBillDocs_fk] FOREIGN KEY ([BillSet]) REFERENCES [dbo].[BillSets] ([ID]),
    CONSTRAINT [RegularBillDocs_fk2] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID])
);



