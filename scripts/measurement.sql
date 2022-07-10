USE [SmartRainSensor]
GO

/****** Object:  Table [dbo].[Measurement]    Script Date: 7/10/2022 5:33:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Measurement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sensorId] [nvarchar](50) NOT NULL,
	[readingDateTime] [datetime] NOT NULL,
	[humidity] [int] NOT NULL,
	[rain] [bit] NOT NULL,
	[temperature] [decimal](18, 0) NOT NULL,
	[luminosity] [int] NOT NULL,
 CONSTRAINT [PK_Measurement] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Measurement]  WITH CHECK ADD  CONSTRAINT [FK_Measurement_Sensor] FOREIGN KEY([sensorId])
REFERENCES [dbo].[Sensor] ([sensorId])
GO

ALTER TABLE [dbo].[Measurement] CHECK CONSTRAINT [FK_Measurement_Sensor]
GO


