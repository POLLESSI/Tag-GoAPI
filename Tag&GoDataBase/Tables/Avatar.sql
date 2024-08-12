CREATE TABLE [dbo].[Avatar]
(
	[Avatar_Id] INT IDENTITY,
	[AvatarName] NVARCHAR(32) NULL,
	[AvatarUrl] NVARCHAR(MAX) NOT NULL,
	[Description] NVARCHAR(256) NOT NULL,
	[Active] BIT DEFAULT 1 

	CONSTRAINT PK_Avatar PRIMARY KEY ([Avatar_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteAvatar]
	ON [dbo].[Avatar]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE Avatar SET Active = 0
		WHERE Avatar_Id = (SELECT Avatar_Id FROM deleted)
	END