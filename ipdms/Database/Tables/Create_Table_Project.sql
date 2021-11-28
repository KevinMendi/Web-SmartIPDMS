USE [IpdmsDb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[project_id] [int] IDENTITY(1,1) NOT NULL,
	[application_type_id] [int] NOT NULL,
	[project_status_id] [int] NOT NULL,
	[ipdms_user_id] [int] NOT NULL,
	[application_no] [varchar](50) NULL,
	[project_title] [varchar](50) NULL,
	[applicant_name] [varchar](100) NULL,
	[project_path] [varchar](200) NULL,
	[CREATE_USER_ID] [int] NULL,
	[CREATE_USER_DATE] [datetime] NULL,
	[LAST_UPDATE_USER_ID] [int] NULL,
	[LAST_UPDATE_USER_DATE] [datetime] NULL,

 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[project_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]