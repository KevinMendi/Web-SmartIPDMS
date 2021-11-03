IF NOT EXISTS (SELECT * FROM lk_UserRole WHERE role_desc = 'Super Admin') INSERT INTO lk_UserRole VALUES ('Super Admin')
IF NOT EXISTS (SELECT * FROM lk_UserRole WHERE role_desc = 'Admin') INSERT INTO lk_UserRole VALUES ('Admin')
IF NOT EXISTS (SELECT * FROM lk_UserRole WHERE role_desc = 'Agent') INSERT INTO lk_UserRole VALUES ('Agent')