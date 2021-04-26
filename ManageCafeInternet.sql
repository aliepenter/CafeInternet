CREATE DATABASE ManageCafeInternet
GO
USE ManageCafeInternet
GO
CREATE TABLE [food_type]
(
	entity_id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(255) NOT NULL
)
GO
CREATE TABLE [area]
(
	entity_id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(255) NOT NULL,
	price FLOAT NOT NULL
)
GO
CREATE TABLE [role]
(
	entity_id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(255) NOT NULL,
)
GO
CREATE TABLE [food]
(
	entity_id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(255) NOT NULL,
	price FLOAT NOT NULL,
	quantity INT NOT NULL,
	image NVARCHAR(255) NOT NULL,
	food_type_id INT NOT NULL,
	FOREIGN KEY (food_type_id) REFERENCES [food_type](entity_id)
)
GO
CREATE TABLE [user]
(
	entity_id INT PRIMARY KEY IDENTITY,
	account NVARCHAR(255) NOT NULL,
	password NVARCHAR(255) NOT NULL,
	name NVARCHAR(255),
	image NVARCHAR(255) NOT NULL,
	role_id INT NOT NULL,
	FOREIGN KEY (role_id) REFERENCES [role](entity_id)
)
GO
CREATE TABLE [computer]
(
	entity_id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(255) NOT NULL,
	area_id INT NOT NULL,
	status TINYINT NOT NULL,
	total_used_time INT NOT NULL,
	profit FLOAT NOT NULL,
	FOREIGN KEY (area_id) REFERENCES [area](entity_id)
)
GO
CREATE TABLE [computer_status]
(
	entity_id INT PRIMARY KEY IDENTITY,
	computer_id INT NOT NULL,
	name NVARCHAR(255) NOT NULL,
	area_id INT NOT NULL,
	status TINYINT NOT NULL,
	used_times INT NOT NULL,
	service_charge FLOAT,
	start_time DATETIME NOT NULL,
	end_time DATETIME NOT NULL,
	food_id NVARCHAR(255),
	FOREIGN KEY (area_id) REFERENCES [area](entity_id)
)
GO
CREATE TABLE [service]
(
	entity_id INT PRIMARY KEY IDENTITY,
	computer_id INT NOT NULL,
	service_name NVARCHAR(255) NOT NULL,
	quantity INT NOT NULL,
	total FLOAT NOT NULL,
	status TINYINT NOT NULL
)
GO

CREATE TABLE [order]
(
	entity_id INT PRIMARY KEY IDENTITY,
	computer_id INT NOT NULL,
	computer_name NVARCHAR(255) NOT NULL,
	s_time DATETIME NOT NULL,
	e_time DATETIME NOT NULL,
	tt_time INT NOT NULL,
	servie FLOAT NOT NULL
)
GO
CREATE TABLE [report]
(
	entity_id INT PRIMARY KEY IDENTITY,
	[date] DATETIME NOT NULL,
	[time] DATETIME NOT NULL,
	[information] NVARCHAR(255) NOT NULL,
	[performer] NVARCHAR(255) NOT NULL,
	[activity] NVARCHAR(255) NOT NULL,
	[type] INT NOT NULL
)
GO

INSERT INTO [role] VALUES
(N'admin'),
(N'Inventory Manager'),
(N'Shop Manager')
GO
INSERT INTO [user] VALUES
(N'admin',N'123',N'Đặng Tuấn Đạt',N'D:\CafeInternet\CafeInternet\Resources\avatar.jpg',1),
(N'user1',N'123',N'Đỗ Hồng Ngọc',N'D:\CafeInternet\CafeInternet\Resources\avatar.jpg',2),
(N'user2',N'123',N'Nguyễn Tuấn Minh',N'D:\CafeInternet\CafeInternet\Resources\redsting.jpg',3)
GO
INSERT INTO [food_type] VALUES
(N'Foods'),
(N'Bottled drinks'),
(N'Concoction drinks'),
(N'Others')
GO
INSERT INTO [food] VALUES
(N'Red Sting',10000,10,N'D:\CafeInternet\CafeInternet\Resources\redsting.jpg',1),
(N'Yellow Sting',10000,20,N'D:\CafeInternet\CafeInternet\Resources\yellowsting.jpg',2),
(N'Egg Cafe',15000,10,N'D:\CafeInternet\CafeInternet\Resources\eggcafe.jpg',3),
(N'Egg2 Cafe',15000,10,N'D:\CafeInternet\CafeInternet\Resources\eggcafe.jpg',4)
GO
INSERT INTO [area] VALUES
(N'Pro',5000),
(N'Vip',10000),
(N'Challenge',15000)
GO
INSERT INTO [computer] VALUES
(N'PC01',1,0,100,500000),
(N'PC02',1,0,0,0),
(N'PC03',1,0,0,0),
(N'PC04',1,0,0,0),
(N'PC05',1,0,0,0),
(N'PC06',1,0,0,0),
(N'PC07',1,0,0,7500000),
(N'PC11',2,0,800,8000000),
(N'PC21',3,0,500,7500000)
GO
CREATE PROC [login_permission] 
@account NVARCHAR(255), @password NVARCHAR(255)
AS
SELECT * FROM [user] WHERE account = @account AND password = @password
GO
CREATE PROC [get_infor] 
@account NVARCHAR(255)
AS
SELECT * FROM [user] WHERE account = @account
GO
CREATE PROC [get_food]
AS
SELECT * FROM [food]
GO
CREATE PROC [get_price_area]
AS
SELECT 
a.entity_id as 'Area',
a.name as 'Name',
a.price as 'Price',
COUNT(c.area_id) as 'Number of computers'
FROM computer c
right join area a on a.entity_id = c.area_id
group by a.entity_id,a.price,a.name
GO

CREATE PROC [send_data]
AS
INSERT INTO [computer_status](computer_id,name, area_id, status,used_times,service_charge,start_time, end_time)
SELECT [computer].entity_id, [computer].name, [computer].area_id,0,0,0,CURRENT_TIMESTAMP,0 FROM [computer]
WHERE [computer].status = 0;
GO

CREATE PROC [search_area]
@name NVARCHAR(255)
AS
SELECT
a.entity_id as 'Area',
a.name as 'Name',
a.price as 'Price',
COUNT(c.area_id) as 'Number of computers'
FROM computer c
right join area a on a.entity_id = c.area_id
WHERE a.name like @name
group by a.entity_id,a.price,a.name
GO

CREATE PROC [getAllComputersFromArea]
AS
SELECT	[computer].entity_id AS 'Computer Id',
		[computer].name AS 'Computer Name',
		[area].name AS 'Area',
		[area].price AS 'Price'
FROM computer
JOIN area
ON [computer].area_id = area.entity_id
GO
CREATE PROC [getAllComputersFromComputerStatus]
AS
SELECT	[computer_status].computer_id AS 'Computer Id',
		[computer_status].name AS 'Computer Name',
		(CASE WHEN [computer_status].status = 0 THEN 'OFFLINE' ELSE 'ONLINE' END) AS 'Status',
		[area].name AS 'Area',
		[area].price AS 'Price',
		[computer_status].start_time AS 'Start Time',
		[computer_status].used_times AS 'Used Time',
		[computer_status].service_charge AS 'Service Money'
FROM computer_status
JOIN area
ON [computer_status].area_id = area.entity_id
GO

CREATE PROC [deleteComputerStatus]
AS
DELETE [computer_status]
GO

CREATE PROC [deleteService]
AS
DELETE [service]
GO
CREATE PROC [deleteServiceCondition]
@id INT
AS
DELETE [service]
WHERE [service].computer_id = @id
GO

CREATE PROC [getService]
@id INT
AS
SELECT	[service].computer_id AS 'Computer ID',	
		[service].service_name AS 'Name',
		[service].quantity AS 'Amont',
		[service].total AS 'Total'
FROM [service]
WHERE [service].computer_id = @id
GO

CREATE PROC [get_service_price]
AS
SELECT 
s.computer_id as 'Computer ID',
s.service_name as 'Service Name',
s.quantity as 'Quantity',
s.total as 'Total'
FROM service s
GO

CREATE PROC [sum_service]
@id INT
AS
SELECT
[computer].name AS 'Name',
SUM(total) AS 'total_money'
FROM [service]
JOIN [computer]
ON [service].computer_id = @id
WHERE [service].status = 0
GROUP BY [computer].name
GO



CREATE PROC [getTotalServiceMoney]
@id INT
AS
DECLARE @total INT
BEGIN 
SET @total =  (SELECT SUM(total) FROM [service]
WHERE computer_id = @id)
END
RETURN @total
GO

ALTER PROC [getServiceMoney]
@id INT
AS
SELECT  [service].entity_id AS 'Id',
		[computer_status].name AS 'Computer Name',
		[service].computer_id AS 'Computer Id',
		[service].service_name AS 'Service Name',
		[service].quantity AS 'Quantity',
		[service].total AS 'Total'
FROM [service]
JOIN [computer_status]
ON [computer_status].computer_id = [service].computer_id
WHERE [service].computer_id = @id
GO

CREATE PROC [getOrder]
@id INT
AS
SELECT TOP 1 
		[order].computer_name AS 'PC',
		[order].s_time AS 'Start Time',
		[order].e_time AS 'End Time',
		[order].tt_time AS'Total Used Time',
		[order].servie AS 'Service Monery' 
FROM [order]
WHERE computer_id = 8
ORDER BY entity_id DESC
GO

CREATE PROC [getComputerStatus]
@id INT
AS
DECLARE @status TINYINT
BEGIN 
SET @status =  (SELECT [computer_status].status FROM computer_status
WHERE computer_id = @id)
END
RETURN @status
GO

SELECT * FROM [food]
GO

CREATE PROC [deleteS]
@id INT
AS
DELETE [service]
WHERE [service].entity_id = @id
GO