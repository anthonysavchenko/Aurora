CREATE TABLE [dbo].[CalculationFiles] (
    [ID]                INT            IDENTITY (1, 1) NOT NULL,
    [CalculationUpload] INT            NOT NULL,
    [ProcessingResult]  TINYINT        NOT NULL,
    [FileName]          NVARCHAR (100) NOT NULL,
    [Contract]          TINYINT        NOT NULL,
    [ErrorDescription]  NVARCHAR (MAX) NULL,
    [ExceptionMessage]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CalculationFiles] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CalculationFiles_CalculationUploads] FOREIGN KEY ([CalculationUpload]) REFERENCES [dbo].[CalculationUploads] ([ID])
);



