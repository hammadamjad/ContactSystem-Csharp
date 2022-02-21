create database ContactSystem

use ContactSystem
go


create table Contact_Login(
login_id int identity(1,1) primary key,
username varchar(45),
userpassword varchar(45),
)
--check constraint
ALTER TABLE Contact_Login
ADD CONSTRAINT len_pass_check CHECK (Len(userpassword) >=4)

insert into Contact_Login values('hh12','1234')

create table Contact_Address(
address_id int identity(1,1) primary key,
address_1 varchar(45),
city varchar(45)
)


create table Contact_Info(
info_id int identity(1,1) primary key,
con_name varchar(45),
con_number varchar(45),
con_email varchar(45),
Address_id int foreign key  references Contact_Address(address_id)
)

--Alter table contact_info for default constraint
alter table Contact_Info
add constraint def_address
default 1 for Address_id

insert into Contact_Info values ('hammad','123','gmail.com')

create table User_Details(
detail_id int identity(1,1) primary key,
Login_id int foreign key  references Contact_Login(login_id),
Info_id int foreign key  references Contact_Info(info_id)
)

create table MyContacts(
myId int identity(1,1) primary key,
Info_id int,
Detail_id int foreign key  references User_Details(detail_id)
)

--Alter table mycontacts and changing Info_id to FK
alter table MyContacts
add constraint fk_info_id foreign key(Info_id)
references Contact_Info(info_id)



--table used for new contact
create table New_Entry(
entry_id int identity(1,1) primary key,
c_name varchar(45),
c_num varchar(45),
Register_date datetime
)

--trigger when a contact is inserted in my contacts
create trigger tr_contact_Insert
on MyContacts
for insert
as begin
declare @Name varchar(45)
declare @Num varchar(45)
select @Name = con_name, @Num = con_number from MyContacts
inner join Contact_Info on MyContacts.Info_id = Contact_Info.info_id
insert into New_Entry values (@Name, @Num, GETDATE())
end

--creating store procedure for Contact Address
Create proc sp_insert_Address
@address varchar(45),
@city varchar(45)
As
Begin
insert into Contact_Address values (@address,@city) 
End



--inserting into contact login
insert into Contact_Login values('Hammad123','comsats@123')

--inserting into contact address
insert into Contact_Address values('block A, street 1, people colony','attock')
insert into Contact_Address values('block B, street 2, officer colony','pindi')
insert into Contact_Address values('block C, street 3, food colony','lahore')
insert into Contact_Address values('block D, street 4, abaad colony','islamabad')
insert into Contact_Address values('block E, street 5, daraslam colony','sialkot')

--inserting into contact info
insert into Contact_Info values('Hammad Amjad','03155907200','hammadamjadali30@gmail.com',1)
insert into Contact_Info values('Ziyad Amjad','03167778880','ziyadamjad@gmail.com',6)
insert into Contact_Info values('Uzair Khan','03141112220','uzairkhan@gmail.com',3)
insert into Contact_Info values('Waleed Maqsood','03129898989','waleedmaqsood@gmail.com',4)
insert into Contact_Info values('Umer Khan','03034545445','umerkhan@gmail.com',5)

--inserting into user details
insert into User_Details values(1,1)

--inserting into mycontacts
insert into MyContacts values(7,1)
insert into MyContacts values(3,1)
insert into MyContacts values(4,1)
insert into MyContacts values(5,1)

--select query
select * from Contact_Info
select con_name from Contact_Info
select con_number from Contact_Info

--update query
update Contact_Address set city = 'attock' where city = 'pindi'
select * from Contact_Address where city = 'attock'


--Between query
select *
from Contact_Info
where info_id between 2 and 4;

--Like query
--query 1
select *
from Contact_info
where con_name LIKE 'h%d';
--query 2
select *
from Contact_info
where con_name LIKE '%n';

--In query
select *
from Contact_Address
where city IN ('attock','islamabad')

--order by clause
SELECT *
FROM Contact_Address
order BY city;

--group by clause
SELECT count(address_id),city
FROM Contact_Address
group BY city;

--having clause
SELECT count(address_id),city
FROM Contact_Address
group BY city
having city = 'attock'

--inserting for checking if check constraint works
insert into Contact_Login values('Usama khan','123')

--full join
select * from User_Details full join Contact_Info  on User_Details.Login_id = Contact_Info.info_id 
full join Contact_Address on Contact_Info.Address_id = Contact_Address.address_id;

--inner join
select * from Contact_Info inner join Contact_Address 
on Contact_Info.Address_id = Contact_Address.address_id 
