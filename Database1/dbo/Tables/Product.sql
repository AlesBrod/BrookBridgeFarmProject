CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    [Code] NVARCHAR(50) NOT NULL, 
    [Price] INT NOT NULL, 
    [Qty] INT NOT NULL, 
    [Veg] NVARCHAR(50) NOT NULL, 
    [QtyToBuy] INT NULL
)
