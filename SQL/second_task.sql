CREATE TABLE Clients (
  id bigint IDENTITY(1,1) PRIMARY KEY,
  ClientName nvarchar(200)
);

CREATE TABLE ClientContacts (
  id bigint IDENTITY(1,1) PRIMARY KEY,
  ClientId bigint,
  ContactType nvarchar(255),
  ContactValue nvarchar(255)
);

insert into Clients (ClientName) values ('Client1'), ('Client2'), ('Client3');
insert into ClientContacts (ClientId, ContactType, ContactValue) 
values 
(1,"Type1", "123123123"),
(1,"Type1", "234234234"),
(1,"Type2", "345634565"),
(2,"Type1", "111111111"), 
(2,"Type1", "3333334333"), 
(3,"Type3", "222222222");

-- Первый запрос, возвращающий наименования клиентов и кол-во их контактов
select c.ClientName, count(cc.ClientId) as ContactCount from Clients as c, ClientContacts as cc
where c.Id = cc.ClientId
group by c.ClientName

-- Второй запрос, возвращающий список клиентов, у которых более 2 контактов
select c.ClientName, count(cc.ClientId) as ContactCount from Clients as c, ClientContacts as cc
where c.Id = cc.ClientId
group by c.ClientName
having count(cc.ClientId) > 2

GO
