CREATE TABLE [dbo].[Message] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [MobileNumber] NVARCHAR (50)  NOT NULL,
	[SMS] NVARCHAR(MAX) NULL,
    [ApiResponse]          NVARCHAR (MAX) NULL,
    [Created] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UserID] INT NULL, 
    CONSTRAINT [PK_SMS] PRIMARY KEY CLUSTERED ([ID] ASC), 
    CONSTRAINT [FK_Message_User] FOREIGN KEY ([UserID]) REFERENCES [User]([ID])
);

