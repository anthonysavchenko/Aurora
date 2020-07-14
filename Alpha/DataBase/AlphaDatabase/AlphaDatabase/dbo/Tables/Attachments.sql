CREATE TABLE [dbo].[Attachments] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Email]            INT            NOT NULL,
    [FileName]         NVARCHAR (200) NULL,
    [ErrorDescription] NVARCHAR (MAX) NULL,
    [ExceptionMessage] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Attachments_Emails] FOREIGN KEY ([Email]) REFERENCES [dbo].[Emails] ([ID])
);

