CREATE TABLE [dbo].[BrandAmount]
(
	[ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Amount] INT NOT NULL DEFAULT 0, 
    [BrandID] INT NOT NULL, 
    [DoctorID] INT NOT NULL,
	CONSTRAINT [PK_BrandAmount] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_BrandAmount_Brand] FOREIGN KEY ([BrandID]) REFERENCES [dbo].[Brand] ([ID]),
	CONSTRAINT [FK_BrandAmount_Doctor] FOREIGN KEY ([DoctorID]) REFERENCES [dbo].[Doctor] ([ID])
)
