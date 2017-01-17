CREATE TABLE [dbo].[TotalBillDocPoses] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [TotalBillDoc]    INT            NOT NULL,
    [ServiceTypeName] NVARCHAR (50)  NOT NULL,
    [Value]           DECIMAL (9, 2) NOT NULL,
    [TotalCharged]    DECIMAL (9, 2) NOT NULL,
    [TotalPaid]       DECIMAL (9, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [TotalBillDocPoses_fk] FOREIGN KEY ([TotalBillDoc]) REFERENCES [dbo].[TotalBillDocs] ([ID])
);



