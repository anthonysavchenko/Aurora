USE [AlphaDataBase]
GO

UPDATE [dbo].[DebtBillDocs] 
   SET [CustomerID] = (SELECT ID FROM [dbo].Customers as c WHERE c.Account = [dbo].[DebtBillDocs].Account)
GO