CREATE TABLE [dbo].[Doctor] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (50) NOT NULL,
    [LastName]   NVARCHAR (50) NOT NULL,
    [Email]      NVARCHAR (50) NOT NULL,
    [PMDC]       NVARCHAR (50) NOT NULL,
    [IsApproved] BIT           CONSTRAINT [DF_Doctor_IsApproved] DEFAULT ((0)) NOT NULL,
    [ShowPhone]  BIT           CONSTRAINT [DF_Doctor_ShowPhone] DEFAULT ((1)) NOT NULL,
    [ShowMobile] BIT           CONSTRAINT [DF_Doctor_ShowMobile] DEFAULT ((1)) NOT NULL,
    [PhoneNo]    NVARCHAR (50) NULL,
    [ValidUpto] DATE NULL, 
    [UserID] INT NOT NULL, 
    [InvoiceNumber] INT NULL, 
    [ProfileImage] NVARCHAR(MAX) NULL, 
    [SignatureImage] NVARCHAR(MAX) NULL, 
    [DisplayName] NVARCHAR(50) NOT NULL, 
    [AllowInvoice] BIT NOT NULL DEFAULT(1) , 
    [AllowChart] BIT NOT NULL DEFAULT(1), 
    [AllowFollowUp] BIT NOT NULL DEFAULT(1), 
    [AllowInventory] BIT NOT NULL DEFAULT(1), 
    [SMSLimit] INT NOT NULL DEFAULT 0, 
    [DoctorType] NVARCHAR(50) NOT NULL DEFAULT '' , 
    [Qualification] NVARCHAR(50) NULL, 
    [AdditionalInfo] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_Doctor_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


GO
