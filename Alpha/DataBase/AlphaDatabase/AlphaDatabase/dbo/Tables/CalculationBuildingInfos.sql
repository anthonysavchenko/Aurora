CREATE TABLE [dbo].[CalculationBuildingInfos] (
    [ID]                   INT             IDENTITY (1, 1) NOT NULL,
    [RowType]              TINYINT         NOT NULL,
    [Street]               NVARCHAR (50)   NULL,
    [Building]             NVARCHAR (25)   NULL,
    [CalculationMethod]    TINYINT         NULL,
    [Debt]                 DECIMAL (11, 3) NULL,
    [Volume]               DECIMAL (11, 3) NULL,
    [Norm]                 DECIMAL (11, 3) NULL,
    [CollectiveVolume]     DECIMAL (11, 3) NULL,
    [NotDistributedVolume] DECIMAL (11, 3) NULL,
    [CollectiveSquare]     DECIMAL (11, 3) NULL,
    [PeriodVolume]         DECIMAL (11, 3) NULL,
    [Rest]                 DECIMAL (11, 3) NULL,
    CONSTRAINT [PK_CalculationBuildingInfos] PRIMARY KEY CLUSTERED ([ID] ASC)
);



