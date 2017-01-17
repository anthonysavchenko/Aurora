CREATE TABLE [dbo].[ServiceOperationRegister] (
    [ID]                INT            IDENTITY (1, 1) NOT NULL,
    [OperationDateTime] DATETIME2 (0)  NOT NULL,
    [OperationID]       INT            NOT NULL,
    [OperationType]     INT            NOT NULL,
    [Service]           INT            NOT NULL,
    [ServicePeriod]     DATE           NOT NULL,
    [Customer]          INT            NOT NULL,
    [Value]             DECIMAL (9, 2) NOT NULL,
    CONSTRAINT [PK_ServicePaymentRegister] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ServiceOperationRegister_Customers] FOREIGN KEY ([Customer]) REFERENCES [dbo].[Customers] ([ID]),
    CONSTRAINT [FK_ServiceOperationRegister_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);
GO
CREATE NONCLUSTERED INDEX [IX_OperationID_OperationType]
    ON [dbo].[ServiceOperationRegister]([OperationID] ASC, [OperationType] ASC)
    INCLUDE([ID], [OperationDateTime], [Service], [ServicePeriod], [Customer], [Value]);

