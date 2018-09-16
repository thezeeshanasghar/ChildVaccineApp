SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (1, N'3331231231', N'1234', N'SUPERADMIN', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (2, N'3335196658', N'1234', N'DOCTOR', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (4, N'3465430413', N'0348', N'PARENT', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (5, N'3327444653', N'8927', N'PARENT', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (6, N'3213000580', N'6467', N'PARENT', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (7, N'3006604519', N'5247', N'PARENT', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (8, N'3310097772', N'7129', N'PARENT', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (9, N'3455112621', N'5080', N'PARENT', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (10, N'3066612853', N'7617', N'PARENT', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (11, N'3225156051', N'4382', N'PARENT', N'92')
GO
INSERT [dbo].[User] ([ID], [MobileNumber], [Password], [UserType], [CountryCode]) VALUES (12, N'3238568526', N'9055', N'PARENT', N'92')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Doctor] ON 

GO
INSERT [dbo].[Doctor] ([ID], [FirstName], [LastName], [Email], [PMDC], [IsApproved], [ShowPhone], [ShowMobile], [PhoneNo], [ValidUpto], [UserID], [InvoiceNumber], [ProfileImage], [SignatureImage], [DisplayName]) VALUES (1, N'Salman', N'bajwa', N'salmanbajwa777@gmail.com', N'12345-A', 1, 1, 1, N'03310097772', CAST(N'2018-04-07' AS Date), 2, 1, NULL, NULL, N'Dr Salman Ahmad Bajwa')
GO
SET IDENTITY_INSERT [dbo].[Doctor] OFF
GO
SET IDENTITY_INSERT [dbo].[Clinic] ON 

GO
INSERT [dbo].[Clinic] ([ID], [Name], [ConsultationFee], [OffDays], [StartTime], [EndTime], [Lat], [Long], [DoctorID], [PhoneNumber], [IsOnline], [Address]) VALUES (1, N'Noor Gyne Centre', 500, N'Sunday', CAST(N'17:00:00' AS Time), CAST(N'18:30:00' AS Time), 33.5918141503898, 73.131420960784908, 1, N'03310097772', 0, NULL)
GO
INSERT [dbo].[Clinic] ([ID], [Name], [ConsultationFee], [OffDays], [StartTime], [EndTime], [Lat], [Long], [DoctorID], [PhoneNumber], [IsOnline], [Address]) VALUES (2, N'South East Hospital, PWD', 1000, N'', CAST(N'19:00:00' AS Time), CAST(N'22:00:00' AS Time), 33.565749561943491, 73.1304660943756, 1, N'03310097772', 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Clinic] OFF
GO
SET IDENTITY_INSERT [dbo].[Child] ON 

GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (2, N'Haya Zeeshan', N'Zeeshan Asghar', N'', CAST(N'2017-07-16' AS Date), N'Girl', 0, N'Islamabad', N'CUSTOM', N'Any', 0, 0, 1, 4)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (3, N'Muhammad', N'Salman', N'abc@gmail.com', CAST(N'2012-12-19' AS Date), N'Boy', 0, N'Islamabad', N'CUSTOM', N'Any', 0, 0, 1, 5)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (4, N'Heyder Ali', N'Hasrat Ali', N'hasrat704@gmail.com', CAST(N'2015-05-19' AS Date), N'Boy', 0, N'Lahore', N'CUSTOM', N'Any', 0, 0, 2, 6)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (5, N'Muhammad Abdullah', N'Rizwan Mehboob', N'', CAST(N'2001-01-26' AS Date), N'Boy', 0, N'Islamabad', N'CUSTOM', N'Friday', 0, 0, 2, 7)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (6, N'Test 1', N'Test11', N'', CAST(N'2017-12-01' AS Date), N'Girl', 1, N'Karachi', N'CUSTOM', N'Any', 0, 0, 1, 8)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (7, N'Test 2', N'Test22', N'', CAST(N'2017-01-01' AS Date), N'Girl', 0, N'Islamabad', N'CUSTOM', N'Any', 1, 0, 1, 2)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (8, N'Muhammad Sarfraz', N'Malik Usman', N'', CAST(N'2017-03-15' AS Date), N'Boy', 1, N'Rawalpindi', N'CUSTOM', N'Friday', 0, 0, 1, 9)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (9, N'Daud', N'Kashif Rasool', N'', CAST(N'2012-10-01' AS Date), N'Boy', 0, N'Abbottabad', N'CUSTOM', N'Any', 1, 0, 1, 10)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (10, N'Muhammad Rohaan Durrani', N'Mamoon Durrani', N'', CAST(N'2016-10-15' AS Date), N'Boy', 1, N'Islamabad', N'CUSTOM', N'Any', 0, 0, 2, 11)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (11, N'Test 2', N'Test22', N'', CAST(N'2017-01-01' AS Date), N'Girl', 0, N'Islamabad', N'CUSTOM', N'', 0, 0, 1, 2)
GO
INSERT [dbo].[Child] ([ID], [Name], [FatherName], [Email], [DOB], [Gender], [PreferredDayOfReminder], [City], [PreferredSchedule], [PreferredDayOfWeek], [IsEPIDone], [IsVerified], [ClinicID], [UserID]) VALUES (12, N'Aroosh Bilal', N'Bilal Razak', N'', CAST(N'2017-07-05' AS Date), N'Girl', 0, N'Islamabad', N'CUSTOM', N'Any', 0, 0, 2, 12)
GO
SET IDENTITY_INSERT [dbo].[Child] OFF
GO
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
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (12, N'MenACWY vaccine', 274, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (14, N'Yellow Fever', 274, NULL)
GO
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (15, N'Typhoid', 730, NULL)
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
INSERT [dbo].[Vaccine] ([ID], [Name], [MinAge], [MaxAge]) VALUES (24, N'PPSV', 730, NULL)
GO
SET IDENTITY_INSERT [dbo].[Vaccine] OFF
GO
SET IDENTITY_INSERT [dbo].[Dose] ON 

GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (1, N'BCG     ', 1, 0, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (3, N'OPV # 1', 2, 0, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (28, N'MenB Vaccine # 1', 11, 56, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (29, N'MenB Vaccine # 2', 11, 112, NULL, 56, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (30, N'Typhoid # 1', 15, 730, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (31, N'Typhoid # 2', 15, 1460, NULL, 730, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (32, N'Typhoid # 3', 15, 2190, NULL, 730, 3)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (33, N'Typhoid # 4', 15, 2920, NULL, 730, 4)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (34, N'OPV/IPV+HBV+DPT+Hib # 1', 19, 42, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (35, N'OPV/IPV+HBV+DPT+Hib # 2', 19, 70, NULL, 28, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (36, N'OPV/IPV+HBV+DPT+Hib # 3', 19, 98, NULL, 28, 3)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (41, N'Pneumococcal # 1', 20, 42, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (42, N'Pneumococcal # 2', 20, 70, NULL, 28, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (43, N'Pneumococcal # 3', 20, 98, NULL, 28, 3)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (44, N'MMR # 1', 10, 365, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (45, N'MMR # 2', 10, 547, NULL, 168, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (46, N'HPV # 1', 22, 3285, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (47, N'HPV # 2', 22, 3315, NULL, 28, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (48, N'Dengue Fever # 1', 23, 3650, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (49, N'Dengue Fever # 2', 23, 3833, NULL, 168, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (50, N'Dengue Fever # 3', 23, 4015, NULL, 168, 3)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (56, N'Hepatitis B # 1', 21, 0, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (57, N'OPV/IPV+HBV+DPT+Hib # 4', 19, 365, NULL, 168, 4)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (58, N'Yellow Fever # 1', 14, 274, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (59, N'MenACWY vaccine # 1', 12, 274, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (60, N'MenACWY vaccine # 2', 12, 456, NULL, 168, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (61, N'Flu Vaccine # 1', 16, 168, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (62, N'Chicken Pox # 1', 9, 365, NULL, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (63, N'Chicken Pox # 2', 9, 456, NULL, 84, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (64, N'Rota Virus GE # 1', 7, 42, 105, NULL, 1)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (65, N'Rota Virus GE # 2', 7, 70, 243, 28, 2)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (66, N'Pneumococcal # 4', 20, 365, NULL, 168, 4)
GO
INSERT [dbo].[Dose] ([ID], [Name], [VaccineID], [MinAge], [MaxAge], [MinGap], [DoseOrder]) VALUES (67, N'PPSV # 1', 24, 730, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Dose] OFF
GO
SET IDENTITY_INSERT [dbo].[DoctorSchedule] ON 

GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (1, 1, 1, 0)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (2, 56, 1, 0)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (3, 3, 1, 0)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (4, 34, 1, 42)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (5, 41, 1, 42)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (6, 64, 1, 42)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (7, 28, 1, 56)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (8, 35, 1, 70)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (9, 42, 1, 98)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (10, 65, 1, 98)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (11, 36, 1, 98)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (12, 43, 1, 168)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (13, 29, 1, 112)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (14, 61, 1, 168)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (15, 59, 1, 274)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (16, 58, 1, 1825)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (17, 62, 1, 365)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (18, 44, 1, 365)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (19, 57, 1, 547)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (20, 66, 1, 547)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (21, 63, 1, 456)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (22, 60, 1, 730)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (23, 45, 1, 547)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (24, 67, 1, 1825)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (25, 30, 1, 730)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (26, 31, 1, 1460)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (27, 32, 1, 2190)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (28, 33, 1, 2920)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (29, 46, 1, 3285)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (30, 47, 1, 3315)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (31, 48, 1, 3650)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (32, 49, 1, 3833)
GO
INSERT [dbo].[DoctorSchedule] ([ID], [DoseID], [DoctorID], [GapInDays]) VALUES (33, 50, 1, 4015)
GO
SET IDENTITY_INSERT [dbo].[DoctorSchedule] OFF
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
SET IDENTITY_INSERT [dbo].[BrandAmount] ON 

GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (1, 0, 1, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (2, 0, 52, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (3, 0, 7, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (4, 0, 24, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (5, 0, 25, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (6, 0, 26, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (7, 0, 27, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (8, 0, 51, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (9, 0, 13, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (10, 0, 14, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (11, 0, 49, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (12, 0, 50, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (13, 0, 11, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (14, 0, 28, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (15, 0, 17, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (16, 0, 29, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (17, 0, 30, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (18, 0, 37, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (19, 0, 38, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (20, 0, 39, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (21, 0, 32, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (22, 0, 33, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (23, 0, 34, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (24, 0, 35, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (25, 0, 36, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (26, 0, 23, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (27, 0, 40, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (28, 0, 41, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (29, 0, 42, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (30, 0, 43, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (31, 0, 44, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (32, 0, 45, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (33, 0, 47, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (34, 0, 46, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (35, 0, 48, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (36, 0, 53, 1)
GO
INSERT [dbo].[BrandAmount] ([ID], [Amount], [BrandID], [DoctorID]) VALUES (37, 0, 54, 1)
GO
SET IDENTITY_INSERT [dbo].[BrandAmount] OFF
GO
SET IDENTITY_INSERT [dbo].[BrandInventory] ON 

GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (1, -1, 1, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (2, -1, 52, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (3, 0, 7, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (4, 0, 24, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (5, 0, 25, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (6, 0, 26, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (7, 0, 27, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (8, 0, 51, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (9, 0, 13, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (10, 0, 14, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (11, 0, 49, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (12, 0, 50, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (13, 0, 11, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (14, 0, 28, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (15, 0, 17, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (16, 0, 29, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (17, 0, 30, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (18, 0, 37, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (19, 0, 38, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (20, 0, 39, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (21, 0, 32, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (22, 0, 33, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (23, 0, 34, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (24, 0, 35, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (25, 0, 36, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (26, 0, 23, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (27, 0, 40, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (28, 0, 41, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (29, 0, 42, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (30, 0, 43, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (31, -1, 44, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (32, 0, 45, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (33, 0, 47, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (34, 0, 46, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (35, 0, 48, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (36, 0, 53, 1)
GO
INSERT [dbo].[BrandInventory] ([ID], [Count], [BrandID], [DoctorID]) VALUES (37, 0, 54, 1)
GO
SET IDENTITY_INSERT [dbo].[BrandInventory] OFF
GO
SET IDENTITY_INSERT [dbo].[FollowUp] ON 

GO
INSERT [dbo].[FollowUp] ([ID], [Disease], [NextVisitDate], [CurrentVisitDate], [ChildID], [DoctorID]) VALUES (1, N'Tonsillitis ', CAST(N'2018-01-16' AS Date), CAST(N'2018-01-09' AS Date), 3, 1)
GO
INSERT [dbo].[FollowUp] ([ID], [Disease], [NextVisitDate], [CurrentVisitDate], [ChildID], [DoctorID]) VALUES (2, N'abc', CAST(N'2018-01-12' AS Date), CAST(N'2018-01-11' AS Date), 2, 1)
GO
INSERT [dbo].[FollowUp] ([ID], [Disease], [NextVisitDate], [CurrentVisitDate], [ChildID], [DoctorID]) VALUES (3, N'reivew child on second visit', CAST(N'2018-01-17' AS Date), CAST(N'2018-01-16' AS Date), 2, 1)
GO
SET IDENTITY_INSERT [dbo].[FollowUp] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 

GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (1, 2, 1, CAST(N'2017-07-16' AS Date), 10, 10, 10, 1, 1, 0, CAST(N'2018-01-12' AS Date))
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (2, 2, 56, CAST(N'2017-07-16' AS Date), 10, 10, 10, 44, 1, 0, CAST(N'2018-01-12' AS Date))
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (3, 2, 3, CAST(N'2017-07-16' AS Date), 10, 10, 10, 52, 1, 0, CAST(N'2018-01-12' AS Date))
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (4, 2, 34, CAST(N'2018-01-11' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (5, 2, 41, CAST(N'2018-01-11' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (6, 2, 64, CAST(N'2018-01-11' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (7, 2, 28, CAST(N'2017-09-10' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (8, 2, 35, CAST(N'2018-02-08' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (9, 2, 42, CAST(N'2018-03-08' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (10, 2, 65, CAST(N'2018-03-08' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (11, 2, 36, CAST(N'2018-03-08' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (12, 2, 43, CAST(N'2018-05-17' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (13, 2, 29, CAST(N'2017-11-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (14, 2, 61, CAST(N'2017-12-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (15, 2, 59, CAST(N'2018-04-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (16, 2, 58, CAST(N'2022-07-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (17, 2, 62, CAST(N'2018-07-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (18, 2, 44, CAST(N'2018-07-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (19, 2, 57, CAST(N'2019-05-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (20, 2, 66, CAST(N'2019-05-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (21, 2, 63, CAST(N'2018-10-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (22, 2, 60, CAST(N'2019-07-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (23, 2, 45, CAST(N'2019-01-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (24, 2, 67, CAST(N'2022-07-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (25, 2, 30, CAST(N'2019-07-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (26, 2, 31, CAST(N'2021-07-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (27, 2, 32, CAST(N'2023-07-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (28, 2, 33, CAST(N'2025-07-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (29, 2, 46, CAST(N'2026-07-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (30, 2, 47, CAST(N'2026-08-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (31, 2, 48, CAST(N'2027-07-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (32, 2, 49, CAST(N'2028-01-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (33, 2, 50, CAST(N'2028-07-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (34, 3, 1, CAST(N'2012-12-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (35, 3, 56, CAST(N'2012-12-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (36, 3, 3, CAST(N'2012-12-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (37, 3, 34, CAST(N'2013-01-30' AS Date), 0, 0, 0, 32, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (38, 3, 41, CAST(N'2013-01-30' AS Date), 0, 0, 0, 40, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (39, 3, 64, CAST(N'2013-01-30' AS Date), 0, 0, 0, 7, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (40, 3, 35, CAST(N'2018-01-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (41, 3, 42, CAST(N'2018-01-11' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (42, 3, 65, CAST(N'2013-03-27' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (43, 3, 36, CAST(N'2018-02-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (44, 3, 43, CAST(N'2018-03-22' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (45, 3, 61, CAST(N'2013-06-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (46, 3, 59, CAST(N'2013-09-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (47, 3, 58, CAST(N'2017-12-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (48, 3, 62, CAST(N'2013-12-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (49, 3, 44, CAST(N'2013-12-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (50, 3, 57, CAST(N'2019-05-04' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (51, 3, 66, CAST(N'2019-04-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (52, 3, 63, CAST(N'2014-03-20' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (53, 3, 60, CAST(N'2014-12-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (54, 3, 45, CAST(N'2014-06-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (55, 3, 67, CAST(N'2017-12-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (56, 3, 30, CAST(N'2014-12-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (57, 3, 31, CAST(N'2016-12-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (58, 3, 32, CAST(N'2018-12-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (59, 3, 33, CAST(N'2020-12-17' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (60, 3, 48, CAST(N'2022-12-17' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (61, 3, 49, CAST(N'2023-06-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (62, 3, 50, CAST(N'2023-12-17' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (63, 4, 1, CAST(N'2015-05-19' AS Date), 0, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (64, 4, 56, CAST(N'2015-05-19' AS Date), 0, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (65, 4, 3, CAST(N'2015-05-19' AS Date), 0, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (66, 4, 34, CAST(N'2015-06-30' AS Date), 0, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (67, 4, 41, CAST(N'2015-06-30' AS Date), 0, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (68, 4, 35, CAST(N'2015-07-28' AS Date), 0, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (69, 4, 42, CAST(N'2015-08-25' AS Date), 0, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (70, 4, 36, CAST(N'2015-08-25' AS Date), 0, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (71, 4, 43, CAST(N'2015-11-03' AS Date), 0, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (72, 4, 59, CAST(N'2018-01-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (73, 4, 62, CAST(N'2018-02-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (74, 4, 44, CAST(N'2018-02-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (75, 4, 57, CAST(N'2018-03-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (76, 4, 66, CAST(N'2018-03-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (77, 4, 63, CAST(N'2018-05-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (78, 4, 60, CAST(N'2019-04-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (79, 4, 45, CAST(N'2018-08-17' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (80, 4, 67, CAST(N'2020-05-17' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (81, 4, 30, CAST(N'2018-04-20' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (82, 4, 31, CAST(N'2020-04-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (83, 4, 32, CAST(N'2022-04-19' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (84, 4, 33, CAST(N'2024-04-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (85, 4, 48, CAST(N'2025-05-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (86, 4, 49, CAST(N'2025-11-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (87, 4, 50, CAST(N'2026-05-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (88, 5, 1, CAST(N'2001-01-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (89, 5, 56, CAST(N'2001-01-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (90, 5, 3, CAST(N'2001-01-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (91, 5, 34, CAST(N'2001-03-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (92, 5, 41, CAST(N'2001-03-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (93, 5, 64, CAST(N'2001-03-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (94, 5, 35, CAST(N'2001-04-06' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (95, 5, 42, CAST(N'2001-05-04' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (96, 5, 65, CAST(N'2001-05-04' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (97, 5, 36, CAST(N'2001-05-04' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (98, 5, 43, CAST(N'2001-07-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (99, 5, 61, CAST(N'2001-07-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (100, 5, 59, CAST(N'2001-10-27' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (101, 5, 62, CAST(N'2002-01-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (102, 5, 44, CAST(N'2002-01-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (103, 5, 57, CAST(N'2002-07-27' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (104, 5, 66, CAST(N'2002-07-27' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (105, 5, 63, CAST(N'2002-04-27' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (106, 5, 60, CAST(N'2003-01-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (107, 5, 45, CAST(N'2002-07-27' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (108, 5, 67, CAST(N'2006-01-25' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (109, 5, 30, CAST(N'2003-01-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (110, 5, 31, CAST(N'2005-01-25' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (111, 5, 32, CAST(N'2007-01-25' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (112, 5, 33, CAST(N'2009-01-24' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (113, 5, 48, CAST(N'2011-01-24' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (114, 5, 49, CAST(N'2011-07-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (115, 5, 50, CAST(N'2012-01-24' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (116, 6, 1, CAST(N'2018-01-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (117, 6, 56, CAST(N'2018-01-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (118, 6, 3, CAST(N'2018-01-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (119, 6, 34, CAST(N'2018-01-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (120, 6, 41, CAST(N'2018-01-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (121, 6, 64, CAST(N'2018-01-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (122, 6, 35, CAST(N'2018-02-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (123, 6, 42, CAST(N'2018-03-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (124, 6, 65, CAST(N'2018-03-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (125, 6, 36, CAST(N'2018-03-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (126, 6, 43, CAST(N'2018-05-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (127, 6, 61, CAST(N'2018-05-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (128, 6, 59, CAST(N'2018-09-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (129, 6, 62, CAST(N'2018-12-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (130, 6, 44, CAST(N'2018-12-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (131, 6, 57, CAST(N'2019-06-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (132, 6, 66, CAST(N'2019-06-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (133, 6, 63, CAST(N'2019-03-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (134, 6, 60, CAST(N'2019-12-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (135, 6, 45, CAST(N'2019-06-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (136, 6, 30, CAST(N'2019-12-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (137, 6, 31, CAST(N'2021-11-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (138, 6, 32, CAST(N'2023-11-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (139, 6, 33, CAST(N'2025-11-29' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (140, 6, 46, CAST(N'2026-11-29' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (141, 6, 47, CAST(N'2026-12-29' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (142, 6, 48, CAST(N'2027-11-29' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (143, 6, 49, CAST(N'2028-05-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (144, 6, 50, CAST(N'2028-11-28' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (145, 7, 1, CAST(N'2017-01-01' AS Date), NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (146, 7, 56, CAST(N'2017-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (147, 7, 3, CAST(N'2017-01-01' AS Date), NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (148, 7, 34, CAST(N'2017-02-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (149, 7, 41, CAST(N'2017-02-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (150, 7, 64, CAST(N'2017-02-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (151, 7, 35, CAST(N'2017-03-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (152, 7, 42, CAST(N'2017-04-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (153, 7, 65, CAST(N'2017-04-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (154, 7, 36, CAST(N'2017-04-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (155, 7, 43, CAST(N'2017-06-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (156, 7, 61, CAST(N'2017-06-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (157, 7, 59, CAST(N'2017-10-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (158, 7, 58, CAST(N'2021-12-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (159, 7, 62, CAST(N'2018-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (160, 7, 44, CAST(N'2018-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (161, 7, 57, CAST(N'2018-07-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (162, 7, 66, CAST(N'2018-07-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (163, 7, 63, CAST(N'2018-04-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (164, 7, 60, CAST(N'2019-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (165, 7, 45, CAST(N'2018-07-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (166, 7, 30, CAST(N'2019-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (167, 7, 31, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (168, 7, 32, CAST(N'2022-12-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (169, 7, 33, CAST(N'2024-12-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (170, 7, 46, CAST(N'2025-12-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (171, 7, 47, CAST(N'2026-01-29' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (172, 7, 48, CAST(N'2026-12-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (173, 7, 49, CAST(N'2027-07-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (174, 7, 50, CAST(N'2027-12-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (175, 8, 1, CAST(N'2017-03-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (176, 8, 56, CAST(N'2017-03-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (177, 8, 3, CAST(N'2017-03-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (178, 8, 34, CAST(N'2017-04-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (179, 8, 41, CAST(N'2017-04-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (180, 8, 64, CAST(N'2017-04-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (181, 8, 35, CAST(N'2017-05-24' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (182, 8, 42, CAST(N'2017-06-21' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (183, 8, 65, CAST(N'2017-06-21' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (184, 8, 36, CAST(N'2017-06-21' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (185, 8, 43, CAST(N'2017-08-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (186, 8, 61, CAST(N'2017-08-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (187, 8, 59, CAST(N'2017-12-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (188, 8, 62, CAST(N'2018-03-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (189, 8, 44, CAST(N'2018-03-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (190, 8, 57, CAST(N'2018-09-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (191, 8, 66, CAST(N'2018-09-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (192, 8, 63, CAST(N'2018-06-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (193, 8, 60, CAST(N'2019-03-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (194, 8, 45, CAST(N'2018-09-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (195, 8, 30, CAST(N'2019-03-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (196, 8, 31, CAST(N'2021-03-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (197, 8, 32, CAST(N'2023-03-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (198, 8, 33, CAST(N'2025-03-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (199, 8, 48, CAST(N'2027-03-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (200, 8, 49, CAST(N'2027-09-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (201, 8, 50, CAST(N'2028-03-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (202, 9, 1, CAST(N'2012-10-01' AS Date), NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (203, 9, 56, CAST(N'2012-10-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (204, 9, 3, CAST(N'2012-10-01' AS Date), NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (205, 9, 34, CAST(N'2012-11-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (206, 9, 41, CAST(N'2012-11-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (207, 9, 35, CAST(N'2012-12-10' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (208, 9, 42, CAST(N'2013-01-07' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (209, 9, 36, CAST(N'2013-01-07' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (210, 9, 43, CAST(N'2013-03-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (211, 9, 61, CAST(N'2013-03-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (212, 9, 59, CAST(N'2013-07-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (213, 9, 62, CAST(N'2013-10-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (214, 9, 44, CAST(N'2013-10-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (215, 9, 57, CAST(N'2014-04-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (216, 9, 66, CAST(N'2014-04-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (217, 9, 63, CAST(N'2013-12-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (218, 9, 60, CAST(N'2014-10-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (219, 9, 45, CAST(N'2014-04-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (220, 9, 30, CAST(N'2014-10-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (221, 9, 31, CAST(N'2016-09-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (222, 9, 32, CAST(N'2018-09-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (223, 9, 33, CAST(N'2020-09-29' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (224, 9, 48, CAST(N'2022-09-29' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (225, 9, 49, CAST(N'2023-03-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (226, 9, 50, CAST(N'2023-09-29' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (227, 10, 1, CAST(N'2016-10-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (228, 10, 56, CAST(N'2016-10-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (229, 10, 3, CAST(N'2016-10-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (230, 10, 34, CAST(N'2016-11-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (231, 10, 41, CAST(N'2016-11-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (232, 10, 64, CAST(N'2016-11-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (233, 10, 35, CAST(N'2016-12-24' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (234, 10, 42, CAST(N'2017-01-21' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (235, 10, 65, CAST(N'2017-01-21' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (236, 10, 36, CAST(N'2017-01-21' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (237, 10, 43, CAST(N'2017-04-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (238, 10, 61, CAST(N'2017-04-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (239, 10, 59, CAST(N'2017-07-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (240, 10, 62, CAST(N'2017-10-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (241, 10, 44, CAST(N'2017-10-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (242, 10, 57, CAST(N'2018-04-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (243, 10, 66, CAST(N'2018-04-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (244, 10, 63, CAST(N'2018-01-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (245, 10, 60, CAST(N'2018-10-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (246, 10, 45, CAST(N'2018-04-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (247, 10, 30, CAST(N'2018-10-15' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (248, 10, 31, CAST(N'2020-10-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (249, 10, 32, CAST(N'2022-10-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (250, 10, 33, CAST(N'2024-10-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (251, 10, 48, CAST(N'2026-10-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (252, 10, 49, CAST(N'2027-04-14' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (253, 10, 50, CAST(N'2027-10-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (254, 11, 1, CAST(N'2017-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (255, 11, 56, CAST(N'2017-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (256, 11, 3, CAST(N'2017-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (257, 11, 34, CAST(N'2017-02-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (258, 11, 41, CAST(N'2017-02-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (259, 11, 64, CAST(N'2017-02-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (260, 11, 28, CAST(N'2017-02-26' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (261, 11, 35, CAST(N'2017-03-12' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (262, 11, 42, CAST(N'2017-04-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (263, 11, 65, CAST(N'2017-04-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (264, 11, 36, CAST(N'2017-04-09' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (265, 11, 43, CAST(N'2017-06-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (266, 11, 29, CAST(N'2017-04-23' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (267, 11, 61, CAST(N'2017-06-18' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (268, 11, 59, CAST(N'2017-10-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (269, 11, 58, CAST(N'2021-12-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (270, 11, 62, CAST(N'2018-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (271, 11, 44, CAST(N'2018-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (272, 11, 57, CAST(N'2018-07-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (273, 11, 66, CAST(N'2018-07-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (274, 11, 63, CAST(N'2018-04-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (275, 11, 60, CAST(N'2019-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (276, 11, 45, CAST(N'2018-07-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (277, 11, 67, CAST(N'2021-12-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (278, 11, 30, CAST(N'2019-01-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (279, 11, 31, CAST(N'2020-12-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (280, 11, 32, CAST(N'2022-12-31' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (281, 11, 33, CAST(N'2024-12-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (282, 11, 48, CAST(N'2026-12-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (283, 11, 49, CAST(N'2027-07-01' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (284, 11, 50, CAST(N'2027-12-30' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (285, 12, 1, CAST(N'2017-07-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (286, 12, 56, CAST(N'2017-07-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (287, 12, 3, CAST(N'2017-07-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (288, 12, 34, CAST(N'2017-08-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (289, 12, 41, CAST(N'2017-08-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (290, 12, 64, CAST(N'2017-08-16' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (291, 12, 35, CAST(N'2017-09-13' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (292, 12, 42, CAST(N'2017-10-11' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (293, 12, 65, CAST(N'2017-10-11' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (294, 12, 36, CAST(N'2017-10-11' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (295, 12, 43, CAST(N'2017-12-20' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (296, 12, 61, CAST(N'2017-12-20' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (297, 12, 59, CAST(N'2018-04-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (298, 12, 62, CAST(N'2018-07-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (299, 12, 44, CAST(N'2018-07-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (300, 12, 57, CAST(N'2019-01-03' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (301, 12, 66, CAST(N'2019-01-03' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (302, 12, 63, CAST(N'2018-10-04' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (303, 12, 60, CAST(N'2019-07-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (304, 12, 45, CAST(N'2019-01-03' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (305, 12, 30, CAST(N'2019-07-05' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (306, 12, 31, CAST(N'2021-07-04' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (307, 12, 32, CAST(N'2023-07-04' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (308, 12, 33, CAST(N'2025-07-03' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (309, 12, 46, CAST(N'2026-07-03' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (310, 12, 47, CAST(N'2026-08-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (311, 12, 48, CAST(N'2027-07-03' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (312, 12, 49, CAST(N'2028-01-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
INSERT [dbo].[Schedule] ([ID], [ChildId], [DoseId], [Date], [Weight], [Height], [Circle], [BrandId], [IsDone], [Due2EPI], [GivenDate]) VALUES (313, 12, 50, CAST(N'2028-07-02' AS Date), NULL, NULL, NULL, NULL, 0, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Message] ON 

GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (1, N'3335196658', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (2, N'3465430413', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (3, N'3327444653', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (4, N'3213000580', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (5, N'3006604519', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (6, N'3310097772', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (7, N'3335196658', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (8, N'3455112621', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (9, N'3066612853', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (10, N'3225156051', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (11, N'3335196658', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
INSERT [dbo].[Message] ([ID], [MobileNumber], [SMS], [Status]) VALUES (12, N'3238568526', N'<?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response><?xml version="1.0" encoding="utf-8"?>
<Response>
  <Message>Message Sent Successfully</Message>
</Response>', N'PENDING')
GO
SET IDENTITY_INSERT [dbo].[Message] OFF
GO
