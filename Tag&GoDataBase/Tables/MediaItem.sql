CREATE TABLE [dbo].[MediaItem]
(
	[MediaItem_Id] INT IDENTITY,
	[MediaType] NVARCHAR(32) NOT NULL,
	[UrlItem] NVARCHAR(MAX) NOT NULL,
	[Description] NVARCHAR(256) NOT NULL,
	[Active] BIT DEFAULT 1

	CONSTRAINT PK_MediaItem PRIMARY KEY ([MediaItem_Id])

)

GO

CREATE TRIGGER [dbo].[OnDeleteMediaItem]
	ON [dbo].[MediaItem]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE MediaItem SET Active = 0
		WHERE MediaItem_Id = (SELECT MediaItem_Id FROM deleted)
	END