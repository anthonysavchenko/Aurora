CREATE TABLE [dbo].[PrivateValuesForms] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [PrivateValuesUpload] INT            NOT NULL,
    [FileName]            NVARCHAR (200) NOT NULL,
    [Street]              NVARCHAR (50)  NULL,
    [Building]            NVARCHAR (25)  NULL,
    [ErrorDescription]    NVARCHAR (MAX) NULL,
    [ExceptionMessage]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_PrivateValuesForms] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrivateValuesForms_PrivateValuesUploads] FOREIGN KEY ([PrivateValuesUpload]) REFERENCES [dbo].[PrivateValuesUploads] ([ID])
);



