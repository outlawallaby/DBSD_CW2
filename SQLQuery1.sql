GO
set language english;
GO

CREATE TABLE [dbo].[Product]
(
    [ProductId] INT NOT NULL IDENTITY,
    [ProductName] NVARCHAR(70) NOT NULL,
    [ProductVendor] NVARCHAR(70) NOT NULL, 
    [QuantityInStock] INT NOT NULL,
    [Price] DECIMAL(10,2) NOT NULL,
    [ProductDetailId] INT NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductId])
);
GO
CREATE TABLE [dbo].[ProductDetail]
(
    [ProductDetailId] INT NOT NULL IDENTITY,
    [ProductDescription] Text NOT NULL,
    CONSTRAINT [PK_ProductDetail] PRIMARY KEY CLUSTERED ([ProductDetailId])
)
GO
CREATE TABLE [dbo].[Customer]
(
    [CustomerId] INT NOT NULL IDENTITY,
    [FirstName] NVARCHAR(40) NOT NULL,
    [LastName] NVARCHAR(20) NOT NULL,
    [ReportsTo] INT,
    [BirthDate] DATETIME,
    [City] NVARCHAR(40),
    [Address] NVARCHAR(70),
    [Phone] NVARCHAR(24),
    [Email] NVARCHAR(60) NOT NULL,
    [SupportRepId] INT,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerId])
);
GO
CREATE TABLE [dbo].[Employee]
(
    [EmployeeId] INT NOT NULL IDENTITY,
    [LastName] NVARCHAR(20) NOT NULL,
    [FirstName] NVARCHAR(20) NOT NULL,
    [JobTitle] NVARCHAR(50) NOT NULL,
    [Department] NVARCHAR(10) NOT NULL,
    [ReportsTo] INT,
    [BirthDate] DATETIME,
    [HireDate] DATETIME,
    [Address] NVARCHAR(70),
    [City] NVARCHAR(40),
    [Phone] NVARCHAR(24),
    [Email] NVARCHAR(60),
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeId])
);
GO
CREATE TABLE [dbo].[Order]
(
     [OrderId] INT NOT NULL IDENTITY,
     [CustomerId] INT NOT NULL,
     [OrderDate] DATETIME NOT NULL,
     [RequiredDate] DATETIME NOT NULL,
     [ShippedDate] DATETIME DEFAULT NULL,
     [Status] NVARCHAR(15) NOT NULL,
     CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderId])
);


GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [FK_ProductProductDetailId]
    FOREIGN KEY ([ProductDetailId]) REFERENCES [dbo].[ProductDetail]([ProductDetailId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

Go
CREATE INDEX [IFK_ProductProductDetailId] ON [dbo].[Product] ([ProductDetailId]);

GO
ALTER TABLE [dbo].[Customer] ADD CONSTRAINT [FK_CustomerSupportRepId]
    FOREIGN KEY ([SupportRepId]) REFERENCES [dbo].[Employee] ([EmployeeId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

Go
CREATE INDEX [IFK_CustomerSupportRepId] ON [dbo].[Customer] ([SupportRepId]);

GO
ALTER TABLE [dbo].[Employee] ADD CONSTRAINT [FK_EmployeeReportsTo]
    FOREIGN KEY ([ReportsTo]) REFERENCES [dbo].[Employee] ([EmployeeId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

GO
CREATE INDEX [IFK_EmployeeReportsTo] ON [dbo].[Employee] ([ReportsTo]);

Go
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_OrderCustomerId]
    FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

GO
CREATE INDEX [IFK_OrderCustomerId] ON [dbo].[Order] ([CustomerId]);

INSERT INTO [dbo].[Product] ([ProductName],[ProductVendor],[QuantityInStock],[Price],[ProductDetailId]) VALUES (N'Note6 pro',N'Redmi',9,220,1);

INSERT INTO [dbo].[ProductDetail] ([ProductDescription]) VALUES (N'Note6 pro');