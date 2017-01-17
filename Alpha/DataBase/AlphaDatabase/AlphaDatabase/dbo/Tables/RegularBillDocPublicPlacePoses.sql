CREATE TABLE [dbo].[RegularBillDocPublicPlacePoses] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Service]          NVARCHAR (50)  NOT NULL,
    [Norm]             DECIMAL (9, 3) NOT NULL,
    [NormMeasure]      NVARCHAR (10)  NOT NULL,
    [Rate]             DECIMAL (9, 2) NOT NULL,
    [Area]             DECIMAL (9, 2) NOT NULL,
    [ServiceVolume]    DECIMAL (9, 3) NOT NULL,
    [Total]            DECIMAL (9, 2) NOT NULL,
    [RegularBillDocID] INT            NOT NULL,
    CONSTRAINT [PK_RegularBillDocPublicPlacePoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RegularBillDocPublicPlacePoses_RegularBillDocs] FOREIGN KEY ([RegularBillDocID]) REFERENCES [dbo].[RegularBillDocs] ([ID])
);

