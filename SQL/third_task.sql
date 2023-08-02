CREATE TABLE Dates (
  id bigint,
  Dt date
);

insert into Dates (id, Dt) values
(1,'2023-02-01'),
(1,'2023-04-01'),
(2,'2023-02-04'),
(2,'2023-06-01'),
(2,'2023-12-01'),
(3,'2023-12-11'),
(3,'2023-12-15'),
(3,'2023-12-18'),
(3,'2023-12-21');

-- Запрос который выводит интервалы дат
select id, min(d.Dt) as Sd, max(d.Dt) as Ed  from Dates as d
group by id;

GO