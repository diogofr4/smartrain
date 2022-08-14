USE [SmartRainSensor]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ChatGroups](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[category] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_ChatGroups] PRIMARY KEY CLUSTERED ([id] ASC) 
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GroupParticipants](
	[groupid] [int] IDENTITY(1,1) NOT NULL,
	[participantName] [nvarchar](100) NOT NULL,
	FOREIGN KEY (groupid) REFERENCES ChatGroups(id)
) 
GO

CREATE TABLE [dbo].[GroupMessages](
	[groupid] [int] IDENTITY(1,1) NOT NULL,
	[participantName] [nvarchar](100) NOT NULL,
	[message] [nvarchar](255) NOT NULL,
	[messageDate] [datetime] NOT NULL,
	FOREIGN KEY (groupid) REFERENCES ChatGroups(id)
) 
GO