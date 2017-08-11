CREATE TABLE [dbo].[Dose] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NOT NULL,
    [VaccineID] INT           NOT NULL,
	[GapInDays] INT NULL, 
    [DoseOrder] INT NULL, 
    CONSTRAINT [PK_Dose] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Dose_Vaccine] FOREIGN KEY ([VaccineID]) REFERENCES [dbo].[Vaccine] ([ID])
);


GO

GO

