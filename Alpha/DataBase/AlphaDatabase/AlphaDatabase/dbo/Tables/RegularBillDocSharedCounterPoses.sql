CREATE TABLE [dbo].[RegularBillDocSharedCounterPoses] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [RegularBillDoc]     INT            NOT NULL,
    [SharedCounterValue] DECIMAL (9, 2) NOT NULL,
    [SharedCharge]       DECIMAL (9, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [RegularBillDocSharedCounterPoses_fk] FOREIGN KEY ([RegularBillDoc]) REFERENCES [dbo].[RegularBillDocs] ([ID])
);



