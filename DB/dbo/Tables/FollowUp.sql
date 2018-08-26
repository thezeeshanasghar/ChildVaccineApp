CREATE TABLE [dbo].[FollowUp]
(
	[ID] INT IDENTITY(1,1) NOT NULL, 
    [Disease] NVARCHAR(MAX) NULL, 
    [NextVisitDate] DATE NULL, 
	[CurrentVisitDate] DATE NULL,
    [ChildID] INT NOT NULL, 
    [DoctorID] INT NOT NULL, 
	[Weight]  FLOAT (53) NULL,
    [Height]  FLOAT (53) NULL,
    [OFC]  FLOAT (53) NULL,
    [BloodPressure] FLOAT (53) NULL, 
    [BloodSugar] FLOAT (53) NULL, 
    CONSTRAINT [PK_FollowUp] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_FollowUp_Child] FOREIGN KEY ([ChildID]) REFERENCES [Child]([ID]), 
    CONSTRAINT [FK_FollowUp_Doctor] FOREIGN KEY ([DoctorID]) REFERENCES [Doctor]([ID])
)
