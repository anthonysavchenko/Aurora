CREATE TABLE [dbo].[RegularBillDocCounterPoses] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [Number]         NVARCHAR (50)  NOT NULL,
    [PrevValue]      DECIMAL (9, 2) NOT NULL,
    [CurValue]       DECIMAL (9, 2) NOT NULL,
    [Consumption]    DECIMAL (9, 2) NOT NULL,
    [Rate]           DECIMAL (9, 2) NOT NULL,
    [RegularBillDoc] INT            NOT NULL,
    [ServiceName]    NVARCHAR (50)  CONSTRAINT [DF_RegularBillDocCounterPoses_ServiceName] DEFAULT ('') NOT NULL,
    [Measure]        NVARCHAR (10)  CONSTRAINT [DF_RegularBillDocCounterPoses_Measure] DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [RegularBillDocCounterPoses_fk] FOREIGN KEY ([RegularBillDoc]) REFERENCES [dbo].[RegularBillDocs] ([ID])
);





