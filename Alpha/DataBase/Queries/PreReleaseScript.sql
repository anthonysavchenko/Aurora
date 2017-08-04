ALTER TABLE [dbo].[PrivateCounterValues] DROP CONSTRAINT [FK_PrivateCounterValues_PrivateCounters];


GO
PRINT N'Dropping [dbo].[FK_PrivateCounters_CustomerPoses]...';


GO
ALTER TABLE [dbo].[PrivateCounters] DROP CONSTRAINT [FK_PrivateCounters_CustomerPoses];


GO
PRINT N'Altering [dbo].[CommonCounterValues]...';


GO
ALTER TABLE [dbo].[CommonCounterValues] ALTER COLUMN [Value] DECIMAL (10, 3) NOT NULL;


GO
PRINT N'Altering [dbo].[CustomerPoses]...';


GO
ALTER TABLE [dbo].[CustomerPoses]
    ADD [PrivateCounterID] INT NULL;


GO
PRINT N'Altering [dbo].[PrivateCounters]...';


GO
ALTER TABLE [dbo].[PrivateCounters] DROP COLUMN [CustomerPos], COLUMN [Rate];


GO
ALTER TABLE [dbo].[PrivateCounters]
    ADD [CustomerID] INT NULL,
        [ServiceID]  INT NULL;


GO
PRINT N'Altering [dbo].[PrivateCounterValues]...';


GO
ALTER TABLE [dbo].[PrivateCounterValues] ALTER COLUMN [Value] DECIMAL (10, 3) NOT NULL;


GO

PRINT N'Creating [dbo].[FK_PrivateCounterValues_PrivateCounters]...';


GO
ALTER TABLE [dbo].[PrivateCounterValues] WITH NOCHECK
    ADD CONSTRAINT [FK_PrivateCounterValues_PrivateCounters] FOREIGN KEY ([PrivateCounter]) REFERENCES [dbo].[PrivateCounters] ([ID]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_CustomerPoses_PrivateCounters]...';


GO
ALTER TABLE [dbo].[CustomerPoses] WITH NOCHECK
    ADD CONSTRAINT [FK_CustomerPoses_PrivateCounters] FOREIGN KEY ([PrivateCounterID]) REFERENCES [dbo].[PrivateCounters] ([ID]);


GO
PRINT N'Creating [dbo].[FK_PrivateCounters_Customers]...';


GO
ALTER TABLE [dbo].[PrivateCounters] WITH NOCHECK
    ADD CONSTRAINT [FK_PrivateCounters_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]);


GO
PRINT N'Creating [dbo].[FK_PrivateCounters_Services]...';


GO
ALTER TABLE [dbo].[PrivateCounters] WITH NOCHECK
    ADD CONSTRAINT [FK_PrivateCounters_Services] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Services] ([ID]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [dbo].[PrivateCounterValues] WITH CHECK CHECK CONSTRAINT [FK_PrivateCounterValues_PrivateCounters];

ALTER TABLE [dbo].[CustomerPoses] WITH CHECK CHECK CONSTRAINT [FK_CustomerPoses_PrivateCounters];

ALTER TABLE [dbo].[PrivateCounters] WITH CHECK CHECK CONSTRAINT [FK_PrivateCounters_Customers];

ALTER TABLE [dbo].[PrivateCounters] WITH CHECK CHECK CONSTRAINT [FK_PrivateCounters_Services];


GO

PRINT N'Altering [dbo].[RegularBillDocCounterPoses]...';


GO
ALTER TABLE [dbo].[RegularBillDocCounterPoses] ALTER COLUMN [Consumption] DECIMAL (10, 3) NOT NULL;

ALTER TABLE [dbo].[RegularBillDocCounterPoses] ALTER COLUMN [CurValue] DECIMAL (10, 3) NOT NULL;

ALTER TABLE [dbo].[RegularBillDocCounterPoses] ALTER COLUMN [PrevValue] DECIMAL (10, 3) NOT NULL;


GO
PRINT N'Creating [dbo].[Customers].[IX_Building]...';


GO
CREATE NONCLUSTERED INDEX [IX_Building]
    ON [dbo].[Customers]([Building] ASC);


GO
PRINT N'Creating [dbo].[Residents].[IX_Customer]...';


GO
CREATE NONCLUSTERED INDEX [IX_Customer]
    ON [dbo].[Residents]([Customer] ASC);


GO



update dbo.PrivateCounters set ServiceID = 15, CustomerID = 6 where ID = 1;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 6 where ID = 2;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 1 where ID = 3;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 1 where ID = 6;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 2 where ID = 5;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 1 where ID = 7;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 2 where ID = 8;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 3 where ID = 10;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 3 where ID = 9;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 4 where ID = 11;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 4 where ID = 12;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 5 where ID = 13;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 5 where ID = 155;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 5 where ID = 156;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 7 where ID = 16;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 7 where ID = 17;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 8 where ID = 18;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 8 where ID = 19;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 9 where ID = 20;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 9 where ID = 21;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 10 where ID = 22;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 10 where ID = 23;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 11 where ID = 24;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 11 where ID = 25;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 11 where ID = 26;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 12 where ID = 27;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 12 where ID = 28;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 12 where ID = 29;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 13 where ID = 30;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 13 where ID = 31;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 52 where ID = 32;
update dbo.PrivateCounters set ServiceID = 18, CustomerID = 52 where ID = 33;
update dbo.PrivateCounters set ServiceID = 18, CustomerID = 52 where ID = 34;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 53 where ID = 35;
update dbo.PrivateCounters set ServiceID = 18, CustomerID = 53 where ID = 36;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 54 where ID = 38;
update dbo.PrivateCounters set ServiceID = 18, CustomerID = 54 where ID = 37;
update dbo.PrivateCounters set ServiceID = 18, CustomerID = 55 where ID = 39;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 55 where ID = 40;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 56 where ID = 41;
update dbo.PrivateCounters set ServiceID = 18, CustomerID = 56 where ID = 42;
update dbo.PrivateCounters set ServiceID = 18, CustomerID = 56 where ID = 43;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 14 where ID = 44;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 15 where ID = 46;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 16 where ID = 66;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 17 where ID = 76;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 18 where ID = 78;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 19 where ID = 80;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 20 where ID = 82;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 21 where ID = 85;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 22 where ID = 88;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 23 where ID = 91;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 24 where ID = 93;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 25 where ID = 95;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 26 where ID = 97;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 27 where ID = 99;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 28 where ID = 101;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 29 where ID = 103;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 30 where ID = 105;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 31 where ID = 108;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 32 where ID = 110;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 33 where ID = 112;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 34 where ID = 114;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 35 where ID = 116;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 36 where ID = 118;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 37 where ID = 120;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 38 where ID = 123;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 39 where ID = 125;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 40 where ID = 127;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 41 where ID = 130;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 42 where ID = 68;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 43 where ID = 132;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 44 where ID = 134;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 45 where ID = 136;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 46 where ID = 138;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 47 where ID = 141;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 14 where ID = 45;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 15 where ID = 65;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 16 where ID = 67;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 17 where ID = 77;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 18 where ID = 79;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 18 where ID = 152;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 19 where ID = 81;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 20 where ID = 83;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 20 where ID = 84;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 21 where ID = 86;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 21 where ID = 87;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 22 where ID = 89;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 22 where ID = 90;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 23 where ID = 92;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 24 where ID = 94;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 25 where ID = 96;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 26 where ID = 98;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 27 where ID = 100;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 28 where ID = 102;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 29 where ID = 104;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 30 where ID = 106;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 30 where ID = 107;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 31 where ID = 109;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 32 where ID = 111;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 33 where ID = 113;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 34 where ID = 115;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 35 where ID = 117;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 36 where ID = 119;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 37 where ID = 121;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 37 where ID = 122;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 38 where ID = 124;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 39 where ID = 126;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 39 where ID = 153;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 40 where ID = 128;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 40 where ID = 129;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 41 where ID = 131;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 42 where ID = 69;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 42 where ID = 70;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 43 where ID = 133;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 44 where ID = 135;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 45 where ID = 137;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 45 where ID = 154;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 46 where ID = 139;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 46 where ID = 140;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 47 where ID = 142;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 47 where ID = 143;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 48 where ID = 144;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 49 where ID = 146;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 50 where ID = 71;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 51 where ID = 74;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 48 where ID = 145;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 49 where ID = 147;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 49 where ID = 148;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 50 where ID = 72;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 50 where ID = 73;
update dbo.PrivateCounters set ServiceID = 15, CustomerID = 51 where ID = 75;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 94 where ID = 51;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 96 where ID = 47;
update dbo.PrivateCounters set ServiceID = 20, CustomerID = 96 where ID = 48;
update dbo.PrivateCounters set ServiceID = 17, CustomerID = 90 where ID = 149;
update dbo.PrivateCounters set ServiceID = 17, CustomerID = 91 where ID = 55;
update dbo.PrivateCounters set ServiceID = 17, CustomerID = 92 where ID = 56;
update dbo.PrivateCounters set ServiceID = 17, CustomerID = 93 where ID = 151;
update dbo.PrivateCounters set ServiceID = 17, CustomerID = 94 where ID = 52;
update dbo.PrivateCounters set ServiceID = 17, CustomerID = 95 where ID = 150;
update dbo.PrivateCounters set ServiceID = 17, CustomerID = 96 where ID = 49;
update dbo.PrivateCounters set ServiceID = 17, CustomerID = 96 where ID = 50;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 98 where ID = 53;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 99 where ID = 54;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 100 where ID = 57;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 101 where ID = 58;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 102 where ID = 59;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 103 where ID = 60;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 104 where ID = 61;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 105 where ID = 62;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 106 where ID = 63;
update dbo.PrivateCounters set ServiceID = 16, CustomerID = 106 where ID = 64;

INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 1, '2016-11-01', '2017-06-01', 6);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 1, '2017-07-01', '2030-01-01', 6);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 11, '2016-11-01', '2017-06-01', 26);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 11, '2017-07-01', '2030-01-01', 26);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 12, '2016-11-01', '2017-06-01', 29);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 12, '2017-07-01', '2030-01-01', 29);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (18, 1, 1.07, 52, '2016-11-01', '2017-06-01', 34);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (18, 1, 1.14, 52, '2017-07-01', '2030-01-01', 34);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (18, 1, 1.07, 56, '2016-11-01', '2017-06-01', 43);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (18, 1, 1.14, 56, '2017-07-01', '2030-01-01', 43);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (17, 1, 1.07, 96, '2016-11-01', '2017-06-01', 50);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (17, 1, 1.14, 96, '2017-07-01', '2030-01-01', 50);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 42, '2016-11-01', '2017-06-01', 70);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 42, '2017-07-01', '2030-01-01', 70);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 50, '2016-11-01', '2017-06-01', 73);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 50, '2017-07-01', '2030-01-01', 73);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 20, '2016-11-01', '2017-06-01', 84);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 20, '2017-07-01', '2030-01-01', 84);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 21, '2016-11-01', '2017-06-01', 87);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 21, '2017-07-01', '2030-01-01', 87);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 22, '2016-11-01', '2017-06-01', 90);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 22, '2017-07-01', '2030-01-01', 90);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 30, '2016-11-01', '2017-06-01', 107);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 30, '2017-07-01', '2030-01-01', 107);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 37, '2016-11-01', '2017-06-01', 122);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 37, '2017-07-01', '2030-01-01', 122);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 40, '2016-11-01', '2017-06-01', 129);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 40, '2017-07-01', '2030-01-01', 129);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 46, '2016-11-01', '2017-06-01', 140);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 46, '2017-07-01', '2030-01-01', 140);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 49, '2016-11-01', '2017-06-01', 148);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 49, '2017-07-01', '2030-01-01', 148);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 18, '2017-01-01', '2017-06-01', 152);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 18, '2017-07-01', '2030-01-01', 152);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 39, '2016-11-01', '2017-06-01', 153);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 39, '2017-07-01', '2030-01-01', 153);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 45, '2016-11-01', '2017-06-01', 154);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 45, '2017-07-01', '2030-01-01', 154);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.07, 5, '2016-11-01', '2017-06-01', 156);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 1.14, 5, '2017-07-01', '2030-01-01', 156);

UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 3 WHERE ID = 21;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 1, '2017-07-01', '2030-01-01', 3);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 25 WHERE ID = 141;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 11, '2017-07-01', '2030-01-01', 25);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 28 WHERE ID = 155;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 12, '2017-07-01', '2030-01-01', 28);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 33 WHERE ID = 174;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (18, 1, 3.15, 52, '2017-07-01', '2030-01-01', 33);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 42 WHERE ID = 202;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (18, 1, 3.15, 56, '2017-07-01', '2030-01-01', 42);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 49 WHERE ID = 779;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (17, 1, 3.15, 96, '2017-07-01', '2030-01-01', 49);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 69 WHERE ID = 575;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 42, '2017-07-01', '2030-01-01', 69);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 72 WHERE ID = 619;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 50, '2017-07-01', '2030-01-01', 72);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 79 WHERE ID = 551;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 18, '2017-07-01', '2030-01-01', 79);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 83 WHERE ID = 553;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 20, '2017-07-01', '2030-01-01', 83);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 86 WHERE ID = 554;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 21, '2017-07-01', '2030-01-01', 86);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 89 WHERE ID = 555;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 22, '2017-07-01', '2030-01-01', 89);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 106 WHERE ID = 563;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 30, '2017-07-01', '2030-01-01', 106);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 121 WHERE ID = 570;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 37, '2017-07-01', '2030-01-01', 121);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 126 WHERE ID = 572;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 39, '2017-07-01', '2030-01-01', 126);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 128 WHERE ID = 573;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 40, '2017-07-01', '2030-01-01', 128);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 137 WHERE ID = 578;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 45, '2017-07-01', '2030-01-01', 137);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 139 WHERE ID = 579;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 46, '2017-07-01', '2030-01-01', 139);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 147 WHERE ID = 618;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 49, '2017-07-01', '2030-01-01', 147);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.94,[Till] = '2017-06-01',[PrivateCounterID] = 155 WHERE ID = 70;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 3.15, 5, '2017-07-01', '2030-01-01', 155);

UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 1 WHERE ID = 3;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 6, '2017-07-01', '2030-01-01', 1);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 5 WHERE ID = 32;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 2, '2017-07-01', '2030-01-01', 5);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 9 WHERE ID = 45;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 3, '2017-07-01', '2030-01-01', 9);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 12 WHERE ID = 57;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 4, '2017-07-01', '2030-01-01', 12);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 17 WHERE ID = 85;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 7, '2017-07-01', '2030-01-01', 17);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 19 WHERE ID = 99;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 8, '2017-07-01', '2030-01-01', 19);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 21 WHERE ID = 113;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 9, '2017-07-01', '2030-01-01', 21);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 23 WHERE ID = 127;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 10, '2017-07-01', '2030-01-01', 23);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 31 WHERE ID = 167;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 13, '2017-07-01', '2030-01-01', 31);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 36 WHERE ID = 181;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (18, 1, 2.83, 53, '2017-07-01', '2030-01-01', 36);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 37 WHERE ID = 188;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (18, 1, 2.83, 54, '2017-07-01', '2030-01-01', 37);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 39 WHERE ID = 194;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (18, 1, 2.83, 55, '2017-07-01', '2030-01-01', 39);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 45 WHERE ID = 547;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 14, '2017-07-01', '2030-01-01', 45);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 52 WHERE ID = 777;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (17, 1, 2.83, 94, '2017-07-01', '2030-01-01', 52);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 53 WHERE ID = 801;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 98, '2017-07-01', '2030-01-01', 53);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 54 WHERE ID = 802;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 99, '2017-07-01', '2030-01-01', 54);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 55 WHERE ID = 774;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (17, 1, 2.83, 91, '2017-07-01', '2030-01-01', 55);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 56 WHERE ID = 775;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (17, 1, 2.83, 92, '2017-07-01', '2030-01-01', 56);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 57 WHERE ID = 803;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 100, '2017-07-01', '2030-01-01', 57);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 58 WHERE ID = 804;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 101, '2017-07-01', '2030-01-01', 58);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 59 WHERE ID = 805;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 102, '2017-07-01', '2030-01-01', 59);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 60 WHERE ID = 806;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 103, '2017-07-01', '2030-01-01', 60);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 61 WHERE ID = 807;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 104, '2017-07-01', '2030-01-01', 61);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 62 WHERE ID = 808;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 105, '2017-07-01', '2030-01-01', 62);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 63 WHERE ID = 809;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 106, '2017-07-01', '2030-01-01', 63);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.70, 106, '2016-11-01', '2017-06-01', 64);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (16, 1, 2.83, 106, '2017-07-01', '2030-01-01', 64);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 65 WHERE ID = 548;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 15, '2017-07-01', '2030-01-01', 65);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 67 WHERE ID = 549;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 16, '2017-07-01', '2030-01-01', 67);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 75 WHERE ID = 620;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 51, '2017-07-01', '2030-01-01', 75);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 77 WHERE ID = 550;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 17, '2017-07-01', '2030-01-01', 77);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 81 WHERE ID = 552;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 19, '2017-07-01', '2030-01-01', 81);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 92 WHERE ID = 556;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 23, '2017-07-01', '2030-01-01', 92);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 94 WHERE ID = 557;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 24, '2017-07-01', '2030-01-01', 94);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 96 WHERE ID = 558;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 25, '2017-07-01', '2030-01-01', 96);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 98 WHERE ID = 559;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 26, '2017-07-01', '2030-01-01', 98);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 100 WHERE ID = 560;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 27, '2017-07-01', '2030-01-01', 100);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 102 WHERE ID = 561;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 28, '2017-07-01', '2030-01-01', 102);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 104 WHERE ID = 562;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 29, '2017-07-01', '2030-01-01', 104);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 109 WHERE ID = 564;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 31, '2017-07-01', '2030-01-01', 109);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 111 WHERE ID = 565;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 32, '2017-07-01', '2030-01-01', 111);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 113 WHERE ID = 566;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 33, '2017-07-01', '2030-01-01', 113);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 115 WHERE ID = 567;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 34, '2017-07-01', '2030-01-01', 115);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 117 WHERE ID = 568;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 35, '2017-07-01', '2030-01-01', 117);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 119 WHERE ID = 569;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 36, '2017-07-01', '2030-01-01', 119);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 124 WHERE ID = 571;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 38, '2017-07-01', '2030-01-01', 124);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 131 WHERE ID = 574;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 41, '2017-07-01', '2030-01-01', 131);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 133 WHERE ID = 576;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 43, '2017-07-01', '2030-01-01', 133);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 135 WHERE ID = 577;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 44, '2017-07-01', '2030-01-01', 135);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 142 WHERE ID = 580;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 47, '2017-07-01', '2030-01-01', 142);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.70, 47, '2016-11-01', '2017-06-01', 143);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 47, '2017-07-01', '2030-01-01', 143);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 145 WHERE ID = 617;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (15, 1, 2.83, 48, '2017-07-01', '2030-01-01', 145);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 149 WHERE ID = 773;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (17, 1, 2.83, 90, '2017-07-01', '2030-01-01', 149);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 150 WHERE ID = 778;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (17, 1, 2.83, 95, '2017-07-01', '2030-01-01', 150);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 2.70,[Till] = '2017-06-01',[PrivateCounterID] = 151 WHERE ID = 776;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (17, 1, 2.83, 93, '2017-07-01', '2030-01-01', 151);


UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 2 WHERE ID = 11;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 6, '2017-07-01', '2030-01-01', 2);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 7 WHERE ID = 33;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 1, '2017-07-01', '2030-01-01', 7);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 8 WHERE ID = 35;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 2, '2017-07-01', '2030-01-01', 8);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 10 WHERE ID = 43;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 3, '2017-07-01', '2030-01-01', 10);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 11 WHERE ID = 54;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 4, '2017-07-01', '2030-01-01', 11);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 13 WHERE ID = 66;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 5, '2017-07-01', '2030-01-01', 13);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 16 WHERE ID = 82;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 7, '2017-07-01', '2030-01-01', 16);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 18 WHERE ID = 96;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 8, '2017-07-01', '2030-01-01', 18);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 20 WHERE ID = 110;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 9, '2017-07-01', '2030-01-01', 20);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 22 WHERE ID = 124;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 10, '2017-07-01', '2030-01-01', 22);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 24 WHERE ID = 138;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 11, '2017-07-01', '2030-01-01', 24);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 27 WHERE ID = 151;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 12, '2017-07-01', '2030-01-01', 27);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 30 WHERE ID = 164;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 13, '2017-07-01', '2030-01-01', 30);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 32 WHERE ID = 172;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 52, '2017-07-01', '2030-01-01', 32);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 35 WHERE ID = 178;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 53, '2017-07-01', '2030-01-01', 35);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 40 WHERE ID = 195;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 55, '2017-07-01', '2030-01-01', 40);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 41 WHERE ID = 201;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 56, '2017-07-01', '2030-01-01', 41);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 44 WHERE ID = 513;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 14, '2017-07-01', '2030-01-01', 44);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 46 WHERE ID = 514;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 15, '2017-07-01', '2030-01-01', 46);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 39.22, 96, '2016-11-01', '2017-06-01', 47);
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 96, '2017-07-01', '2030-01-01', 47);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 48 WHERE ID = 763;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 96, '2017-07-01', '2030-01-01', 48);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 51 WHERE ID = 761;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 94, '2017-07-01', '2030-01-01', 51);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 66 WHERE ID = 515;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 16, '2017-07-01', '2030-01-01', 66);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 68 WHERE ID = 541;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 42, '2017-07-01', '2030-01-01', 68);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 71 WHERE ID = 615;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 50, '2017-07-01', '2030-01-01', 71);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 74 WHERE ID = 616;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 51, '2017-07-01', '2030-01-01', 74);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 76 WHERE ID = 516;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 17, '2017-07-01', '2030-01-01', 76);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 78 WHERE ID = 517;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 18, '2017-07-01', '2030-01-01', 78);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 80 WHERE ID = 518;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 19, '2017-07-01', '2030-01-01', 80);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 82 WHERE ID = 519;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 20, '2017-07-01', '2030-01-01', 82);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 85 WHERE ID = 520;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 21, '2017-07-01', '2030-01-01', 85);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 88 WHERE ID = 521;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 22, '2017-07-01', '2030-01-01', 88);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 91 WHERE ID = 522;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 23, '2017-07-01', '2030-01-01', 91);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 93 WHERE ID = 523;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 24, '2017-07-01', '2030-01-01', 93);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 95 WHERE ID = 524;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 25, '2017-07-01', '2030-01-01', 95);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 97 WHERE ID = 525;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 26, '2017-07-01', '2030-01-01', 97);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 99 WHERE ID = 526;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 27, '2017-07-01', '2030-01-01', 99);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 101 WHERE ID = 527;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 28, '2017-07-01', '2030-01-01', 101);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 103 WHERE ID = 528;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 29, '2017-07-01', '2030-01-01', 103);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 105 WHERE ID = 529;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 30, '2017-07-01', '2030-01-01', 105);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 108 WHERE ID = 530;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 31, '2017-07-01', '2030-01-01', 108);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 110 WHERE ID = 531;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 32, '2017-07-01', '2030-01-01', 110);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 112 WHERE ID = 532;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 33, '2017-07-01', '2030-01-01', 112);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 114 WHERE ID = 533;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 34, '2017-07-01', '2030-01-01', 114);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 116 WHERE ID = 534;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 35, '2017-07-01', '2030-01-01', 116);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 118 WHERE ID = 535;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 36, '2017-07-01', '2030-01-01', 118);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 120 WHERE ID = 536;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 37, '2017-07-01', '2030-01-01', 120);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 123 WHERE ID = 537;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 38, '2017-07-01', '2030-01-01', 123);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 125 WHERE ID = 538;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 39, '2017-07-01', '2030-01-01', 125);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 127 WHERE ID = 539;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 40, '2017-07-01', '2030-01-01', 127);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 130 WHERE ID = 540;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 41, '2017-07-01', '2030-01-01', 130);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 132 WHERE ID = 542;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 43, '2017-07-01', '2030-01-01', 132);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 134 WHERE ID = 543;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 44, '2017-07-01', '2030-01-01', 134);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 136 WHERE ID = 544;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 45, '2017-07-01', '2030-01-01', 136);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 138 WHERE ID = 545;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 46, '2017-07-01', '2030-01-01', 138);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 141 WHERE ID = 546;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 47, '2017-07-01', '2030-01-01', 141);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 144 WHERE ID = 613;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 48, '2017-07-01', '2030-01-01', 144);
UPDATE [dbo].[CustomerPoses] SET [Rate] = 39.22,[Till] = '2017-06-01',[PrivateCounterID] = 146 WHERE ID = 614;
INSERT INTO [dbo].[CustomerPoses] ([Service],[Contractor],[Rate],[Customer],[Since],[Till],[PrivateCounterID]) VALUES (20, 1, 41.82, 49, '2017-07-01', '2030-01-01', 146);

PRINT N'Update complete.';

GO