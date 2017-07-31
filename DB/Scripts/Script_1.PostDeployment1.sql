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

INSERT INTO Dose (Name, VaccineID) VALUES
('BCG # 1', (SELECT ID FROM Vaccine WHERE Name='BCG')),
('Hepatitis B', (SELECT ID FROM Vaccine WHERE Name='Hepatitis B')),
('Polio # 1', (SELECT ID FROM Vaccine WHERE Name='Polio')),
('Polio # 2', (SELECT ID FROM Vaccine WHERE Name='Polio')),
('Polio # 3', (SELECT ID FROM Vaccine WHERE Name='Polio')),
('Polio # 4', (SELECT ID FROM Vaccine WHERE Name='Polio')),
('Pentavalent # 1', (SELECT ID FROM Vaccine WHERE Name='Pentavalent')),
('Pentavalent # 2',  (SELECT ID FROM Vaccine WHERE Name='Pentavalent')),
('Pentavalent # 3', (SELECT ID FROM Vaccine WHERE Name='Pentavalent')),
('Phneumococal # 1', (SELECT ID FROM Vaccine WHERE Name='Phneumococal')),
('Phneumococal # 2', (SELECT ID FROM Vaccine WHERE Name='Phneumococal')),
('Phneumococal # 3', (SELECT ID FROM Vaccine WHERE Name='Phneumococal')),
('Rota Virus # 1', (SELECT ID FROM Vaccine WHERE Name='Rota Virus')),
('Rota Virus # 2', (SELECT ID FROM Vaccine WHERE Name='Rota Virus'));
GO

INSERT INTO [User] (MobileNumber, [Password], UserType) VALUES
('923331231231','1234','SUPERADMIN');
GO