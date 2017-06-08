CREATE TABLE [dbo].[Customers] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [Account]                 NVARCHAR (50)  NOT NULL,
    [OwnerType]               INT            NOT NULL,
    [IsPrivate]               BIT            NOT NULL,
    [RoomsCount]              INT            NOT NULL,
    [Apartment]               NVARCHAR (50)  NOT NULL,
    [Square]                  DECIMAL (9, 2) NOT NULL,
    [PhysicalPersonFullName]  NVARCHAR (50)  NOT NULL,
    [PhysicalPersonShortName] NVARCHAR (50)  NOT NULL,
    [JuridicalPersonFullName] NVARCHAR (50)  NOT NULL,
    [Building]                INT            NOT NULL,
    [Comment]                 NVARCHAR (MAX) NULL,
    [Floor]                   SMALLINT       NOT NULL,
    [LiftPresence]            BIT            NOT NULL,
    [RubbishChutePresence]    BIT            NOT NULL,
    [BillSendingSubscription] BIT            DEFAULT ((0)) NOT NULL,
    [UserID]                  INT            NULL,
    [DebtsRepayment]          BIT            CONSTRAINT [DF_Customers_DebtsRepayment] DEFAULT ((0)) NOT NULL,
    [Entrance]                TINYINT        DEFAULT ((1)) NOT NULL,
    [GisZhkhID]               NVARCHAR (14)  NULL,
    CONSTRAINT [PK_Customers_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Customers_Buildings] FOREIGN KEY ([Building]) REFERENCES [dbo].[Buildings] ([ID]),
    CONSTRAINT [FK_Customers_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);
















GO
CREATE NONCLUSTERED INDEX [IX_Building_Account]
    ON [dbo].[Customers]([Account] ASC, [Building] ASC)
    INCLUDE([ID], [OwnerType], [IsPrivate], [RoomsCount], [Apartment], [Square], [PhysicalPersonFullName], [PhysicalPersonShortName], [JuridicalPersonFullName], [Comment], [Floor], [LiftPresence], [RubbishChutePresence], [BillSendingSubscription], [UserID], [DebtsRepayment], [Entrance], [GisZhkhID]);

