CREATE TABLE [dbo].[RegularBillDocCounterPoses] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [Number]         NVARCHAR (50)  NOT NULL,
    [PrevValue]      DECIMAL (9, 2) NOT NULL,
    [CurValue]       DECIMAL (9, 2) NOT NULL,
    [Consumption]    DECIMAL (9, 2) NOT NULL,
    [Rate]           DECIMAL (9, 2) NOT NULL,
    [RegularBillDoc] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [RegularBillDocCounterPoses_fk] FOREIGN KEY ([RegularBillDoc]) REFERENCES [dbo].[RegularBillDocs] ([ID])
);



