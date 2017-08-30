CREATE TABLE [dbo].[Child] (
    [ID]                     INT           IDENTITY (1, 1) NOT NULL,
    [Name]                   NVARCHAR (50) NOT NULL,
    [FatherName]             NVARCHAR (50) NOT NULL,
    [Email]                  NVARCHAR (50) NOT NULL,
    [DOB]                    DATE          NOT NULL,
    [Gender]                 NVARCHAR (50) NOT NULL,
    [PreferredDayOfReminder] INT           NULL,
    [City]                   NVARCHAR (50) NULL,
    [PreferredSchedule]      NVARCHAR (50) NULL,
    [PreferredDayOfWeek]     NVARCHAR (56) NULL,
    [IsEPIDone]              BIT           NULL,
    [IsVerified]             BIT           NULL,
    [ClinicID]               INT           NOT NULL,
    [UserID] INT NOT NULL, 
    CONSTRAINT [PK_Child] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Child_Clinic] FOREIGN KEY ([ClinicID]) REFERENCES [dbo].[Clinic] ([ID]),
	CONSTRAINT [FK_Child_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

