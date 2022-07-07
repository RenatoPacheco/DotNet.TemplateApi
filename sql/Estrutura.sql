USE [Testando]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 07/07/2022 16:44:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Codigo_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[Nome_Usuario] [varchar](255) NULL,
	[Email_Usuario] [varchar](255) NULL,
	[DataCriacao_Usuario] [datetime] NULL,
	[DataAlteracao_Usuario] [datetime] NULL,
	[Status_Usuario] [char](1) NULL,
	[Telefone_Usuario] [varchar](50) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Codigo_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
