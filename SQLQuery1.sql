Alter PROCEDURE pUsers
    @username NVARCHAR(50),
    @pwd NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Проверяем, существует ли пользователь с таким именем
    IF EXISTS (SELECT 1 FROM Users WHERE username = @username)
    BEGIN
        -- Если пользователь уже существует, просто завершаем процедуру
        RETURN;
    END

    -- Если пользователь с таким именем не существует, создаем новую запись
    INSERT INTO Users (username, pwd)
    VALUES (@username, @pwd);

END

UPDATE Users SET username = 'negr' WHERE id = 1
select * from users