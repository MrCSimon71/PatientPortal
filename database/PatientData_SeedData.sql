INSERT OR IGNORE INTO [User] (UserID, FirstName, LastName, Username, [Password], Email, CreatedBy, LastModifiedBy) 
	VALUES (1, "Admin", "User", "admin", "$2y$10$QWqfRcVbndL8CItC4PeVyudwVIWnorDVxXMUiJ4fvdUbvezWM/5Uy", "admin@planetdds.com", 1, 1);	
INSERT OR IGNORE INTO [User] (UserID, FirstName, LastName, Username, [Password], Email, CreatedBy, LastModifiedBy) 
	VALUES	(2, "Sarah", "Nichols", "snichols", "$2y$10$YsBk5YFFo6AKsS4SgcUwTu7xfOmMQURk.RmfvQEV8V0HzKwDSNFt.", "snichols@planetdds.com", 1, 1);	
INSERT OR IGNORE INTO [User] (UserID, FirstName, LastName, Username, [Password], Email, CreatedBy, LastModifiedBy) 
	VALUES	(3, "Bill", "Patron", "bpatron", "$2y$10$qTPCKtEl4BuMPpBTZ.kXqeoWw2oOkTGrYGBGflK34Fu25/qsEy/sW", "bpatron@planetdds.com", 1, 1);
	
	
INSERT OR IGNORE INTO [Patient] (PatientID, FirstName, LastName, Address1, City, [State], PostalCode, Email, PrimaryPhone, DOB, CreatedBy, LastModifiedBy)
	VALUES (1, "John", "Smith", "2300 W. Covina St.", "Covina", "CA", "91722", "jsmith101@example.com", "6267852322", "1996-03-14", 1, 1);		
INSERT OR IGNORE INTO [Patient] (PatientID, FirstName, LastName, Address1, City, [State], PostalCode, Email, PrimaryPhone, DOB, CreatedBy, LastModifiedBy)
	VALUES 	(2, "Mary", "Smith", "2300 W. Covina St.", "Covina", "CA", "91722", "msith99@example.com", "6264508066", "1999-07-05", 1, 1);
INSERT OR IGNORE INTO [Patient] (PatientID, FirstName, LastName, Address1, City, [State], PostalCode, Email, PrimaryPhone, DOB, CreatedBy, LastModifiedBy)
	VALUES 	(3, "Derrick", "London", "345 Tupperware Dr", "Covina", "CA", "91722", "dlondon@example.com", "6262153422", "2000-02-20", 1, 1);
INSERT OR IGNORE INTO [Patient] (PatientID, FirstName, LastName, Address1, City, [State], PostalCode, Email, PrimaryPhone, DOB, CreatedBy, LastModifiedBy)
	VALUES 	(4, "Paulina", "McGregor", "6100 Pottery Ln", "Covina", "CA", "91722", "pmgr12@example.com", "6264476300", "1993-06-18", 1, 1);
INSERT OR IGNORE INTO [Patient] (PatientID, FirstName, LastName, Address1, City, [State], PostalCode, Email, PrimaryPhone, DOB, CreatedBy, LastModifiedBy)
	VALUES 	(5, "William", "Jones", "9011 S. Grand Ave.", "Covina", "CA", "91722", "wjones71@example.com", "6265331200", "1989-11-22", 1, 1);
