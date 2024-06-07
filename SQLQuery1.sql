set ansi_nulls on
go
set ansi_padding on
go
set quoted_identifier on
go

drop database [technoservice]
go

create database [technoservice]
go



use [technoservice]
go


create table [dbo].[Client]
(
[ID_Client] [int] not null identity (1,1),
[Login] [varchar] (12) not null,
[Password] [varchar] (20) not null,
constraint [UQ_Login] unique ([Login]),
constraint [CH_Login] check (len([Login])>=4),
constraint [CH_Password] check (len([Password])>=4),
constraint [PK_Client] primary key clustered
([ID_Client] ASC) on [PRIMARY],
)
go

insert into [dbo].[Client] ([Login], [Password])
values ('qwer', 'qwer')
go


create table [dbo].[Device]
(
[ID_Device] [int] not null identity (1,1),
[Name] [varchar] (50) not null,
constraint [PK_Device] primary key clustered
([ID_Device] ASC) on [PRIMARY],
)
go

insert into [dbo].[Device] ([Name])
values ('ШЫРВАВЫФЗЩШО')
go

create table [dbo].[Type]
(
[ID_Type] [int] not null identity (1,1),
[Name] [varchar] (50) not null,
constraint [PK_Type] primary key clustered
([ID_Type] ASC) on [PRIMARY],
)
go

insert into [dbo].[Type] ([Name])
values ('qwe')
go

create table [dbo].[Status]
(
[ID_Status] [int] not null identity (1,1),
[Name] [varchar] (50) not null,
constraint [PK_Status] primary key clustered
([ID_Status] ASC) on [PRIMARY],
)
go

insert into [dbo].[Status] ([Name])
values ('В ожидании'),
('В работе'),
('Выполнено')
go

select * from [dbo].[Status]

create table [dbo].[Sotrudnik]
(
[ID_Sotrudnik] [int] not null identity (1,1),
[Login] [varchar] (12) not null,
[Password] [varchar] (20) not null,
constraint [UQ_Login_Sot] unique ([Login]),
constraint [CH_Login_Sot] check (len([Login])>=4),
constraint [CH_Password_Sot] check (len([Password])>=4),
constraint [PK_Sotrudnik] primary key clustered
([ID_Sotrudnik] ASC) on [PRIMARY],
)
go

insert into [dbo].[Sotrudnik] ([Login], [Password])
values ('asdf', 'asdf')
go

create table [dbo].[Zayavka]
(
[ID_Zayavka] [int] not null identity (1,1),
[Number] [int] not null,
[Date] [date] not null,
[Problem] [varchar] (200) not null,
[Device_ID] [int] not null,
[Type_ID] [int] not null,
[Client_ID] [int] not null,
[Status_ID] [int] not null,
constraint [FK_Device] foreign key ([Device_ID])
references [dbo].[Device] ([ID_Device]),
constraint [FK_Type] foreign key ([Type_ID])
references [dbo].[Type] ([ID_Type]),
constraint [FK_Client] foreign key ([Client_ID])
references [dbo].[Client] ([ID_Client]),
constraint [FK_Status] foreign key ([Status_ID])
references [dbo].[Status] ([ID_Status]),
constraint [PK_Zayavka] primary key clustered
([ID_Zayavka] ASC) on [PRIMARY],
)
go

insert into [dbo].[Zayavka] ([Number], [Date], [Problem], [Device_ID], [Type_ID], [Client_ID], [Status_ID])
values ('1234', '2023-02-02', 'awqeq', 1, 1, 1, 1)
go