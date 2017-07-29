CREATE TABLE [dbo].[Vaccine] (
    [ID]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (50) NOT NULL,
    [MinAge] INT           NOT NULL,
    [MaxAge] INT           NULL,
    CONSTRAINT [PK_Vaccine] PRIMARY KEY CLUSTERED ([ID] ASC)
);

