CREATE TABLE [dbo].[Doctor] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (50) NOT NULL,
    [LastName]   NVARCHAR (50) NOT NULL,
    [Email]      NVARCHAR (50) NOT NULL,
	[CountryCode] NVARCHAR (50) NULL,
    [MobileNo]   NVARCHAR (50) NOT NULL,
    [Password]   NVARCHAR (50) NOT NULL,
    [PMDC]       NVARCHAR (50) NOT NULL,
    [IsApproved] BIT           CONSTRAINT [DF_Doctor_IsApproved] DEFAULT ((0)) NOT NULL,
    [ShowPhone]  BIT           CONSTRAINT [DF_Doctor_ShowPhone] DEFAULT ((1)) NOT NULL,
    [ShowMobile] BIT           CONSTRAINT [DF_Doctor_ShowMobile] DEFAULT ((1)) NOT NULL,
    [PhoneNo]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
