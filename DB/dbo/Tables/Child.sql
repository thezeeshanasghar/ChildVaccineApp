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
	[PreferredShedule] [nvarchar](50) NULL,
 CONSTRAINT [PK_Child] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]