CREATE TABLE [dbo].[ChildVaccine](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChildId] [int] NOT NULL,
	[DoseId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
	[Circle] [float] NULL,
	[Brand] [nvarchar](50) NULL,
	[IsDone] [bit] NOT NULL,
 CONSTRAINT [PK_ChildVaccine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChildVaccine]  WITH CHECK ADD  CONSTRAINT [FK_ChildVaccine_Child] FOREIGN KEY([ChildId])
REFERENCES [dbo].[Child] ([ID])
GO

ALTER TABLE [dbo].[ChildVaccine] CHECK CONSTRAINT [FK_ChildVaccine_Child]
GO
ALTER TABLE [dbo].[ChildVaccine]  WITH CHECK ADD  CONSTRAINT [FK_ChildVaccine_Dose] FOREIGN KEY([DoseId])
REFERENCES [dbo].[Dose] ([ID])
GO

ALTER TABLE [dbo].[ChildVaccine] CHECK CONSTRAINT [FK_ChildVaccine_Dose]