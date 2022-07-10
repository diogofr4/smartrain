USE [SmartRainSensor]
GO

/****** Object:  Table [dbo].[IrrigationConfig]    Script Date: 7/10/2022 5:32:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IrrigationConfig](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[humidityMin] [int] NULL,
	[temperatureMax] [int] NULL,
	[plantId] [int] NULL,
 CONSTRAINT [PK_IrrigationConfig] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[IrrigationConfig]  WITH CHECK ADD  CONSTRAINT [FK_Plant] FOREIGN KEY([plantId])
REFERENCES [dbo].[Plant] ([id])
GO

ALTER TABLE [dbo].[IrrigationConfig] CHECK CONSTRAINT [FK_Plant]
GO


