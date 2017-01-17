CREATE TABLE [dbo].[CommonCounters] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Number]   NVARCHAR (50)  NOT NULL,
    [Building] INT            NOT NULL,
    [Service]  INT            NOT NULL,
    CONSTRAINT [PK_CommonCounters] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CommonCounters_Buildings] FOREIGN KEY ([Building]) REFERENCES [dbo].[Buildings] ([ID]),
    CONSTRAINT [FK_CommonCounters_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);



