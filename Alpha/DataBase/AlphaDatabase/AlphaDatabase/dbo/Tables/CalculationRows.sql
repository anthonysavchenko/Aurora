CREATE TABLE [dbo].[CalculationRows] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [CalculationForm]    INT            NOT NULL,
    [ProcessingResult]   TINYINT        NOT NULL,
    [RowType]            TINYINT        NOT NULL,
    [BuildingAddressRow] INT            NULL,
    [BuildingInfo]       INT            NULL,
    [BuildingCounter]    INT            NULL,
    [Customer]           INT            NULL,
    [LegalEntity]        INT            NULL,
    [ErrorDescription]   NVARCHAR (MAX) NULL,
    [ExceptionMessage]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CalculationRows] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CalculationRows_CalculationBuildingCounters] FOREIGN KEY ([BuildingCounter]) REFERENCES [dbo].[CalculationBuildingCounters] ([ID]),
    CONSTRAINT [FK_CalculationRows_CalculationBuildingInfos] FOREIGN KEY ([BuildingInfo]) REFERENCES [dbo].[CalculationBuildingInfos] ([ID]),
    CONSTRAINT [FK_CalculationRows_CalculationCustomers] FOREIGN KEY ([Customer]) REFERENCES [dbo].[CalculationCustomers] ([ID]),
    CONSTRAINT [FK_CalculationRows_CalculationForms] FOREIGN KEY ([CalculationForm]) REFERENCES [dbo].[CalculationForms] ([ID]),
    CONSTRAINT [FK_CalculationRows_CalculationLegalEntities] FOREIGN KEY ([LegalEntity]) REFERENCES [dbo].[CalculationLegalEntities] ([ID]),
    CONSTRAINT [FK_CalculationRows_CalculationRows] FOREIGN KEY ([BuildingAddressRow]) REFERENCES [dbo].[CalculationRows] ([ID])
);

