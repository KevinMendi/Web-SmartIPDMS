USE [IpdmsDb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lk_OfficeAction](
	[office_action_id] [int] IDENTITY(1,1) NOT NULL,
	[office_action_name1] [nvarchar](200) NOT NULL,
	[office_action_name2] [nvarchar](200) NOT NULL,
	[office_action_name3] [nvarchar](200) NOT NULL,
	[office_action_desc] [nvarchar](200) NULL,
	[office_action_due] [int] NULL,

 CONSTRAINT [PK_lk_OfficeAction] PRIMARY KEY CLUSTERED 
(
	[office_action_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]