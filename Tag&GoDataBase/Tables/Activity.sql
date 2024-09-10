CREATE TABLE [dbo].[Activity]
(
	[Activity_Id] INT IDENTITY,
	[ActivityName] NVARCHAR(64) NOT NULL,
	[ActivityAddress] NVARCHAR(64) NOT NULL,
	[ActivityDescription] NVARCHAR(256) NOT NULL,
	[ComplementareInformation] NVARCHAR(64) NULL,
	[PosLat] DECIMAL(8, 6) NOT NULL,
	[PosLong] DECIMAL(9, 6) NOT NULL,
	[Organisateur_Id] INT NOT NULL,
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_Activity] PRIMARY KEY ([Activity_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteActivity]
	ON [dbo].[Activity]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE Activity SET Active = 0
		WHERE Activity_Id = (SELECT Activity_Id FROM deleted)
	END