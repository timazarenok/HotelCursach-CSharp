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
price decimal(5,2)
)
insert into Rooms values (11, 200)
select * from Rooms;

create table ClientCard (
id int Identity(1,1) primary key,
full_name varchar(200),
passport_number bigint,
telephone_number varchar(12)
)

insert into ClientCard values('gggggg', 3434343, '375296055004')

create table RoomCard (
id int Identity(1,1) primary key,
id_room int references Rooms(id),
id_client_card int references ClientCard(id),
food bit,
[services] bit,
amount int
)

insert into RoomCard values(1, 1, 1, 1, 3)
select * from RoomCard join Rooms on RoomCard.id = id_room

SELECT * FROM Rooms WHERE NOT EXISTS (SELECT * FROM RoomCard WHERE Rooms.id=RoomCard.id_room);

create table Dates (
id int Identity(1,1),
id_room_card int references RoomCard(id),
datein date,
dateout date
)

select * from Dates;