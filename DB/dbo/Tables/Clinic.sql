CREATE TABLE [dbo].[Clinic] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [ConsultationFee] INT NULL, 
    [Lat]         FLOAT (53)    NOT NULL,
    [Long]        FLOAT (53)    NOT NULL,
    [DoctorID]    INT           NOT NULL,
    [PhoneNumber] NVARCHAR (50) NULL,
    [IsOnline]    BIT           CONSTRAINT [DF_Clinic_IsOnline] DEFAULT ((0)) NOT NULL,
    [Address] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_Clinic] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Clinic_Doctor] FOREIGN KEY ([DoctorID]) REFERENCES [dbo].[Doctor] ([ID])
);


GO

GO

