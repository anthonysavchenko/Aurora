CREATE TABLE [dbo].[RouteForms] (
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [Street]            NVARCHAR (50) NOT NULL,
    [Building]          NVARCHAR (10) NOT NULL,
    [DecFormsUploadPos] INT           NOT NULL,
    CONSTRAINT [PK_RouteForms] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RouteForms_DecFormsUploadPoses] FOREIGN KEY ([DecFormsUploadPos]) REFERENCES [dbo].[DecFormsUploadPoses] ([ID])
);

