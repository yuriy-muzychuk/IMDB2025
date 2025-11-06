USE [IMDB2025]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [binary](64) NOT NULL,
	[Salt] [uniqueidentifier] NOT NULL,
	[RowInsertTime] [datetime] NOT NULL,
	[RowUpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_RowInsertTime]  DEFAULT (getutcdate()) FOR [RowInsertTime]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_RowUpdateTime]  DEFAULT (getutcdate()) FOR [RowUpdateTime]
GO


CREATE TABLE [dbo].[Privileges](
	[PrivilegeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RowInsertTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Privileges] PRIMARY KEY CLUSTERED 
(
	[PrivilegeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Privileges] ADD  CONSTRAINT [DF_Privileges_RowInsertTime]  DEFAULT (getutcdate()) FOR [RowInsertTime]
GO




CREATE TABLE [dbo].[UserPrivileges](
	[UserID] [int] NOT NULL,
	[PrivilegeID] [int] NOT NULL,
	[RowInsertTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserPrivileges] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[PrivilegeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserPrivileges] ADD  CONSTRAINT [DF_UserInRights_RowInsertTime]  DEFAULT (getutcdate()) FOR [RowInsertTime]
GO

ALTER TABLE [dbo].[UserPrivileges]  WITH CHECK ADD  CONSTRAINT [FK_UserPrivileges_Privileges] FOREIGN KEY([PrivilegeID])
REFERENCES [dbo].[Privileges] ([PrivilegeID])
GO

ALTER TABLE [dbo].[UserPrivileges] CHECK CONSTRAINT [FK_UserPrivileges_Privileges]
GO

ALTER TABLE [dbo].[UserPrivileges]  WITH CHECK ADD  CONSTRAINT [FK_UserPrivileges_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[UserPrivileges] CHECK CONSTRAINT [FK_UserPrivileges_Users]
GO

INSERT INTO [dbo].[Privileges] ([Name]) VALUES ('Admin'), ('User'), ('Moderator')
GO


