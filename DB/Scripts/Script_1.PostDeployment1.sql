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
--INSERT INTO Vaccine(Name, MinAge, MaxAge) VALUES
--('BCG', 0, NULL),
--('OPV', 0, NULL),
--('PCV', 42, NULL),
--('Rota Virus GE', 42, NULL),
--('Chicken Pox', 84, NULL),
--('MMR', 84, NULL),
--('MenB Vaccine', 56, NULL),
--('MenACWY vaccine', 63, NULL),
--('Yellow Fever', 63, NULL),
--('Typhoid', 168, NULL),
--('Flu Vaccine', 168, NULL),
--('OPV/IPV+DPT+Hib+HBV', 42, NULL);
--GO
--INSERT INTO Dose (Name, VaccineID) VALUES
--('BCG # 1', (SELECT ID FROM Vaccine WHERE Name='BCG')),
--('OPV # 0', (SELECT ID FROM Vaccine WHERE Name='OPV')),
--('OPV # 1', (SELECT ID FROM Vaccine WHERE Name='OPV')),
--('OPV # 2', (SELECT ID FROM Vaccine WHERE Name='OPV')),
--('OPV # 3', (SELECT ID FROM Vaccine WHERE Name='OPV')),
--('OPV # 4', (SELECT ID FROM Vaccine WHERE Name='OPV')),
--('PENTAVALENT # 1', (SELECT ID FROM Vaccine WHERE Name='OPV/IPV+DPT+Hib+HBV')),
--('PENTAVALENT # 2', (SELECT ID FROM Vaccine WHERE Name='OPV/IPV+DPT+Hib+HBV')),
--('PENTAVALENT # 3', (SELECT ID FROM Vaccine WHERE Name='OPV/IPV+DPT+Hib+HBV')),
--('PENTAVALENT # 4', (SELECT ID FROM Vaccine WHERE Name='OPV/IPV+DPT+Hib+HBV')),
--('PCV # 1', (SELECT ID FROM Vaccine WHERE Name='PCV')),
--('PCV # 2', (SELECT ID FROM Vaccine WHERE Name='PCV')),
--('PCV # 3', (SELECT ID FROM Vaccine WHERE Name='PCV')),
--('PCV # 4', (SELECT ID FROM Vaccine WHERE Name='PCV'));
--GO

INSERT INTO Vaccine(Name, MinAge, MaxAge) VALUES
('BCG', 0, NULL),
('OPV', 0, NULL),
('PENTAVALENT', 42, NULL),
('PCV', 42, NULL),
('IPV', 42, NULL),
('Measles', 168, NULL),
('ROTARIX', 42, NULL),
('Hexavalent', 42, NULL),
('CHICKEN POX', 84, NULL),
('MMR', 84, NULL);
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
('3331231231','1234','SUPERADMIN','92');
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