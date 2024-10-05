CREATE TABLE [dbo].[NVote]
(
	[NVote_Id] INT IDENTITY,
	[NEvenement_Id] INT NOT NULL,
	[FunOrNot] BIT NULL,
	[Comment] NVARCHAR(128),
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_NVote] PRIMARY KEY ([NVote_Id]),
	CONSTRAINT [FK_NEvenement_NVote] FOREIGN KEY (NEvenement_Id) REFERENCES [NEvenement] ([NEvenement_Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteNVote]
	ON [dbo].[NVote]
	INSTEAD OF DELETE
	AS 
	BEGIN
		UPDATE NVote SET Active = 0
		WHERE NVote_Id = (SELECT NVote_Id FROM deleted)
	END