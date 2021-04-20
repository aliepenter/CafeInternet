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
	receipt_date datetime NOT NULL,
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
	status TINYINT NOT NULL,
	start_time DATETIME NOT NULL,
	end_time DATETIME NOT NULL,
	food_id NVARCHAR(255)
)
GO
CREATE TABLE [order]
(
	entity_id INT PRIMARY KEY IDENTITY,
	computer_status_id INT NOT NULL,
	FOREIGN KEY (computer_status_id) REFERENCES [computer_status](entity_id)
)
GO
INSERT INTO [role] VALUES
(N'admin'),
(N'Inventory Manager'),
(N'SHop Manager')
GO
INSERT INTO [user] VALUES
(N'admin',N'123',N'Đặng Tuấn Đạt',N'C:/Users/dangt/Desktop/CafeInternet/CafeInternet/Resources/avatar.jpg',1),
(N'user1',N'123',N'Đỗ Hồng Ngọc',N'C:/Users/dangt/Desktop/CafeInternet/CafeInternet/Resources/avatar.jpg',2),
(N'user2',N'123',N'Nguyễn Tuấn Minh',N'C:/Users/dangt/Desktop/CafeInternet/CafeInternet/Resources/redsting.jpg',3)
GO
INSERT INTO [food_type] VALUES
(N'Đồ ăn'),
(N'Đồ uống đóng chai'),
(N'Đồ uống pha chế')
GO
INSERT INTO [food] VALUES
(N'Red Sting',0.5,10,N'C:/Users/dangt/Desktop/CafeInternet/CafeInternet/Resources/redsting.jpg','12/02/2021',2),
(N'Yellow Sting',0.5,20,N'C:/Users/dangt/Desktop/CafeInternet/CafeInternet/Resources/yellowsting.jpg','12/02/2021',2),
(N'Egg Cafe',1.5,10,N'C:/Users/dangt/Desktop/CafeInternet/CafeInternet/Resources/eggcafe.jpg','12/02/2021',3)
GO
INSERT INTO [area] VALUES
(N'Pro',0.5),
(N'Vip',0.75),
(N'Challenge',1)
GO
INSERT INTO [computer] VALUES
(N'PC01',1,1,100,50),
(N'PC11',2,0,800,600),
(N'PC21',3,1,500,500)
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
@id INT
AS
SELECT price FROM area WHERE entity_id = @id
GO
