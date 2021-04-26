create database Hotel;
use Hotel;

create table Users (
id int Identity(1,1) primary key,
[login] varchar(20),
[password] varchar(20)
)

create table Rooms (
id int Identity(1,1) primary key,
number int,
amount int,
price decimal(5,2)
)

create table ClientCard (
id int Identity(1,1) primary key,
full_name varchar(200),
passport_number varchar(10),
telephone_number varchar(12)
)

create table RoomCard (
id int Identity(1,1) primary key,
id_room int references Rooms(id),
id_client_card int references ClientCard(id),
food bit,
[services] bit,
amount int
)

create table Dates (
id int Identity(1,1),
id_room_card int references RoomCard(id),
datein date,
dateout date
)

select * from Users;