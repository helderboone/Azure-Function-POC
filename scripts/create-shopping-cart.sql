BEGIN TRANSACTION;
GO

CREATE TABLE [ShoppingCart] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    [HasVoucher] bit NOT NULL,
    [Discount] decimal(18,2) NOT NULL,
    [Voucher_Percentage] decimal(18,2) NULL,
    [Voucher_Discount] decimal(18,2) NULL,
    [Voucher_Code] varchar(50) NOT NULL,
    [Voucher_DiscountType] int NOT NULL,
    CONSTRAINT [PK_ShoppingCart] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CartItems] (
    [Id] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [Name] varchar(100) NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Image] varchar(100) NOT NULL,
    [ShoppingCartId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_CartItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CartItems_ShoppingCart_ShoppingCartId] FOREIGN KEY ([ShoppingCartId]) REFERENCES [ShoppingCart] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CartItems_ShoppingCartId] ON [CartItems] ([ShoppingCartId]);
GO

CREATE INDEX [IDX_Customer] ON [ShoppingCart] ([CustomerId]);
GO