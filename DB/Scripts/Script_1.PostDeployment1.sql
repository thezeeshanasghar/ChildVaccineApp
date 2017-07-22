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
('BCG',0,NULL),
('Hepatitis B', 0, NULL),
('Polio', 0, 98),
('Pentavalent', 42, 98),
('Phneumococal', 42, 98),
('Rota Virus', 42, 70);
GO

INSERT INTO Dose (Name,GapInDays, DoseOrder, VaccineID) VALUES
('BCG - Dose 1',0,1, 1),
('Hepatitis B',0 , 1, 2),
('Polio - Dose 1', 0 , 1, 3),
('Polio - Dose 2', 45 , 2, 3),
('Polio - Dose 3', 25 , 3, 3),
('Polio - Dose 4', 25 , 4, 3),
('Pentavalent - Dose 1', 42 , 1, 4),
('Pentavalent - Dose 2', 70,2,  4),
('Pentavalent - Dose 3', 98,3, 4),
('Phneumococal - Dose 1',42,1, 5),
('Phneumococal - Dose 2',70,2, 5),
('Phneumococal - Dose 3',14,3, 5),
('Rota Virus - Dose 1', 42,1,  6),
('Rota Virus - Dose 2', 70,2, 6);
GO

INSERT INTO [User] (MobileNumber, [Password], UserType) VALUES
('03331231231','1234','SUPERADMIN');
GO