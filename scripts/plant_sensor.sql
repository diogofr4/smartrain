USE [SmartRainSensor]
GO

/****** Object:  Table [dbo].[Plant_Sensor]    Script Date: 7/10/2022 5:33:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Plant_Sensor](
	[sensorId] [nvarchar](50) NOT NULL,
	[plantId] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Plant_Sensor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Plant_Sensor]  WITH CHECK ADD  CONSTRAINT [FK_Plant_Sensor_Plant] FOREIGN KEY([plantId])
REFERENCES [dbo].[Plant] ([id])
GO

ALTER TABLE [dbo].[Plant_Sensor] CHECK CONSTRAINT [FK_Plant_Sensor_Plant]
GO

ALTER TABLE [dbo].[Plant_Sensor]  WITH CHECK ADD  CONSTRAINT [FK_Plant_Sensor_Sensor] FOREIGN KEY([sensorId])
REFERENCES [dbo].[Sensor] ([sensorId])
GO

ALTER TABLE [dbo].[Plant_Sensor] CHECK CONSTRAINT [FK_Plant_Sensor_Sensor]
GO


