CREATE PROCEDURE AddSong
    @title NVARCHAR(300),
    @catId INT,
    @duration INT
AS
BEGIN
    INSERT INTO Music (title, catId, duration) VALUES (@title, @catId, @duration)
END

CREATE PROCEDURE GetAllSongs
AS
BEGIN
    SELECT * FROM Music
END

CREATE PROCEDURE EditSong
    @id INT,
    @title NVARCHAR(300),
    @catId INT,
    @duration INT
AS
BEGIN
    UPDATE Music SET title = @title, catId = @catId, duration = @duration WHERE id = @id
END

CREATE PROCEDURE DelSong
    @id INT
AS
BEGIN
    DELETE FROM Music WHERE id = @id
END

CREATE PROCEDURE GetAllCategories
AS
BEGIN
    SELECT * FROM Category
END

    create table Music(
 id int identity primary key,
 title nvarchar(299),
 catId int foreign key references Category,
 duration int 
)
create table Category(
id int identity primary key,
genre nvarchar(400)
)