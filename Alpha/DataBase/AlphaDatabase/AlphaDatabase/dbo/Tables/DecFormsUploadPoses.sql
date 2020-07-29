CREATE TABLE [dbo].[DecFormsUploadPoses] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [DecFormsUpload]   INT            NOT NULL,
    [FileName]         NVARCHAR (200) NOT NULL,
    [FormType]         TINYINT        NOT NULL,
    [RouteForm]        INT            NULL,
    [FillForm]         INT            NULL,
    [ErrorDescription] NVARCHAR (MAX) NULL,
    [ExceptionMessage] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_DecFormsUploadPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DecFormsUploadPoses_DecFormsUploads] FOREIGN KEY ([DecFormsUpload]) REFERENCES [dbo].[DecFormsUploads] ([ID]),
    CONSTRAINT [FK_DecFormsUploadPoses_FillForms] FOREIGN KEY ([FillForm]) REFERENCES [dbo].[FillForms] ([ID]),
    CONSTRAINT [FK_DecFormsUploadPoses_RouteForms] FOREIGN KEY ([RouteForm]) REFERENCES [dbo].[RouteForms] ([ID])
);





