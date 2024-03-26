create table _user (
    id int primary key identity,
    username nvarchar(50) unique not null,
    password_hash nvarchar(200) not null
);
create procedure check_user
    @username nvarchar(50),
    @password nvarchar(200)
as
begin
    if exists (select 1 from _user where username = @username and pwdcompare(@password, password_hash) = 1)
        select 1 as authenticated;
    else
        select 0 as authenticated;
end;

create procedure create_user
    @username nvarchar(50),
    @password nvarchar(200)
as
begin
    insert into _user (username, password_hash)
    values (@username, pwdencrypt(@password));
end;
