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
	start_time DATETIME NOT NULL,
	end_time DATETIME NOT NULL,
	food_id NVARCHAR(255),
	FOREIGN KEY (area_id) REFERENCES [area](entity_id)
)
GO
CREATE TABLE [service]
(
	entity_id INT PRIMARY KEY IDENTITY,
	computer_name NVARCHAR(255) NOT NULL,
	service_name NVARCHAR(255) NOT NULL,
	quantity INT NOT NULL,
	total FLOAT NOT NULL
)
GO
SELECT * FROM [food_type]
GO
CREATE TABLE [order]
(
	entity_id INT PRIMARY KEY IDENTITY,
	computer_status_id INT NOT NULL,
	FOREIGN KEY (computer_status_id) REFERENCES [computer_status](entity_id)
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
(N'admin',N'123',N'Đặng Tuấn Đạt',N'E:/CafeInternet/CafeInternet/Resources/avatar.jpg',1),
(N'user1',N'123',N'Đỗ Hồng Ngọc',N'E:/CafeInternet/CafeInternet/Resources/avatar.jpg',2),
(N'user2',N'123',N'Nguyễn Tuấn Minh',N'E:/CafeInternet/CafeInternet/Resources/redsting.jpg',3)
GO
INSERT INTO [food_type] VALUES
(N'Foods'),
(N'Bottled drinks'),
(N'Concoction drinks'),
(N'Others')
GO
INSERT INTO [food] VALUES
(N'Red Sting',10000,10,N'E:/CafeInternet/CafeInternet/Resources/redsting.jpg',1),
(N'Yellow Sting',10000,20,N'E:/CafeInternet/CafeInternet/Resources/yellowsting.jpg',2),
(N'Egg Cafe',15000,10,N'E:/CafeInternet/CafeInternet/Resources/eggcafe.jpg',3),
(N'Egg2 Cafe',15000,10,N'E:/CafeInternet/CafeInternet/Resources/eggcafe.jpg',4)
GO
INSERT INTO [area] VALUES
(N'Pro',5000),
(N'Vip',10000),
(N'Challenge',15000)
GO
INSERT INTO [computer] VALUES
(N'PC01',1,0,100,500000),
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
SELECT * FROM [area]
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
INSERT INTO [computer_status](computer_id,name, area_id, status,used_times,start_time, end_time)
SELECT [computer].entity_id, [computer].name, [computer].area_id,0,0,CURRENT_TIMESTAMP,0 FROM [computer]
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
		[computer_status].used_times AS 'Used_time'
FROM computer_status
JOIN area
ON [computer_status].area_id = area.entity_id
GO

CREATE PROC [deleteComputerStatus]
AS
DELETE [computer_status]
GO