CREATE TABLE [dbo].[FollowUp]
(
	[ID] INT IDENTITY(1,1) NOT NULL, 
    [Disease] NVARCHAR(MAX) NULL, 
    [Date] DATE NULL, 
    [ChildID] INT NOT NULL, 
    [DoctorID] INT NOT NULL, 
    CONSTRAINT [PK_FollowUp] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_FollowUp_Child] FOREIGN KEY ([ChildID]) REFERENCES [Child]([ID]), 
    CONSTRAINT [FK_FollowUp_Doctor] FOREIGN KEY ([DoctorID]) REFERENCES [Doctor]([ID])
)
