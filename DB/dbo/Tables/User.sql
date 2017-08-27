CREATE TABLE [dbo].[User] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [MobileNumber] NVARCHAR (50) NOT NULL,
    [Password]     NVARCHAR (50) NOT NULL,
    [UserType]     NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);


