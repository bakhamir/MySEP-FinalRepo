CREATE PROCEDURE pUsers
    @username NVARCHAR(50),
    @password NVARCHAR(50),
    @email NVARCHAR(100) 
AS
BEGIN
    INSERT INTO Users (username, pwd, email)
    VALUES (@username, @password, @email);
END
CREATE PROCEDURE pUsers;2
    @login NVARCHAR(50),
    @pwd NVARCHAR(50)
AS
BEGIN
    SELECT id, username 
    FROM Users
    WHERE username = @login AND pwd = @pwd;
END
