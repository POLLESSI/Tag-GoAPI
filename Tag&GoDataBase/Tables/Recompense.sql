CREATE TABLE [dbo].[Recompense]
(
	[Recompense_Id] INT IDENTITY,
	[Definition] NVARCHAR(64),
	[Point] INT,
	[Implication] NVARCHAR(256),
	[Granted] BIT DEFAULT 0,
	[Active] BIT DEFAULT 1

	CONSTRAINT PK_Recompense PRIMARY KEY ([Recompense_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteRecompense]
	ON [dbo].[Recompense]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE Recompense SET Active = 0
		WHERE Recompense_Id = (SELECT Recompense_Id FROM deleted)
	END