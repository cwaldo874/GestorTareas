/****** Object:  Database [GestorTarea]    Script Date: 17/10/2023 7:27:39 a. m. ******/
CREATE DATABASE [GestorTarea]  (EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_S_Gen5_1', MAXSIZE = 32 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [GestorTarea] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [GestorTarea] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GestorTarea] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GestorTarea] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GestorTarea] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GestorTarea] SET ARITHABORT OFF 
GO
ALTER DATABASE [GestorTarea] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GestorTarea] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GestorTarea] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GestorTarea] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GestorTarea] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GestorTarea] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GestorTarea] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GestorTarea] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GestorTarea] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [GestorTarea] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GestorTarea] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [GestorTarea] SET  MULTI_USER 
GO
ALTER DATABASE [GestorTarea] SET ENCRYPTION ON
GO
ALTER DATABASE [GestorTarea] SET QUERY_STORE = ON
GO
ALTER DATABASE [GestorTarea] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 17/10/2023 7:27:40 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tareas]    Script Date: 17/10/2023 7:27:40 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tareas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Descripcion] [varchar](500) NULL,
	[FechaTarea] [datetime] NULL,
	[Completada] [bit] NULL,
	[CategoriaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 17/10/2023 7:27:40 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Correo] [varchar](500) NULL,
	[Clave] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 
GO
INSERT [dbo].[Categorias] ([id], [nombre]) VALUES (1, N'desarrollo')
GO
INSERT [dbo].[Categorias] ([id], [nombre]) VALUES (2, N'prueba')
GO
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Tareas] ON 
GO
INSERT [dbo].[Tareas] ([id], [Nombre], [Descripcion], [FechaTarea], [Completada], [CategoriaId]) VALUES (2, N'prueba 2', N'prueba', CAST(N'2023-10-17T00:00:00.000' AS DateTime), 1, 2)
GO
INSERT [dbo].[Tareas] ([id], [Nombre], [Descripcion], [FechaTarea], [Completada], [CategoriaId]) VALUES (3, N'reunion de daily', N'reunion con el equipo', CAST(N'2023-10-16T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[Tareas] ([id], [Nombre], [Descripcion], [FechaTarea], [Completada], [CategoriaId]) VALUES (7, N'desarrollo de api', N'desarrollar api', CAST(N'2023-10-16T00:00:00.000' AS DateTime), 0, 1)
GO
INSERT [dbo].[Tareas] ([id], [Nombre], [Descripcion], [FechaTarea], [Completada], [CategoriaId]) VALUES (13, N'reunion de levantamiento', N'reunion', CAST(N'2023-10-16T00:00:00.000' AS DateTime), 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Tareas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([id], [Nombre], [Correo], [Clave]) VALUES (1, N'waldo', N'waldo@hotmail.com', N'123')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Tareas]  WITH CHECK ADD FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([id])
GO
ALTER DATABASE [GestorTarea] SET  READ_WRITE 
GO
