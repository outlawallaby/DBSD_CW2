USE [USMALL]
GO
set language english;
GO

CREATE TABLE [dbo].[Product]
(
    [ProductId] INT NOT NULL IDENTITY,
    [ProductName] NVARCHAR(70) NOT NULL,
    [ProductVendor] NVARCHAR(70) NOT NULL,
    [ProductDescription] Text NOT NULL,



    [ProductDetailsId] INT NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductId])
);