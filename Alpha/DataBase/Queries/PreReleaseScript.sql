USE [AlphaRepairDataBase]
GO


UPDATE [dbo].[RegularBillDocSeviceTypePoses] SET [ServiceTypeID] = (select ID from dbo.ServiceTypes where Name = ServiceTypeName)
GO