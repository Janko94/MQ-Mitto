CREATE TABLE CountryCode
(
CC_AUID INT PRIMARY KEY IDENTITY(1,1),
CC_Name NVARCHAR(MAX),
CC_MobileCountryCode VARCHAR(4),
CC_CountryCode VARCHAR(4),
CC_PricePerSMS MONEY
)

GO

IF NOT EXISTS(SELECT 1 FROM CountryCode WHERE CC_Name = 'Germany')
BEGIN
	INSERT INTO CountryCode 
	(CC_Name, CC_MobileCountryCode, CC_CountryCode, CC_PricePerSMS)
	VALUES
	('Germany', '262', '49', 0.055)
END
IF NOT EXISTS(SELECT 1 FROM CountryCode WHERE CC_Name = 'Austria')
BEGIN
	INSERT INTO CountryCode 
	(CC_Name, CC_MobileCountryCode, CC_CountryCode, CC_PricePerSMS)
	VALUES
	('Austria', '232', '43', 0.053)
END
IF NOT EXISTS(SELECT 1 FROM CountryCode WHERE CC_Name = 'Poland')
BEGIN
	INSERT INTO CountryCode 
	(CC_Name, CC_MobileCountryCode, CC_CountryCode, CC_PricePerSMS)
	VALUES
	('Poland', '260', '48', 0.032)
END

GO

CREATE TABLE SMS
(
SMS_AUID INT PRIMARY KEY IDENTITY(1,1),
SMS_From NVARCHAR(100),
SMS_To NVARCHAR(100),
SMS_Text NVARCHAR(MAX),
SMS_CC_From INT FOREIGN KEY REFERENCES CountryCode(CC_AUID),
SMS_CC_To INT FOREIGN KEY REFERENCES CountryCode(CC_AUID),
SMS_State BIT,
SMS_DateTime DATETIME
)

GO

INSERT INTO SMS 
(SMS_From, SMS_To, SMS_Text, SMS_CC_From, SMS_CC_To, SMS_State, SMS_DateTime)
VALUES 
('sender 11', 'receiver 11', 'Message 12', 2, 2, 1, '2021-01-15 01:40:38.377'),
('sender 22', 'receiver 21', 'Message 12', 3, 3, 1, '2021-02-20 01:40:38.377'),
('sender 13', 'receiver 12', 'Message 12', 1, 2, 1, '2021-07-11 01:40:38.377'),
('sender 14', 'receiver 23', 'Message 12', 1, 1, 1, '2021-05-12 01:40:38.377'),
('sender 14', 'receiver 42', 'Message 12', 2, 1, 1, '2021-05-05 01:40:38.377'),
('sender 35', 'receiver 25', 'Message 12', 3, 1, 1, '2021-05-03 01:40:38.377'),
('sender 21', 'receiver 41', 'Message 12', 2, 3, 0, '2021-05-15 01:40:38.377'),
('sender 12', 'receiver 52', 'Message 12', 1, 2, 1, '2021-05-13 01:40:38.377'),
('sender 13', 'receiver 11', 'Message 12', 1, 2, 1, '2021-05-17 01:40:38.377'),
('sender 24', 'receiver 31', 'Message 12', 1, 2, 1, '2021-05-23 01:40:38.377'),
('sender 31', 'receiver 34', 'Message 12', 1, 2, 1, '2021-05-29 01:40:38.377')

GO

CREATE PROCEDURE GetTotalNumberOfSMSWithParams
@DateFrom DATETIME,
@DateTo DATETIME,
@skip INT,
@take INT

AS

WITH cte 
AS
(
SELECT
	*
FROM SMS
WHERE SMS_DateTime BETWEEN @DateFrom AND @DateTo
ORDER BY SMS_DateTime DESC
OFFSET @skip ROWS 
FETCH NEXT @take ROWS ONLY
)

SELECT COUNT(1) FROM cte

--EXEC GetTotalNumberOfSMSWithParams '2020-06-20 01:40:38.377', '2022-06-20 01:40:38.377', 5, 21
---------------------------------------------------------

GO

CREATE PROCEDURE GetSMSWithParams
@DateFrom DATETIME,
@DateTo DATETIME,
@skip INT,
@take INT

AS

SELECT
	SMS_DateTime AS dateTime,
	c2.CC_MobileCountryCode AS mcc,
	SMS_From AS _from,
	SMS_To AS _to,
	c2.CC_PricePerSMS AS price,
	SMS_State AS state
FROM SMS
INNER JOIN CountryCode c1 ON
c1.CC_AUID = SMS_CC_From
INNER JOIN CountryCode c2 ON
c2.CC_AUID = SMS_CC_To
WHERE SMS_DateTime BETWEEN @DateFrom AND @DateTo
ORDER BY SMS_DateTime DESC
OFFSET @skip ROWS 
FETCH NEXT @take ROWS ONLY

--EXEC GetSMSWithParams '2020-06-20 01:40:38.377', '2022-06-20 01:40:38.377', 5, 21
----------------------------------------------

--GO

--SELECT 
--CONCAT(YEAR(SMS_DateTime), MONTH(SMS_DateTime), DAY(SMS_DateTime)) AS Date, 
--CC_MobileCountryCode AS mcc, 
--SUM(CC_PricePerSMS) AS price,
--COUNT(1) AS NumberOfMessages
--from sms
--inner join CountryCode on
--CC_AUID = SMS_CC_From 
--group by CC_MobileCountryCode, CONCAT(YEAR(SMS_DateTime), MONTH(SMS_DateTime), DAY(SMS_DateTime))

--SELECT
--*
--from sms
--inner join CountryCode on
--CC_AUID = SMS_CC_From