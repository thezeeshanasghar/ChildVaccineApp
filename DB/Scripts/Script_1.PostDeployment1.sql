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
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (1, N'BCG', 0, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (2, N'OPV', 0, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (7, N'Rota Virus GE', 42, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (9, N'Chicken Pox', 365, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (10, N'MMR', 365, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (11, N'MenB Vaccine', 56, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (12, N'MenACWY vaccine', 273, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (14, N'Yellow Fever', 273, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (15, N'Typhoid', 726, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (16, N'Flu Vaccine', 168, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (19, N'OPV/IPV+HBV+DPT+Hib', 42, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (20, N'Pneumococcal', 42, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (21, N'Hepatitis B', 0, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (22, N'HPV', 3285, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (23, N'Dengue Fever', 3650, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (24, N'PPSV', 726, NULL)
GO
SET IDENTITY_INSERT [dbo].[Vaccine] OFF
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (1, 1, N'BCG Brand')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (7, 7, N'ROTARIX Brand')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (11, 11, N'Men B ')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (13, 10, N'TRIMOVAX')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (14, 10, N'PRIORIX')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (17, 14, N'STAMARIL')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (23, 20, N'Local')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (24, 7, N'Rota Teq')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (25, 9, N'VARIVAC')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (26, 9, N'VARILRIX')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (27, 9, N'VAXAPOX')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (28, 12, N'MENECTRA')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (29, 15, N'TYPHIRIX')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (30, 15, N'TYPBAR')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (32, 19, N'INFANRIX HEXA')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (33, 19, N'HEXAXIM')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (34, 19, N'QUINAVAXEM + OPV')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (35, 19, N'EUPENTA + OPV')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (36, 19, N'PENTAVALENT + OPV')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (37, 16, N'INFLUVAC')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (38, 16, N'FLUARIX')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (39, 16, N'VAXIGRIP')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (40, 20, N'SYNFLORIX')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (41, 20, N'PREVENAR')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (42, 21, N'Local')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (43, 21, N'AMVAX B JR')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (44, 21, N'ENGERIX B JR')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (45, 22, N'Local')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (46, 23, N'Local')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (47, 22, N'CERVARIX')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (48, 23, N'Dengvaxia')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (49, 10, N'MMR Imp ')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (50, 10, N'PRIORIX TETRA')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (51, 9, N'PRIORIX TETRA')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (52, 2, N'OPV')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (53, 24, N'Local')
GO
INSERT [dbo].[Brand] ([ID], [VaccineID], [Name]) VALUES (54, 24, N'PNEUMOVAX')
GO
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Dose] ON 

GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (1, N'BCG     ', 1, 1, 0, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (3, N'OPV # 1', 2, 1, 0, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (28, N'MenB Vaccine # 1', 11, 1, 56, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (29, N'MenB Vaccine # 2', 11, 2, 112, NULL, 56)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (30, N'Typhoid # 1', 15, 1, 726, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (31, N'Typhoid # 2', 15, 2, 1446, NULL, 726)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (32, N'Typhoid # 3', 15, 3, 2190, NULL, 726)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (33, N'Typhoid # 4', 15, 4, 2920, NULL, 726)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (34, N'OPV/IPV+HBV+DPT+Hib # 1', 19, 1, 42, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (35, N'OPV/IPV+HBV+DPT+Hib # 2', 19, 2, 70, NULL, 28)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (36, N'OPV/IPV+HBV+DPT+Hib # 3', 19, 3, 98, NULL, 28)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (41, N'Pneumococcal # 1', 20, 1, 42, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (42, N'Pneumococcal # 2', 20, 2, 70, NULL, 28)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (43, N'Pneumococcal # 3', 20, 3, 98, NULL, 28)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (44, N'MMR # 1', 10, 1, 365, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (45, N'MMR # 2', 10, 2, 546, NULL, 168)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (46, N'HPV # 1', 22, 1, 3285, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (47, N'HPV # 3', 22, 2, 3315, NULL, 28)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (48, N'Dengue Fever # 1', 23, 1, 3650, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (49, N'Dengue Fever # 2', 23, 2, 3833, NULL, 168)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (50, N'Dengue Fever # 3', 23, 3, 4015, NULL, 168)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (53, N'OPV # 2', 2, 2, 42, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (54, N'OPV # 3', 2, 3, 70, NULL, 28)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (55, N'OPV # 4', 2, 4, 365, NULL, 168)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (56, N'Hepatitis B # 1', 21, 1, 0, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (57, N'OPV/IPV+HBV+DPT+Hib # 4', 19, 4, 365, NULL, 168)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (58, N'Yellow Fever # 1', 14, 1, 273, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (59, N'MenACWY vaccine # 1', 12, 1, 273, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (60, N'MenACWY vaccine # 2', 12, 2, 726, NULL, 168)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (61, N'Flu Vaccine # 1', 16, 1, 168, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (62, N'Chicken Pox # 1', 9, 1, 365, NULL, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (63, N'Chicken Pox # 2', 9, 2, 456, NULL, 84)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (64, N'Rota Virus GE # 1', 7, 1, 42, 105, NULL)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (65, N'Rota Virus GE # 2', 7, 2, 70, 243, 28)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (66, N'Pneumococcal # 4', 20, 4, 365, NULL, 168)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [DoseOrder], [MinAge], [MaxAge], [MinGap]) VALUES (67, N'PPSV # 1', 24, 1, 726, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Dose] OFF
GO



INSERT INTO [User] (MobileNumber, [Password], UserType, CountryCode) VALUES
('3331231231','1234','SUPERADMIN','92');
GO

