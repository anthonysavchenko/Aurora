CREATE TABLE [dbo].[CalculationForms] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [CalculationFile] INT NOT NULL,
    CONSTRAINT [PK_CalculationForms] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CalculationForms_CalculationFiles] FOREIGN KEY ([CalculationFile]) REFERENCES [dbo].[CalculationFiles] ([ID])
);

