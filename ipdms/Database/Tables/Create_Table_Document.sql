USE [IpdmsDb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[document_id] [int] IDENTITY(1,1) NOT NULL,
	[office_action_id] [int] NOT NULL,
	[project_id] [int] NOT NULL,
	[pdf_name] [nvarchar](100) NOT NULL,
	[mail_date] [datetime] NULL,
	[filling_date] [datetime] NULL,
	[CREATE_USER_ID] [int] NULL,
	[CREATE_USER_DATE] [datetime] NULL,
	[LAST_UPDATE_USER_ID] [int] NULL,
	[LAST_UPDATE_USER_DATE] [datetime] NULL,

 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[document_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]