insert into users values ('test','test')

pUsers;2 test,test 

CREATE TABLE Users (
    id INT PRIMARY KEY IDENTITY,
    username NVARCHAR(50) NOT NULL,
    pwd NVARCHAR(50) NOT NULL
);
drop table users