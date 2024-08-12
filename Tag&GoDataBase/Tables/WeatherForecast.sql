CREATE TABLE [dbo].[WeatherForecast]
(
	[WeatherForecast_Id] INT IDENTITY,
	[Date] SMALLDATETIME NULL DEFAULT GETDATE(),
	[TemperatureC] NVARCHAR(4) NOT NULL,
	[TemperatureF] NVARCHAR(4) NULL,
	[Summary] NVARCHAR(256) NOT NULL,
	[Description] NVARCHAR(256) NOT NULL,
	[Humidity] FLOAT NULL,
	[Precipitation] FLOAT NULL,
	[NEvenement_Id] INT NOT NULL,
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_WeatherForecast] PRIMARY KEY ([WeatherForecast_Id]),
	CONSTRAINT [FK_WeatherForecast_NEvenement] FOREIGN KEY (NEvenement_Id) REFERENCES [NEvenement] ([NEvenement_Id])
)

GO 

CREATE TRIGGER [dbo].[OnDeleteWeatherForecast]
	ON [dbo].[weatherForecast]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE WeatherForecast SET Active = 0
		WHERE WeatherForecast_Id = (SELECT WeatherForecast_Id FROM deleted)
	END