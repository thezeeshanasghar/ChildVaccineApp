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
INSERT INTO Vaccine(Name, MinAge, MaxAge) VALUES
('BCG', 0, NULL),
('OPV', 0, NULL),
('PENTAVALENT', 42, NULL),
('PCV', 42, NULL),
('IPV', 42, NULL),
('Measles', 168, NULL),
('ROTARIX', 42, NULL);
GO
INSERT INTO Dose (Name, VaccineID) VALUES
('BCG # 1', (SELECT ID FROM Vaccine WHERE Name='BCG')),
('OPV # 0', (SELECT ID FROM Vaccine WHERE Name='OPV')),
('OPV # 1', (SELECT ID FROM Vaccine WHERE Name='OPV')),
('OPV # 2', (SELECT ID FROM Vaccine WHERE Name='OPV')),
('OPV # 3', (SELECT ID FROM Vaccine WHERE Name='OPV')),
('OPV # 4', (SELECT ID FROM Vaccine WHERE Name='OPV')),
('PENTAVALENT # 1', (SELECT ID FROM Vaccine WHERE Name='PENTAVALENT')),
('PENTAVALENT # 2', (SELECT ID FROM Vaccine WHERE Name='PENTAVALENT')),
('PENTAVALENT # 3', (SELECT ID FROM Vaccine WHERE Name='PENTAVALENT')),
('PENTAVALENT # 4', (SELECT ID FROM Vaccine WHERE Name='PENTAVALENT')),
('PCV # 1', (SELECT ID FROM Vaccine WHERE Name='PCV')),
('PCV # 2', (SELECT ID FROM Vaccine WHERE Name='PCV')),
('PCV # 3', (SELECT ID FROM Vaccine WHERE Name='PCV')),
('PCV # 4', (SELECT ID FROM Vaccine WHERE Name='PCV')),
('IPV # 1', (SELECT ID FROM Vaccine WHERE Name='IPV')),
('IPV # 2', (SELECT ID FROM Vaccine WHERE Name='IPV')),
('IPV # 3', (SELECT ID FROM Vaccine WHERE Name='IPV')),
('IPV # 4', (SELECT ID FROM Vaccine WHERE Name='IPV')),
('Measles # 1', (SELECT ID FROM Vaccine WHERE Name='Measles')),
('Measles # 2', (SELECT ID FROM Vaccine WHERE Name='Measles')),
('ROTARIX # 1', (SELECT ID FROM Vaccine WHERE Name='ROTARIX')),
('ROTARIX # 2', (SELECT ID FROM Vaccine WHERE Name='ROTARIX'));
GO

INSERT INTO [User] (MobileNumber, [Password], UserType, CountryCode) VALUES
('3331231231','1234','SUPERADMIN','92'),
('3335196658','0431','DOCTOR','92');
GO

INSERT [dbo].[Doctor] ([FirstName], [LastName], [Email], [PMDC], [IsApproved], [ShowPhone], [ShowMobile], [PhoneNo],[UserID]) 
VALUES (N'Salman', N'Bajwa', N'salmanbajwa777@gmail.com', N'12345-p', 0, 1, 1, N'03335196658',(select Id from [User] where MobileNumber = '3335196658'))
GO
INSERT [dbo].[Clinic] ([Name], [OffDays], [StartTime], [EndTime], [Lat], [Long], [DoctorID], [PhoneNumber], [IsOnline])
VALUES (N'KIDS & FAMILY clinic', N'Sunday', CAST(N'19:30:00' AS Time), CAST(N'22:00:00' AS Time), 33.590258458777022, 73.133164149047843, 
 (SELECT ID FROM Doctor WHERE FirstName='Salman' AND LastName='Bajwa'), N'03335196658', 1)
GO
INSERT INTO dbo.Brand(Name,VaccineID) VALUES
('BCG Brand', (SELECT ID FROM Vaccine WHERE Name='BCG')),
('OPV Brand', (SELECT ID FROM Vaccine WHERE Name='OPV')),
('PENTAVALENT Brand', (SELECT ID FROM Vaccine WHERE Name='PENTAVALENT')),
('PCV Brand', (SELECT ID FROM Vaccine WHERE Name='PCV')),
('IPV Brand', (SELECT ID FROM Vaccine WHERE Name='IPV')),
('Measles Brand', (SELECT ID FROM Vaccine WHERE Name='Measles')),
('ROTARIX Brand', (SELECT ID FROM Vaccine WHERE Name='ROTARIX'))
GO