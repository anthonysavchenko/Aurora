CREATE TABLE [dbo].[FillForms] (
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [Street]            NVARCHAR (50) NOT NULL,
    [Building]          NVARCHAR (10) NOT NULL,
    [DecFormsUploadPos] INT           NOT NULL,
    CONSTRAINT [PK_FillForms] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_FillForms_DecFormsUploadPoses] FOREIGN KEY ([DecFormsUploadPos]) REFERENCES [dbo].[DecFormsUploadPoses] ([ID])
);

