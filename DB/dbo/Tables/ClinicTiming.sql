CREATE TABLE [dbo].[ClinicTiming]
(
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Day]		  NVARCHAR (10)	NOT NULL,
	[StartTime]   TIME (7)      NOT NULL,
    [EndTime]     TIME (7)      NOT NULL, 
    [Session]     INT NOT NULL, 
	[ClinicID]    INT NOT NULL,
	[IsOpen]      BIT NOT NULL Default(0),
    CONSTRAINT [PK_ClinicTiming] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_ClinicTiming_Clinic] FOREIGN KEY ([ClinicID]) REFERENCES [dbo].[Clinic] ([ID])
);
GO
