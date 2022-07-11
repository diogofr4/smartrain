USE [SmartRainSensor]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Task](
[id] [int] IDENTITY(1,1) NOT NULL,
[title] varchar(255) NOT NULL, 
[description] varchar(255) NOT NULL, 
[responsibleUser] varchar(255) NOT NULL, 
[startDateTime] [datetime] NOT NULL,
[endDateTime] [datetime] NOT NULL,
CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED (id ASC) 
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
