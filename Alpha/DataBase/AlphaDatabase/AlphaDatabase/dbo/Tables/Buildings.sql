CREATE TABLE [dbo].[Buildings] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [Street]                  INT            NOT NULL,
    [Number]                  NVARCHAR (50)  NOT NULL,
    [ZipCode]                 NVARCHAR (50)  NOT NULL,
    [FloorCount]              SMALLINT       NOT NULL,
    [EntranceCount]           TINYINT        NOT NULL,
    [Note]                    NVARCHAR (MAX) NOT NULL,
    [FiasID]                  NVARCHAR (36)  NULL,
    [NonResidentialPlaceArea] DECIMAL (9, 2) CONSTRAINT [DF_Buildings_NonResidentialPlaceArea] DEFAULT ((0)) NOT NULL,
    [BankDetailID]            INT            NULL,
    CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Buildings_BankDetails] FOREIGN KEY ([BankDetailID]) REFERENCES [dbo].[BankDetails] ([ID]),
    CONSTRAINT [FK_Buildings_Streets] FOREIGN KEY ([Street]) REFERENCES [dbo].[Streets] ([ID])
);