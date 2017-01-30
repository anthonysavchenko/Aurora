USE [AuroraDb]
GO

INSERT INTO [dbo].[Users]
           ([Login]
           ,[Password]
           ,[Aka]
           ,[SecurityStamp]
           ,[LockoutEndDateUtc]
           ,[LockoutEnabled]
           ,[AccessFailedCount])
     VALUES
           ('admin',
		   '21232f297a57a5a743894a0e4a801fc3',
		   'Администратор',
		   	NULL,
			NULL,
			0,
			0)
GO

SET IDENTITY_INSERT [dbo].[Settings] ON 

GO
INSERT [dbo].[Settings] ([ID], [Name], [Value]) VALUES (1, N'FineCoefficient', N'0,1')
GO
INSERT [dbo].[Settings] ([ID], [Name], [Value]) VALUES (2, N'SmtpServer', N'smtp.sendgrid.net')
GO
INSERT [dbo].[Settings] ([ID], [Name], [Value]) VALUES (3, N'SmtpPort', N'25')
GO
INSERT [dbo].[Settings] ([ID], [Name], [Value]) VALUES (4, N'SmtpLogin', N'azure_3dbbaa3e185845ba8f9a96bd58176029@azure.com')
GO
INSERT [dbo].[Settings] ([ID], [Name], [Value]) VALUES (5, N'SmtpPassword', N'FsjroS1w79g29SX')
GO
INSERT [dbo].[Settings] ([ID], [Name], [Value]) VALUES (6, N'SmtpSenderName', N'Qvitex.ru')
GO
INSERT [dbo].[Settings] ([ID], [Name], [Value]) VALUES (7, N'SmtpSenderEmail', N'qvitex@taumis.ru')
GO
INSERT [dbo].[Settings] ([ID], [Name], [Value]) VALUES (8, N'BackupPath', N'D:\Backup')
GO
SET IDENTITY_INSERT [dbo].[Settings] OFF
GO