CREATE TABLE [dbo].[DoctorSchedule] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [DoseID]    INT NOT NULL,
    [DoctorID]  INT NOT NULL,
    [GapInDays] INT NOT NULL,
    CONSTRAINT [PK_DoctorSchedule] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DoctorSchedule_Doctor] FOREIGN KEY ([DoctorID]) REFERENCES [dbo].[Doctor] ([ID]),
    CONSTRAINT [FK_DoctorSchedule_Dose] FOREIGN KEY ([DoseID]) REFERENCES [dbo].[Dose] ([ID]) ON DELETE CASCADE
);



