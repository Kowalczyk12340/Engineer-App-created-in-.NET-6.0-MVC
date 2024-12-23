-- Build started...
-- Build succeeded.
-- The Entity Framework tools version '6.0.1' is older than that of the runtime '6.0.2'. Update the tools for the latest features and bug fixes. See https://aka.ms/AAc1fbw for more information.
-- info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
--      Entity Framework Core 6.0.2 initialized 'EngineerDbContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer:6.0.2' with options: None
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NULL,
    [StreetAddress] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [PostalCode] nvarchar(max) NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [WebImages] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Picture] varbinary(max) NOT NULL,
    CONSTRAINT [PK_WebImages] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220224141903_SetApplicationUserEntity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [DisplayOrder] int NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220224142052_CategoryEntity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Frequency] (
    [Id] int NOT NULL IDENTITY,
    [FrequencyCount] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Frequency] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220224142308_FrequencyEntity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Commodity] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Price] float NOT NULL,
    [LongDesc] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [CategoryId] int NOT NULL,
    [FrequencyId] int NOT NULL,
    CONSTRAINT [PK_Commodity] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Commodity_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Commodity_Frequency_FrequencyId] FOREIGN KEY ([FrequencyId]) REFERENCES [Frequency] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Commodity_CategoryId] ON [Commodity] ([CategoryId]);
GO

CREATE INDEX [IX_Commodity_FrequencyId] ON [Commodity] ([FrequencyId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220224142405_CommodityEntity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [OrderHeader] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [ZipCode] nvarchar(max) NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Comments] nvarchar(max) NOT NULL,
    [CommodityCount] int NOT NULL,
    CONSTRAINT [PK_OrderHeader] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OrderDetails] (
    [Id] int NOT NULL IDENTITY,
    [OrderHeaderId] int NOT NULL,
    [CommodityId] int NOT NULL,
    [CommodityName] nvarchar(max) NOT NULL,
    [Price] float NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderDetails_Commodity_CommodityId] FOREIGN KEY ([CommodityId]) REFERENCES [Commodity] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_OrderHeader_OrderHeaderId] FOREIGN KEY ([OrderHeaderId]) REFERENCES [OrderHeader] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_OrderDetails_CommodityId] ON [OrderDetails] ([CommodityId]);
GO

CREATE INDEX [IX_OrderDetails_OrderHeaderId] ON [OrderDetails] ([OrderHeaderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220224142523_OrderHeaderAndOrderDetails', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WebImages]') AND [c].[name] = N'Picture');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [WebImages] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [WebImages] ALTER COLUMN [Picture] varbinary(max) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderHeader]') AND [c].[name] = N'Status');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [OrderHeader] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [OrderHeader] ALTER COLUMN [Status] nvarchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderHeader]') AND [c].[name] = N'Comments');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [OrderHeader] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [OrderHeader] ALTER COLUMN [Comments] nvarchar(max) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commodity]') AND [c].[name] = N'LongDesc');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Commodity] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Commodity] ALTER COLUMN [LongDesc] nvarchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commodity]') AND [c].[name] = N'ImageUrl');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Commodity] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Commodity] ALTER COLUMN [ImageUrl] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220224161358_ImproveModels', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Commodity] ADD [SupplierId] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Supplier] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [Street] nvarchar(max) NULL,
    [PostalCode] nvarchar(max) NULL,
    [PhoneNumer] nvarchar(max) NOT NULL,
    [EmailAddress] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Supplier] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Commodity_SupplierId] ON [Commodity] ([SupplierId]);
GO

ALTER TABLE [Commodity] ADD CONSTRAINT [FK_Commodity_Supplier_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Supplier] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220225221914_AddSupplierEntity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Supplier].[PhoneNumer]', N'PhoneNumber', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220225230744_ImproveSupplierNamingConvention', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Commodity] ADD [OfferId] int NULL;
GO

CREATE TABLE [Offer] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Offer] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Commodity_OfferId] ON [Commodity] ([OfferId]);
GO

ALTER TABLE [Commodity] ADD CONSTRAINT [FK_Commodity_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220226005353_AddOfferEntity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Commodity] DROP CONSTRAINT [FK_Commodity_Offer_OfferId];
GO

DROP INDEX [IX_Commodity_OfferId] ON [Commodity];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commodity]') AND [c].[name] = N'OfferId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Commodity] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Commodity] DROP COLUMN [OfferId];
GO

ALTER TABLE [Offer] ADD [Count] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Offer] ADD [OfferDesc] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220226021207_ImproveOffer', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Offer] ADD [CommodityId] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_Offer_CommodityId] ON [Offer] ([CommodityId]);
GO

ALTER TABLE [Offer] ADD CONSTRAINT [FK_Offer_Commodity_CommodityId] FOREIGN KEY ([CommodityId]) REFERENCES [Commodity] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220226185146_SetOffer', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[OrderHeader].[CommodityCount]', N'Count', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220226202234_ImproveOfferAndOrder', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[OrderHeader].[Count]', N'CommodityCount', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220227183619_ImproveOrderHeaderEntity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Commodity] ADD [DeliveryId] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Delivery] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [DeliveryDesc] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Delivery] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Commodity_DeliveryId] ON [Commodity] ([DeliveryId]);
GO

ALTER TABLE [Commodity] ADD CONSTRAINT [FK_Commodity_Delivery_DeliveryId] FOREIGN KEY ([DeliveryId]) REFERENCES [Delivery] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220302014358_DeliveryEntity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Service] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Price] float NOT NULL,
    [LongDesc] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [CategoryId] int NOT NULL,
    [FrequencyId] int NOT NULL,
    [DeliveryId] int NOT NULL,
    [SupplierId] int NOT NULL,
    CONSTRAINT [PK_Service] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Service_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Service_Delivery_DeliveryId] FOREIGN KEY ([DeliveryId]) REFERENCES [Delivery] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Service_Frequency_FrequencyId] FOREIGN KEY ([FrequencyId]) REFERENCES [Frequency] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Service_Supplier_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Supplier] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Service_CategoryId] ON [Service] ([CategoryId]);
GO

CREATE INDEX [IX_Service_DeliveryId] ON [Service] ([DeliveryId]);
GO

CREATE INDEX [IX_Service_FrequencyId] ON [Service] ([FrequencyId]);
GO

CREATE INDEX [IX_Service_SupplierId] ON [Service] ([SupplierId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220319143059_AddServiceEntity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Service] DROP CONSTRAINT [FK_Service_Delivery_DeliveryId];
GO

ALTER TABLE [Service] DROP CONSTRAINT [FK_Service_Supplier_SupplierId];
GO

DROP TABLE [Offer];
GO

DROP INDEX [IX_Service_DeliveryId] ON [Service];
GO

DROP INDEX [IX_Service_SupplierId] ON [Service];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Service]') AND [c].[name] = N'DeliveryId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Service] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Service] DROP COLUMN [DeliveryId];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Service]') AND [c].[name] = N'SupplierId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Service] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Service] DROP COLUMN [SupplierId];
GO

CREATE TABLE [Employee] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [EmailAddress] nvarchar(max) NOT NULL,
    [EmployeeDesc] nvarchar(max) NOT NULL,
    [ServiceId] int NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employee_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Service] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Employee_ServiceId] ON [Employee] ([ServiceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220325232336_ImproveTheStatusOfRoleAndDatabaseModelClassDiagram', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Commodity] DROP CONSTRAINT [FK_Commodity_Delivery_DeliveryId];
GO

ALTER TABLE [Commodity] DROP CONSTRAINT [FK_Commodity_Frequency_FrequencyId];
GO

ALTER TABLE [Commodity] DROP CONSTRAINT [FK_Commodity_Supplier_SupplierId];
GO

ALTER TABLE [Service] DROP CONSTRAINT [FK_Service_Frequency_FrequencyId];
GO

DROP TABLE [Frequency];
GO

DROP INDEX [IX_Commodity_DeliveryId] ON [Commodity];
GO

DROP INDEX [IX_Commodity_FrequencyId] ON [Commodity];
GO

DROP INDEX [IX_Commodity_SupplierId] ON [Commodity];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commodity]') AND [c].[name] = N'DeliveryId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Commodity] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Commodity] DROP COLUMN [DeliveryId];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commodity]') AND [c].[name] = N'FrequencyId');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Commodity] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Commodity] DROP COLUMN [FrequencyId];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commodity]') AND [c].[name] = N'SupplierId');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Commodity] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Commodity] DROP COLUMN [SupplierId];
GO

EXEC sp_rename N'[Service].[FrequencyId]', N'PaymentId', N'COLUMN';
GO

EXEC sp_rename N'[Service].[IX_Service_FrequencyId]', N'IX_Service_PaymentId', N'INDEX';
GO

ALTER TABLE [OrderHeader] ADD [DeliveryId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [OrderHeader] ADD [PaymentId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [OrderHeader] ADD [SupplierId] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Payment] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Payment] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_OrderHeader_DeliveryId] ON [OrderHeader] ([DeliveryId]);
GO

CREATE INDEX [IX_OrderHeader_PaymentId] ON [OrderHeader] ([PaymentId]);
GO

CREATE INDEX [IX_OrderHeader_SupplierId] ON [OrderHeader] ([SupplierId]);
GO

ALTER TABLE [OrderHeader] ADD CONSTRAINT [FK_OrderHeader_Delivery_DeliveryId] FOREIGN KEY ([DeliveryId]) REFERENCES [Delivery] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [OrderHeader] ADD CONSTRAINT [FK_OrderHeader_Payment_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [Payment] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [OrderHeader] ADD CONSTRAINT [FK_OrderHeader_Supplier_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Supplier] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Service] ADD CONSTRAINT [FK_Service_Payment_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [Payment] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220328225123_ChangeTheStructureForCounting', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [OrderHeader] ADD [Amount] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220329170401_AddAmountProp', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderHeader]') AND [c].[name] = N'Amount');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [OrderHeader] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [OrderHeader] DROP COLUMN [Amount];
GO

ALTER TABLE [Commodity] ADD [Amount] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220329173540_AddAmountPropToCommodity', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [OrderHeader] ADD [TimeToOrder] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [OrderHeader] ADD [TimeToRealisation] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220412161205_AddTheDateTimeToOrder', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [OrderHeader] ADD [InfoToPdf] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220427184224_AddInfoToPdf', N'6.0.2');
GO

COMMIT;
GO


