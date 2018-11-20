CREATE TABLE [dbo].[RegularBillDocSeviceTypePoses] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [RegularBillDoc]  INT            NOT NULL,
    [ServiceTypeName] NVARCHAR (150) NOT NULL,
    [PayRate]         DECIMAL (9, 2) NOT NULL,
    [Charge]          DECIMAL (9, 2) NOT NULL,
    [Benefit]         DECIMAL (9, 2) NOT NULL,
    [Recalculation]   DECIMAL (9, 2) NOT NULL,
    [Payable]         DECIMAL (9, 2) NOT NULL,
    [ServiceTypeID]   INT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RegularBillDocSeviceTypePoses_ServiceTypes] FOREIGN KEY ([ServiceTypeID]) REFERENCES [dbo].[ServiceTypes] ([ID]),
    CONSTRAINT [RegularBillDocSeviceTypePoses_fk] FOREIGN KEY ([RegularBillDoc]) REFERENCES [dbo].[RegularBillDocs] ([ID])
);








GO
CREATE NONCLUSTERED INDEX [<Name of Missing Index, sysname,>]
    ON [dbo].[RegularBillDocSeviceTypePoses]([RegularBillDoc] ASC)
    INCLUDE([ID], [ServiceTypeName], [PayRate], [Charge], [Benefit], [Recalculation], [Payable], [ServiceTypeID]);

