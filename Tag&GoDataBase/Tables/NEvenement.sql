CREATE TABLE [dbo].[NEvenement]
(
	[NEvenement_Id] INT IDENTITY,
	[NEvenementDate] DATE NOT NULL,
	[NEvenementName] NVARCHAR(64) NOT NULL,
	[NEvenementDescription] NVARCHAR(256) NOT NULL,
	[PosLat] DECIMAL(8, 6) NOT NULL,
	[PosLong] DECIMAL(9, 6) NOT NULL,
	[Positif] BIT DEFAULT 1,
	[Organisateur_Id] INT NULL,
	[NIcon_Id] INT NULL,
	[Recompense_Id] INT NULL,
	[Bonus_Id] INT NULL,
	[MediaItem_Id] INT NULL,
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_NEvenement] PRIMARY KEY ([NEvenement_Id]),
	CONSTRAINT [FK_NEvenement_Organisateur] FOREIGN KEY (Organisateur_Id) REFERENCES [Organisateur] ([Organisateur_Id]),
	CONSTRAINT [FK_NEvenement_NIcon] FOREIGN KEY (NIcon_Id) REFERENCES [NIcon] ([NIcon_Id]),
	CONSTRAINT [FK_NEvenement_Recompense] FOREIGN KEY (Recompense_Id) REFERENCES [Recompense] ([Recompense_Id]),
	CONSTRAINT [FK_NEvenement_Bonus] FOREIGN KEY (Bonus_Id) REFERENCES [Bonus] ([Bonus_Id]),
	CONSTRAINT [FK_NEvenement_MediaItem] FOREIGN KEY (MediaItem_Id) REFERENCES [MediaItem] ([MediaItem_Id]),
)

GO 

CREATE TRIGGER [dbo].[OnDeleteNEvenement]
	ON [dbo].[NEvenement]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE NEvenement SET Active = 0
		WHERE NEvenement_Id = (SELECT NEvenement_Id FROM deleted)
	END