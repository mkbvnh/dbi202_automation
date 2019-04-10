create table Students(
StuID int primary key,
Name nvarchar(30),
DateOfBirth date
)

create table Dom(
DomID int primary key,
Name nvarchar(50)
)

create table Rooms(
DomID int references Dom(DomID),
RoomNumber int,
Capacity int,
Floor int,
primary key(RoomNumber, DomID)
)

create table Live(	
DomID int,
RoomNumber int,
StuID int references Students(StuID),
StartDate Date,
EndDate Date,
primary key (DomID, RoomNumber, StuID, StartDate),
foreign key (RoomNumber,DomID) references Rooms(RoomNumber, DomID)
)
