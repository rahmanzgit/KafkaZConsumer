CREATE TABLE [dbo].[TimeTable]
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	[key] varchar(10) not null,
	[time] varchar(10) not null,
	[value] decimal(15,14) not null,
	[CreatedDate] datetime not null
)
GO
Create PROCEDURE dbo.AddTimeTable
	@key varchar(10),
	@time varchar(10),
	@value decimal(15,14),
	@Id int out
AS
BEGIN
	insert into dbo.TimeTable([key], [time], [value], [CreatedDate])
	values (@key, @time, @value, GETDATE())
	set @Id = @@IDENTITY
END