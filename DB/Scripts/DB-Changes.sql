ALTER TABLE Schedule
ADD GivenDate date

alter table dose
alter column MinAge int not null

Alter table Doctor
Add  [AllowInvoice] BIT NOT NULL DEFAULT(1), 
	 [AllowChart] BIT NOT NULL DEFAULT(1), 
     [AllowFollowUp] BIT NOT NULL DEFAULT(1), 
     [AllowInventory] BIT NOT NULL DEFAULT (1)