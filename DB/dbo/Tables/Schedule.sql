CREATE TABLE [dbo].[Schedule] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [ChildId] INT           NOT NULL,
    [DoseId]  INT           NOT NULL,
    [Date]    DATE          NOT NULL,
    [Weight]  FLOAT (53)    NULL,
    [Height]  FLOAT (53)    NULL,
    [Circle]  FLOAT (53)    NULL,
    [BrandId]   INT NULL,
    [IsDone]  BIT           CONSTRAINT [DF_Schedule_IsDone] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Schedule_Child] FOREIGN KEY ([ChildId]) REFERENCES [dbo].[Child] ([ID]),
    CONSTRAINT [FK_Schedule_Dose] FOREIGN KEY ([DoseId]) REFERENCES [dbo].[Dose] ([ID]),
	CONSTRAINT [FK_Schedule_Brand] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brand]([ID])
);


GO

GO


GO

GO

