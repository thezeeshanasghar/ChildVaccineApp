CREATE TABLE [dbo].[Brand]
(
	 [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [VaccineID] INT NOT NULL, 
    [Name] NVARCHAR(50) NULL,
	CONSTRAINT [PK_VaccineBrand] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_VaccineBrand_Vaccine] FOREIGN KEY ([VaccineID]) REFERENCES [dbo].[Vaccine] ([ID])
)
