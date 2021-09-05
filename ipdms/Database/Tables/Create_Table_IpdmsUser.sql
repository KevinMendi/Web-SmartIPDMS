USE [IpdmsDb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IpdmsUser](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](20) NOT NULL,
	[middle_name] [nvarchar](20) NULL,
	[last_name] [nvarchar](20) NOT NULL,
	[user_role_id] [int] NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[CREATE_USER_ID] [int] NULL,
	[CREATE_USER_DATE] [datetime] NULL,
	[LAST_UPDATE_USER_ID] [int] NULL,
	[LAST_UPDATE_DATE] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]