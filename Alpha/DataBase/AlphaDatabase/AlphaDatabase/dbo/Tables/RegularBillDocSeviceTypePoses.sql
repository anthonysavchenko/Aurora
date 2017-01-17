CREATE TABLE [dbo].[RegularBillDocSeviceTypePoses] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [RegularBillDoc]  INT            NOT NULL,
    [ServiceTypeName] NVARCHAR (50)  NOT NULL,
    [PayRate]         DECIMAL (9, 2) NOT NULL,
    [Charge]          DECIMAL (9, 2) NOT NULL,
    [Benefit]         DECIMAL (9, 2) NOT NULL,
    [Recalculation]   DECIMAL (9, 2) NOT NULL,
    [Payable]         DECIMAL (9, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [RegularBillDocSeviceTypePoses_fk] FOREIGN KEY ([RegularBillDoc]) REFERENCES [dbo].[RegularBillDocs] ([ID])
);



