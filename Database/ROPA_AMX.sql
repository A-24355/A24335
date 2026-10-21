-- =====================================================
-- Base de datos: ROPA_AMX
-- =====================================================
CREATE DATABASE ROPA_AMX;
GO
USE ROPA_AMX;
GO

CREATE TABLE dbo.Categorias (
    Id     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,   -- Hombre, Mujer, Niño
    Slug   VARCHAR(50) NOT NULL UNIQUE  -- hombre, mujer, ninos (para la URL)
);
GO

CREATE TABLE dbo.Productos (
    Id            UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Nombre        VARCHAR(100)  NOT NULL,
    Descripcion   VARCHAR(250)  NULL,
    Precio        DECIMAL(10,2) NOT NULL,
    PrecioRebaja  DECIMAL(10,2) NULL,        -- si tiene valor, aparece en REBAJAS
    Tipo          VARCHAR(30)   NULL,        -- Camisas, Sudaderas, Pantalones, Accesorios
    Existencia    INT           NOT NULL DEFAULT 0,
    EsMasVendido  BIT           NOT NULL DEFAULT 0,
    ImagenUrl     VARCHAR(250)  NULL,        -- ej. /img/playera-polo.jpg
    IdCategoria   INT           NOT NULL,
    FechaRegistro DATETIME      NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Productos_Categorias
        FOREIGN KEY (IdCategoria) REFERENCES dbo.Categorias(Id)
);
GO

CREATE TABLE dbo.Usuarios (
    Id                UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    NombreCompleto    VARCHAR(120) NULL,
    CorreoElectronico VARCHAR(150) NULL,
    Password          VARCHAR(250) NULL,
    Activo            BIT NULL DEFAULT 1,
    FechaRegistro     DATETIME NULL DEFAULT GETDATE()
);
GO

-- ---------- Datos de ejemplo ----------
INSERT INTO dbo.Categorias (Nombre, Slug) VALUES
 ('Hombre', 'hombre'),
 ('Mujer',  'mujer'),
 ('Niño',   'ninos');
GO

INSERT INTO dbo.Productos (Nombre, Descripcion, Precio, PrecioRebaja, Tipo, Existencia, EsMasVendido, ImagenUrl, IdCategoria) VALUES
 ('Playera polo',      'Playera polo de alta calidad, 100% algodon', 399.59, NULL,   'Camisas',    25, 1, '/img/playera-polo.jpg', (SELECT Id FROM dbo.Categorias WHERE Slug='hombre')),
 ('Camisa de lino',    'Camisa de lino ligera, ideal para clima calido', 549.00, 449.00, 'Camisas',  15, 0, NULL, (SELECT Id FROM dbo.Categorias WHERE Slug='hombre')),
 ('Sudadera estampada','Sudadera blanca con estampado artesanal',   699.90, NULL,   'Sudaderas',  12, 1, NULL, (SELECT Id FROM dbo.Categorias WHERE Slug='hombre')),
 ('Gorra negra',       'Gorra bordada, ajustable',                   229.00, 179.00, 'Accesorios', 40, 0, NULL, (SELECT Id FROM dbo.Categorias WHERE Slug='hombre')),
 ('Blusa rayada',      'Blusa de rayas con lazo al cuello',          459.50, NULL,   'Camisas',    18, 1, NULL, (SELECT Id FROM dbo.Categorias WHERE Slug='mujer')),
 ('Pantalon de vestir','Pantalon cafe de corte amplio',              629.00, 529.00, 'Pantalones', 10, 0, NULL, (SELECT Id FROM dbo.Categorias WHERE Slug='mujer')),
 ('Sudadera oversize', 'Sudadera suave de felpa',                    649.00, NULL,   'Sudaderas',  14, 0, NULL, (SELECT Id FROM dbo.Categorias WHERE Slug='mujer')),
 ('Playera basica',    'Playera de algodon para nino',               189.00, NULL,   'Camisas',    30, 1, NULL, (SELECT Id FROM dbo.Categorias WHERE Slug='ninos')),
 ('Pantalon jogger',   'Jogger comodo para uso diario',              319.00, 259.00, 'Pantalones', 22, 0, NULL, (SELECT Id FROM dbo.Categorias WHERE Slug='ninos'));
GO

SELECT c.Nombre AS Categoria, p.Nombre, p.Precio, p.Tipo
FROM dbo.Productos p JOIN dbo.Categorias c ON c.Id = p.IdCategoria;
GO
