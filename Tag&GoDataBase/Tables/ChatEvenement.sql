CREATE TABLE [dbo].[ChatEvenement]
(
	[ChatEvenement_Id] INT IDENTITY,
	[NewMessage] NVARCHAR(64) NULL,
	[Author] NVARCHAR(64) NULL,
	[SendingDate] DATE DEFAULT GETDATE(),
	[NEvenement_Id] INT NULL,
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_ChatEvenement] PRIMARY KEY ([ChatEvenement_Id])
	CONSTRAINT [FK_ChatActivity_NEvenement] FOREIGN KEY (NEvenement_Id) REFERENCES [NEvenement] ([NEvenement_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteChatEvenement]
	ON [dbo].[ChatEvenement]
	INSTEAD OF DELETE
	AS 
	BEGIN
		UPDATE ChatEvenement SET Active = 0
		WHERE ChatEvenement_Id = (SELECT ChatEvenement_Id FROM deleted)
	END