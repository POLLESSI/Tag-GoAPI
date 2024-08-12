CREATE TABLE [dbo].[Bonus]
(
	[Bonus_Id] INT IDENTITY,
	[BonusType] NVARCHAR(32),
	[BonusDescription] NVARCHAR(256),
	[Application] NVARCHAR(64),
	[Granted] BIT DEFAULT 0,
	[Active] BIT DEFAULT 1

	CONSTRAINT PK_Bonus PRIMARY KEY ([Bonus_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteBonus]
	ON [dbo].[Bonus]
	INSTEAD OF DELETE
	AS
	BEGIN
		update Bonus SET Active = 0
		WHERE Bonus_Id = (SELECT Bonus_Id FROM deleted)
	END