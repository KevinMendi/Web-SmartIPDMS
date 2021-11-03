USE [IpdmsDb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lk_ApplicationType](
	[application_type_id] [int] IDENTITY(1,1) NOT NULL,
	[application_type_name] [nvarchar](200) NOT NULL,
	[application_type_desc] [nvarchar](200) NULL,

 CONSTRAINT [PK_lk_ApplicationType] PRIMARY KEY CLUSTERED 
(
	[application_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]