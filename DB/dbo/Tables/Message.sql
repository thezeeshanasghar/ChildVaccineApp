CREATE TABLE [dbo].[Message] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [MobileNumber] NVARCHAR (50)  NOT NULL,
    [SMS]          NVARCHAR (MAX) NOT NULL,
    [Status]       NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_SMS] PRIMARY KEY CLUSTERED ([ID] ASC)
);

