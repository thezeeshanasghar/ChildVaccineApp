/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [dbo].[Vaccine] ON 

GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (25, N'BCG', 0, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (29, N'OPV/IPV+HBV+DPT+Hib', 42, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (30, N'Rota Virus GE', 42, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (31, N'Dengue Fever', 3650, NULL)
GO
SET IDENTITY_INSERT [dbo].[Vaccine] OFF
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (55, 25, N'Local')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (59, 29, N'INFANRIX HEXA')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (60, 29, N'HEXAXIM')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (61, 29, N'QUINAVAXEM + OPV')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (62, 29, N'EUPENTA + OPV')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (63, 29, N'PENTAVALENT + OPV')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (64, 30, N'ROTARIX Brand')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (65, 30, N'Rota Teq')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (66, 31, N'Local')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (67, 31, N'Dengvaxia')
GO
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Dose] ON 

GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (68, N'BCG # 1', 25, 0, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (69, N'OPV/IPV+HBV+DPT+Hib # 1', 29, 42, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (70, N'OPV/IPV+HBV+DPT+Hib # 2', 29, 70, NULL, 28, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (71, N'OPV/IPV+HBV+DPT+Hib # 3', 29, 98, NULL, 28, 3)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (72, N'OPV/IPV+HBV+DPT+Hib # 4', 29, 365, NULL, 168, 4)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (73, N'Rota Virus GE # 1', 30, 42, 105, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (74, N'Rota Virus GE # 2', 30, 70, 243, 28, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (75, N'Dengue Fever # 1', 31, 3650, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (76, N'Dengue Fever # 2', 31, 3833, NULL, 168, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (77, N'Dengue Fever # 3', 31, 4015, NULL, 168, 3)
GO
SET IDENTITY_INSERT [dbo].[Dose] OFF
GO




INSERT INTO [User] (MobileNumber, [Password], UserType, CountryCode) VALUES
('3331231231','1234','SUPERADMIN','92');
GO

