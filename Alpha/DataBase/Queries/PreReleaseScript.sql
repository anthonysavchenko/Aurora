USE [AlphaDataBase]
GO

INSERT INTO [dbo].[ServiceTypes] ([Name],[Code]) VALUES('Замена системы ГВС', 'Замена системы ГВС')
GO

INSERT INTO [dbo].[ServiceTypes] ([Name],[Code]) VALUES('Содержание и обслуживание спец.счета кап.ремонта', 'Содержание и обслуживание спец.счета кап.ремонта')
GO

UPDATE [dbo].[RegularBillDocSeviceTypePoses] SET [ServiceTypeID] = (select ID from dbo.ServiceTypes where Name = ServiceTypeName)
GO