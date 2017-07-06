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
('Polio', 0, 98);
GO

INSERT INTO Dose (Name, VaccineID) VALUES
('BCG - Dose 1', 1),
('Hepatitis B', 2),
('Polio - Dose 1', 3),
('Polio - Dose 2', 3),
('Polio - Dose 3', 3),
('Polio - Dose 4', 3);
GO