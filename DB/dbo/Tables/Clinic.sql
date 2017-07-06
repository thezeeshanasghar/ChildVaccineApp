CREATE TABLE [dbo].[Clinic](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[OffDays] [nvarchar](50) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Lat] [float] NOT NULL,
	[Long] [float] NOT NULL,
	[DoctorID] [int] NOT NULL,
 CONSTRAINT [PK_Clinic] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clinic]  WITH CHECK ADD  CONSTRAINT [FK_Clinic_Doctor] FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Doctor] ([ID])
GO

ALTER TABLE [dbo].[Clinic] CHECK CONSTRAINT [FK_Clinic_Doctor]