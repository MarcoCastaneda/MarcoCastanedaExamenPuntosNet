CREATE DATABASE [MCastañedaPuntosNet]
GO
USE [MCastañedaPuntosNet]
GO
/****** Object:  StoredProcedure [dbo].[PasswordUpdate]    Script Date: 07/07/2022 04:08:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PasswordUpdate](
@IdUsuario INT,
@Password VARCHAR(50))

AS

UPDATE Usuario
SET
Password = @Password

WHERE IdUsuario = @IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioAdd]    Script Date: 07/07/2022 04:08:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioAdd](
@Nombre VARCHAR(50),
@UserName VARCHAR(50),
@ApellidoPaterno VARCHAR(50), 
@ApellidoMaterno VARCHAR(50), 
@Email VARCHAR(50),
@Password VARCHAR(50))

AS

INSERT INTO Usuario(
Nombre,
UserName,
ApellidoPaterno, 
ApellidoMaterno, 
Email,
Password
) 

VALUES 
(@Nombre,
@UserName,
@ApellidoPaterno, 
@ApellidoMaterno, 
@Email,
@Password
 )
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDelete]    Script Date: 07/07/2022 04:08:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDelete] 
 @IdUsuario INT

 AS

 DELETE FROM Usuario

 WHERE 
 IdUsuario = @IdUsuario

GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAll]    Script Date: 07/07/2022 04:08:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UsuarioGetAll]

AS

SELECT
IdUsuario,
UserName,
Nombre,
ApellidoPaterno, 
ApellidoMaterno, 
Email,
Password 

FROM Usuario


GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetByEmail]    Script Date: 07/07/2022 04:08:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UsuarioGetByEmail] 
@Email VARCHAR(50)
AS
SELECT 
		Email, 
		Password,
		IdUsuario
FROM Usuario
WHERE Email = @Email
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetById]    Script Date: 07/07/2022 04:08:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetById]
@IdUsuario INT
AS

SELECT
IdUsuario,
UserName,
Nombre,
ApellidoPaterno, 
ApellidoMaterno, 
Email,
Password 

FROM Usuario

WHERE 
 IdUsuario = @IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioUpdate]    Script Date: 07/07/2022 04:08:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioUpdate](
@IdUsuario INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50), 
@ApellidoMaterno VARCHAR(50), 
@Email VARCHAR(50),
@Password VARCHAR(50))

AS

UPDATE Usuario
SET
Nombre = @Nombre,
ApellidoPaterno = @Apellidopaterno, 
ApellidoMaterno = @ApellidoMaterno, 
Email = @Email,
Password = @Password

WHERE IdUsuario = @IdUsuario
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 07/07/2022 04:08:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[ApellidoPaterno] [varchar](50) NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [UserName], [ApellidoPaterno], [ApellidoMaterno], [Email], [Password]) VALUES (1, N'Luis', N'Luisillo', N'Perez', N'sanchez', N'Luisill@gmail.com', N'Master')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [UserName], [ApellidoPaterno], [ApellidoMaterno], [Email], [Password]) VALUES (44, N'Leonardo', N'Leo', N'Esc', N'Bravo', N'leoebravo@gmail.com', N'12345')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [UserName], [ApellidoPaterno], [ApellidoMaterno], [Email], [Password]) VALUES (58, N'Isaac', N'isac', N'Espinoza', N'Paz', N'isaacjeo96@gmail.com', N'12345')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [UserName], [ApellidoPaterno], [ApellidoMaterno], [Email], [Password]) VALUES (77, N'a', N'a', N'a', N'a', N'marcocb1998@gmail.com', N'12345')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
