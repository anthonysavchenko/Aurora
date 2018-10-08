CREATE TABLE [dbo].[RebenefitOperPoses] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [BenefitRule]   TINYINT        NOT NULL,
    [Value]         DECIMAL (9, 2) NOT NULL,
    [Service]       INT            NOT NULL,
    [RebenefitOper] INT            NOT NULL,
    [Contractor]    INT            NOT NULL,
    CONSTRAINT [PK_RebenefitOperPoses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RebenefitOperPoses_Contractors] FOREIGN KEY ([Contractor]) REFERENCES [dbo].[Contractors] ([ID]),
    CONSTRAINT [FK_RebenefitOperPoses_RebenefitOpers] FOREIGN KEY ([RebenefitOper]) REFERENCES [dbo].[RebenefitOpers] ([ID]),
    CONSTRAINT [FK_RebenefitOperPoses_Services] FOREIGN KEY ([Service]) REFERENCES [dbo].[Services] ([ID])
);






GO
CREATE NONCLUSTERED INDEX [IX_RebenefitOper]
    ON [dbo].[RebenefitOperPoses]([RebenefitOper] ASC)
    INCLUDE([Value], [Service]);

