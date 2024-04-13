-- Создание таблицы Users
CREATE TABLE Users (
    id INT PRIMARY KEY IDENTITY,
    username NVARCHAR(50) NOT NULL,
    pwd NVARCHAR(50) NOT NULL,
    email NVARCHAR(100) NOT NULL
);

-- Создание таблицы Books
CREATE TABLE Books (
    id INT PRIMARY KEY IDENTITY,
    title NVARCHAR(100) NOT NULL,
    genre NVARCHAR(50) NOT NULL,
    written DATE NOT NULL,
    rating INT NOT NULL,
    author nvarchar(200),
    contents nvarchar(max)
);
-- Вставка записей в таблицу Books
INSERT INTO Books (title, genre, written, author,rating,contents)
VALUES 
('BOOK1', 'GENRE1', '2023-01-01','the man', 85,'blah blah blah'),
('BOOK2', 'GENRE2', '2022-05-15','the man', 92,'blah blah blah'),
('BOOK3', 'GENRE3', '2024-02-20', 'the man',78,'blah blah blah');
drop table Books


CREATE PROCEDURE RateBook
    @id INT,
    @rating INT
AS
BEGIN
    UPDATE Books
    SET rating = @rating
    WHERE id = @id
END

CREATE TABLE Comments (
    id INT PRIMARY KEY IDENTITY,
    bookId INT NOT NULL,
    comment NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_Book_Comments FOREIGN KEY (bookId) REFERENCES Books(id)
);
