CREATE TABLE [dbo].[ClinicTiming]
(
	[ID] INT IDENTITY(1,1) NOT NULL, 
    [Day] NVARCHAR(50) NOT NULL, 
    [StartTime] TIME NOT NULL, 
    [EndTime] TIME NOT NULL, 
    [Session] INT NOT NULL, 
    [IsOpen] BIT NOT NULL DEFAULT 0, 
    [ClinicID] INT NOT NULL, 
	CONSTRAINT [PK_ClinicTiming] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ClinicTiming_Clinic] FOREIGN KEY ([ClinicID]) REFERENCES [Clinic]([ID])
)
