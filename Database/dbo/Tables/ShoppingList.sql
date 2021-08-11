CREATE TABLE [dbo].[ShoppingList]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    [Code] NVARCHAR(50) NOT NULL, 
    [Price] INT NOT NULL, 
    [Qty] INT NOT NULL, 
    [Veg] NVARCHAR(50) NOT NULL, 
    [QtyToBuy] INT NULL, 
    [Summary] INT NULL, 
   
)
