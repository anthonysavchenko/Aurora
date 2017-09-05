USE [AuroraDb]
GO

DELETE FROM [dbo].[PrivateCounterValues] WHERE Period = '2016-11-01'
GO

update [dbo].[PrivateCounterValues] set Value = 0 WHERE Period = '2016-12-01'
GO

UPDATE [dbo].[CustomerPoses]
   SET [Since] = '2017-02-01'
 WHERE PrivateCounterID is null and Since = '2016-11-01'
GO

INSERT INTO ChargeSets (CreationDateTime, Period, Number, Quantity, ValueSum, Author) VALUES ('2016-12-01 00:00:01', '2016-12-01', (select MAX(Number) + 1 from dbo.ChargeSets), 9, 46065.54, 1)
GO

INSERT INTO ChargeOpers (CreationDateTime, Value, Customer, ChargeSet, ChargeCorrectionOper) VALUES ('2016-12-01 00:00:01', 14301.32, 1 , (select top 1 ID from dbo.ChargeSets order by id desc), null)
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (12212.76,	15,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (35.32,	20,	1,      (select top 1 ID from dbo.ChargeOpers order by id desc))
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (853.24, 15,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO

INSERT INTO ChargeOpers (CreationDateTime, Value, Customer, ChargeSet, ChargeCorrectionOper) VALUES ('2016-12-01 00:00:01', 117.52,   48 , (select top 1 ID from dbo.ChargeSets order by id desc), null)
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (39.22,	20,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (78.30,	15,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO

INSERT INTO ChargeOpers (CreationDateTime, Value, Customer, ChargeSet, ChargeCorrectionOper) VALUES ('2016-12-01 00:00:01', 25987.50, 92 , (select top 1 ID from dbo.ChargeSets order by id desc), null)
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (25987.50,	17,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO

INSERT INTO ChargeOpers (CreationDateTime, Value, Customer, ChargeSet, ChargeCorrectionOper) VALUES ('2016-12-01 00:00:01', 567.00,   95 , (select top 1 ID from dbo.ChargeSets order by id desc), null)
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (567.00,	17,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO

INSERT INTO ChargeOpers (CreationDateTime, Value, Customer, ChargeSet, ChargeCorrectionOper) VALUES ('2016-12-01 00:00:01', 2357.10,  98 , (select top 1 ID from dbo.ChargeSets order by id desc), null)
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (2357.10,	16,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO

INSERT INTO ChargeOpers (CreationDateTime, Value, Customer, ChargeSet, ChargeCorrectionOper) VALUES ('2016-12-01 00:00:01', 97.20,    101 , (select top 1 ID from dbo.ChargeSets order by id desc), null)
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (97.20,	16,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO

INSERT INTO ChargeOpers (CreationDateTime, Value, Customer, ChargeSet, ChargeCorrectionOper) VALUES ('2016-12-01 00:00:01', 5.40,     104 , (select top 1 ID from dbo.ChargeSets order by id desc), null)
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (5.40,	16,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO

INSERT INTO ChargeOpers (CreationDateTime, Value, Customer, ChargeSet, ChargeCorrectionOper) VALUES ('2016-12-01 00:00:01', 1579.50,  105 , (select top 1 ID from dbo.ChargeSets order by id desc), null)
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (1579.50,	16,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO

INSERT INTO ChargeOpers (CreationDateTime, Value, Customer, ChargeSet, ChargeCorrectionOper) VALUES ('2016-12-01 00:00:01', 1053.00,  106 , (select top 1 ID from dbo.ChargeSets order by id desc), null)
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (896.66,	16,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO
INSERT INTO dbo.ChargeOperPoses (Value, Service, Contractor, ChargeOper) VALUES (156.34,	16,	1,  (select top 1 ID from dbo.ChargeOpers order by id desc))
GO




INSERT INTO PaymentSets (CreationDateTime, Number, IsFile, Quantity, ValueSum, Comment, Intermediary, Author, PaymentDate) VALUES ('2016-12-04 16:54:57', 178, 0, 9, 46065.54, '', null, 1, '2016-12-31')
GO

INSERT INTO PaymentOpers (CreationDateTime, PaymentPeriod, Value, Customer, PaymentSet, PaymentCorrectionOper) VALUES ('2016-12-04 16:54:57', '2016-12-01', -25987.50, 92, (select top 1 id from dbo.PaymentSets order by id desc), null)
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -25987.50, 17, (select top 1 id from dbo.PaymentOpers order by id desc));
GO

INSERT INTO PaymentOpers (CreationDateTime, PaymentPeriod, Value, Customer, PaymentSet, PaymentCorrectionOper) VALUES ('2016-12-04 16:54:57', '2016-12-01', -2357.10, 98, (select top 1 id from dbo.PaymentSets order by id desc), null)
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -2357.10, 16, (select top 1 id from dbo.PaymentOpers order by id desc))
GO

INSERT INTO PaymentOpers (CreationDateTime, PaymentPeriod, Value, Customer, PaymentSet, PaymentCorrectionOper) VALUES ('2016-12-04 16:54:57', '2016-12-01', -567.00, 95, (select top 1 id from dbo.PaymentSets order by id desc), null)
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -567.00, 17, (select top 1 id from dbo.PaymentOpers order by id desc))
GO

INSERT INTO PaymentOpers (CreationDateTime, PaymentPeriod, Value, Customer, PaymentSet, PaymentCorrectionOper) VALUES ('2016-12-04 16:54:57', '2016-12-01', -97.20, 101, (select top 1 id from dbo.PaymentSets order by id desc), null)
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -97.20, 16, (select top 1 id from dbo.PaymentOpers order by id desc))
GO

INSERT INTO PaymentOpers (CreationDateTime, PaymentPeriod, Value, Customer, PaymentSet, PaymentCorrectionOper) VALUES ('2016-12-04 16:54:57', '2016-12-01', -1579.50, 105, (select top 1 id from dbo.PaymentSets order by id desc), null)
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -1579.50, 16, (select top 1 id from dbo.PaymentOpers order by id desc))
GO

INSERT INTO PaymentOpers (CreationDateTime, PaymentPeriod, Value, Customer, PaymentSet, PaymentCorrectionOper) VALUES ('2016-12-04 16:54:57', '2016-12-01', -5.40, 104, (select top 1 id from dbo.PaymentSets order by id desc), null)
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -5.40, 16, (select top 1 id from dbo.PaymentOpers order by id desc))
GO

INSERT INTO PaymentOpers (CreationDateTime, PaymentPeriod, Value, Customer, PaymentSet, PaymentCorrectionOper) VALUES ('2016-12-04 16:54:57', '2016-12-01', -1053.00, 106, (select top 1 id from dbo.PaymentSets order by id desc), null)
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -1053.00, 16, (select top 1 id from dbo.PaymentOpers order by id desc))
GO

INSERT INTO PaymentOpers (CreationDateTime, PaymentPeriod, Value, Customer, PaymentSet, PaymentCorrectionOper) VALUES ('2016-12-04 16:54:57', '2016-12-01', -117.52, 48, (select top 1 id from dbo.PaymentSets order by id desc), null)
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -78.30, 15, (select top 1 id from dbo.PaymentOpers order by id desc))
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -39.22, 20, (select top 1 id from dbo.PaymentOpers order by id desc))
GO


INSERT INTO PaymentOpers (CreationDateTime, PaymentPeriod, Value, Customer, PaymentSet, PaymentCorrectionOper) VALUES ('2016-12-04 16:54:57', '2016-12-01', -14301.32, 1, (select top 1 id from dbo.PaymentSets order by id desc), null)
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -14066.00, 15, (select top 1 id from dbo.PaymentOpers order by id desc))
GO
INSERT INTO PaymentOperPoses (Period, Value, Service, PaymentOper) VALUES ('2016-12-01', -235.32, 20, (select top 1 id from dbo.PaymentOpers order by id desc))
GO