CREATE TABLE [dbo].[Organisateur]
(
	[Organisateur_Id] INT IDENTITY,
	[CompanyName] NVARCHAR(64) NULL,
	[BusinessNumber] NVARCHAR(16) NOT NULL,
	[NUser_Id] INT NOT NULL,
	[Point] INT NULL DEFAULT 0,
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_Organisateur] PRIMARY KEY ([Organisateur_Id]),
	CONSTRAINT [FK_Organisateur_NUser] FOREIGN KEY (NUser_Id) REFERENCES [NUser] ([NUser_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteOrganisateur]
	ON [dbo].[Organisateur]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE Organisateur SET Active = 0
		WHERE Organisateur_Id = (SELECT Organisateur_Id FROM deleted)
	END