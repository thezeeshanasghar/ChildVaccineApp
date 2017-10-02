CREATE TABLE [dbo].[BrandInventory]
(
	[ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Count] INT NULL, 
    [BrandID] INT NOT NULL, 
    [DoctorID] INT NOT NULL,
	CONSTRAINT [PK_BrandInventory] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_BrandInventory_Brand] FOREIGN KEY ([BrandID]) REFERENCES [dbo].[Brand] ([ID]),
	CONSTRAINT [FK_BrandInventory_Doctor] FOREIGN KEY ([DoctorID]) REFERENCES [dbo].[Doctor] ([ID])
)
