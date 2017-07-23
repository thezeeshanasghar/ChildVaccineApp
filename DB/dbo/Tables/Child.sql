CREATE TABLE [dbo].[Child](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[FatherName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[DOB] [date] NULL,
	[MobileNumber] [nvarchar](50) NULL,
	[StreetAddress] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[PreferredDayOfReminder] [int] NULL,
	[City] [nvarchar](50) NULL,
	[PreferredSchedule] [nvarchar](50) NULL,
 [Password] NVARCHAR(50) NULL, 
    [PreferredDayOfWeek] NVARCHAR(50) NULL, 
    [IsEPIDone] BIT NULL, 
    [IsVerified] BIT NULL, 
    [ClinicID] INT NOT NULL, 
    CONSTRAINT [PK_Child] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [FK_Child_Clinic] FOREIGN KEY (ClinicID) REFERENCES [Clinic](ID)
) ON [PRIMARY]