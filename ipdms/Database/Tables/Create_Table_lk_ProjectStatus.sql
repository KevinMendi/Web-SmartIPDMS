USE [IpdmsDb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lk_ProjectStatus](
	[project_status_id] [int] IDENTITY(1,1) NOT NULL,
	[project_status_name] [nvarchar](200) NOT NULL,
	[project_status_desc] [nvarchar](200) NULL,

 CONSTRAINT [PK_lk_ProjectStatus] PRIMARY KEY CLUSTERED 
(
	[project_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]