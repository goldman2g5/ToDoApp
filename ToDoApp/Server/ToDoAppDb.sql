USE master
GO
IF EXISTS (
 SELECT [name]
  FROM sys.databases
  WHERE [name] = N'ToDoApp'
)
DROP DATABASE ToDoApp
GO

set ansi_nulls on
go
set ansi_padding on
go
set quoted_identifier on
go
create database [ToDoApp]
go
use [ToDoApp]
go


create table [dbo].[User]
(
    [Id] int identity(1, 1) constraint User_Id primary key,
    [Username]     varchar(max) not null,
    [PasswordHash] varbinary(max) not null,
    [PasswordSalt] varbinary(max) not null
)
go

create table [dbo].[Board]
(
   [Id] int     not null identity(1,1) constraint Board_Id primary key,
   [Name] varchar(max)  not null,
)
go

create table AppointmentData
(
    [Id]      int identity(1, 1) constraint AppointmentData_Id primary key,
    [Subject]             varchar(max),
    [Location]            varchar(max),
    [StartTime]           datetime not null,
    [EndTime]             datetime not null,
    [Description]         varchar(max),
    [IsAllDay]            bit      not null,
    [RecurrenceRule]      varchar(max),
    [RecurrenceException] varchar(max),
    [RecurrenceID]        int,
    [Creator]             int null,
    [Assigned_To]         int null,
    [Board]               int null,
   constraint [FK_Board] foreign key ([Board])
   references [dbo].[Board] ([Id]),
   constraint [FK_Board_Creator] foreign key ([Creator])
   references [dbo].[User] ([Id]),
   constraint [FK_Board_Assigned_To] foreign key ([Assigned_To])
   references [dbo].[User] ([Id])
)
go
create table [dbo].[Connection]
(
   [Id] int    identity(1,1) constraint Connection_Id primary key,
   [User] int  not null,
   [Board] int  not null,
   constraint [FK_Connection_User] foreign key ([User])
   references [dbo].[User] ([Id]),
   constraint [FK_Connection_Board] foreign key ([Board])
   references [dbo].[Board] ([Id])
)
go