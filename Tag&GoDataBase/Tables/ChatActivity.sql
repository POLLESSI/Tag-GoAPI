CREATE TABLE [dbo].[ChatActivity]
(
	[ChatActivity_Id] INT IDENTITY,
	[NewMessage] NVARCHAR(64) NULL,
	[Author] NVARCHAR(64) NULL,
	[SendingDate] DATE DEFAULT GETDATE(),
	[Activity_Id] INT NULL,
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_ChatActivity] PRIMARY KEY ([ChatActivity_Id])
	CONSTRAINT [FK_ChatActivity_Activity] FOREIGN KEY (Activity_Id) REFERENCES [Activity] ([Activity_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteChatActivity]
	ON [dbo].[ChatActivity]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE ChatActivity SET Active = 0
		WHERE ChatActivity_Id = (SELECT ChatActivity_Id FROM deleted)
	END


