CREATE TABLE [dbo].[VaccineInventory]
(
	[ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Count] INT NULL, 
    [VaccineID] INT NOT NULL, 
    [DoctorID] INT NOT NULL,
	CONSTRAINT [PK_VaccineInventory] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_VaccineInventory_Vaccine] FOREIGN KEY ([VaccineID]) REFERENCES [dbo].[Vaccine] ([ID]),
	CONSTRAINT [FK_VaccineInventory_Doctor] FOREIGN KEY ([DoctorID]) REFERENCES [dbo].[Doctor] ([ID])
)
