CREATE TABLE [dbo].[ShoppingList] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Description] NVARCHAR (250) NOT NULL,
    [Code]        NVARCHAR (50) NOT NULL,
    [Price]       INT           NOT NULL,
    [Qty]         INT           NOT NULL,
    [Veg]         NVARCHAR (50) NOT NULL,
    [QtyToBuy]    INT           NULL,
    [Summary]     INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

