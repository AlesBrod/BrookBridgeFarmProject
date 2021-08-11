CREATE TABLE [dbo].[ShoppingList]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(50) NOT NULL, 
    [Price] INT NOT NULL, 
    [QtyToBuy] INT NOT NULL
)
