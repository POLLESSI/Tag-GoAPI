CREATE TABLE [dbo].[Chat]
(
	[Chat_Id] INT IDENTITY,
	[NewMessage] NVARCHAR(64) NULL,
	[SendingDate] DATE DEFAULT GETDATE(),
	[NEvenement_Id] INT NULL,
	[Activity_Id] INT NULL,
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_Chat] PRIMARY KEY ([Chat_Id])
	CONSTRAINT [FK_Chat_NEvenement] FOREIGN KEY (NEvenement_Id) REFERENCES [NEvenement] ([NEvenement_Id])
	CONSTRAINT [FK_Chat_Activity] FOREIGN KEY (Activity_Id) REFERENCES [Activity] ([Activity_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteChat]
	ON [dbo].[Chat]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE Chat SET Active = 0
		WHERE Chat_Id = (SELECT Chat_Id FROM deleted)
	END


