CREATE TABLE [dbo].[Map]
(
	[Map_Id] INT IDENTITY,
	[DateCreation] DATE NOT NULL,
	[MapUrl] NVARCHAR(MAX) NOT NULL,
	[Description] NVARCHAR(64) NOT NULL,
	[Active] BIT DEFAULT 1

	CONSTRAINT PK_Map PRIMARY KEY ([Map_Id])
)

GO 

CREATE TRIGGER [dbo].[OnDeleteMap]
	ON [dbo].[Map]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE Map SET Active = 0
		WHERE Map_Id = (SELECT Map_Id FROM deleted)
	END
